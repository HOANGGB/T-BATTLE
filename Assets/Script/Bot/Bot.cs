using System.Collections;
using UnityEngine;
using Pathfinding;
public abstract class Bot : MonoBehaviour
{
    // Start is called before the first frame update
    public int heal;
    public int healMax;

    public int damageBonus;
    public int damageDeathZone;


    public float timeFire, timeSkill, timeTakeDmg,timeDZ,timeTakeDamageDeathZone =.5f;

    public GameObject upperBody, lowerBody, firePoint;
    public GameObject target;
    public Tank tankCurrent;
    public ParticleSystem fireEffect;
    [HideInInspector] public bool isMove, canFire;
    [SerializeField] LayerMask obstacleLayer;

    Transform centerMapPoint;
    Rigidbody rb;
    Seeker seeker;
    Path path;
    Coroutine moveCorou;
    [HideInInspector]public UIBot uIBot;
    [HideInInspector] public TankSound tankSound;
    [HideInInspector] public Animator anim;


    void Start()
    {
        heal =tankCurrent.heal;
        healMax = heal;
        rb = GetComponent<Rigidbody>();
        seeker = GetComponent<Seeker>();
        uIBot = GetComponentInChildren<UIBot>();
        tankSound = GetComponent<TankSound>();
        anim = GetComponent<Animator>();
        Assign();


    }
    void Assign(){
        upperBody = transform.Find("UpperBody").gameObject;
        lowerBody = transform.Find("LowerBody").gameObject;
        centerMapPoint = GameObject.Find("CenterMapPoint").transform;
        target= GameObject.Find("CenterMapPoint").gameObject;
        InvokeRepeating(nameof(CalculatePath),0f,2f);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Action();
        CoolDown();
        if(Input.GetKeyDown(KeyCode.J)){
            CalculatePath();
        }
        if(Input.GetKeyDown(KeyCode.K)){
            StopMoveInPath();
        }
    }
    void Rotate(){
        if(!target) return;
        var targetNor = (target.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetNor.x,transform.position.y,targetNor.z));
        upperBody.transform.rotation = Quaternion.Slerp(upperBody.transform.rotation, targetRotation,tankCurrent.SpeedRotation);
        // lowerBody.transform.rotation = Quaternion.Slerp(lowerBody.transform.rotation, targetRotation,tankCurrent.SpeedRotation);

    }

    public abstract void Action();
    public void TakeDamage(int damage,GameObject origin, Vector3 pos){
        if(timeTakeDmg<=0){
            timeTakeDmg = .1f;
            heal -= damage;
            uIBot.UpdateHeal(heal,healMax);
            if(heal<=0) Deal(origin);
            else{
                var dmg = PoolManager.instance.GetObj(Data.DamageShow);
                dmg.GetComponent<DamageShow>().damageS = damage;
                dmg.transform.position = transform.position + new Vector3(Random.Range(0f,1f),Random.Range(3f,4f),Random.Range(0f,1f));
                dmg.SetActive(true);
            }
        }
    }
    public abstract void DealModelSpawn();
    void Deal(GameObject origin){
        StopMoveInPath();
        GameObject ef = PoolManager.instance.GetObj(Data.Died_Effect);
        ef.transform.position = transform.position;
        ef.SetActive(true);
        DealModelSpawn();
        gameObject.SetActive(false);
        StatisticsBattle.instance.UpdateStatistic(origin,gameObject);
    }


    void CalculatePath(){
        if(seeker.IsDone() && Vector3.Distance(transform.position,target.transform.position) > 5){
            seeker.StartPath(transform.position,
                            new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z),
                            OnPathCallBack);
        }

    }
    void OnPathCallBack(Path p){
        if(p.error) return;
        path = p;
        MoveToTarget();

    }
    void MoveToTarget(){
        if (!gameObject.activeInHierarchy) return;
    
        if(moveCorou != null) StopCoroutine(moveCorou);
        moveCorou = StartCoroutine(MoveToTargetCorou());
    }
    IEnumerator MoveToTargetCorou(){
        int point = 0;
        while(point < path.vectorPath.Count){
            var dir = (path.vectorPath[point]-transform.position).normalized;
            var force = dir * tankCurrent.Speed*.9f * Time.deltaTime;
            transform.position += force;
            var targetNor = (path.vectorPath[point] - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetNor.x,transform.position.y,targetNor.z));
            lowerBody.transform.rotation = Quaternion.Slerp(lowerBody.transform.rotation, targetRotation,tankCurrent.SpeedRotation);

            // rb.velocity = new Vector3(dir.x,rb.velocity.y,dir.z) * tankCurrent.Speed;
            if(Vector3.Distance(transform.position,path.vectorPath[point]) < 0.3f){
                point++;
            }
            yield return null;
        }
    }
    public void StopMoveInPath(){
        if(moveCorou != null) StopCoroutine(moveCorou);
        moveCorou = null;
    }
    public void CheckDirFire()
    {
        Vector3 direction = new Vector3(target.transform.position.x,firePoint.transform.position.y,target.transform.position.z)
                                - firePoint.transform.position;
        float distance = direction.magnitude;

        if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit, distance, obstacleLayer))
        {
            Debug.DrawRay(transform.position, direction.normalized * distance, Color.red);
        }
        else
        {
         Debug.DrawRay(transform.position, direction.normalized * distance, Color.green);
        }
      
    }
    void RunAnimation(){
        anim.SetFloat("velocity",Mathf.Abs(rb.velocity.z + rb.velocity.x));
    }
    void CoolDown(){
        timeFire -= Time.deltaTime;
        timeTakeDmg -= Time.deltaTime;
        timeSkill += Time.deltaTime;

    }
    private void OnTriggerStay(Collider other) {
        if(other.tag == "DeathZone"){
            timeDZ -= Time.deltaTime;
            if(timeDZ<=0){
                TakeDamage(healMax/10,gameObject,transform.position);
                timeDZ = timeTakeDamageDeathZone;
            }
        }
    }

}


