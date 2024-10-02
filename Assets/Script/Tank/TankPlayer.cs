using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class TankPlayer : MonoBehaviour
{
     public static TankPlayer instance;

    private void Awake(){
        instance = this;
    }
    public int healCurrent;
    public int healMax;

    public int damageBonus;

    public Tank tankCurrent;
    public TankSound tankSound;
    public Joystick joystickMove,joystickFire;
    public Transform bulletPoint;
    public GameObject dirFireBar;
    public ParticleSystem fireEffect;
    public LayerMask layerCheck;
    [HideInInspector]  public float horizontal,vertical,timeFire,timeSkill,timeTakeDmg,timeDZ,timeTakeDamageDeathZone = .5f;

    [HideInInspector]  public bool  isTap, isTapSkill = false;

    [HideInInspector] public GameObject UpperBody,LowerBody;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public UITank uITank;

    [HideInInspector] public Vector2 dir;
    [HideInInspector] public LineRenderer LineDirFireBar;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        tankSound = GetComponent<TankSound>();
        UpperBody = transform.GetChild(0).gameObject;
        LowerBody = transform.GetChild(1).gameObject;
        uITank = GetComponentInChildren<UITank>();
        LineDirFireBar = dirFireBar.GetComponent<LineRenderer>();
        healCurrent = tankCurrent.heal;
        healMax = healCurrent;

        timeFire = tankCurrent.SpeedReload;
        timeSkill = 0;
        dirFireBar.gameObject.SetActive(false);
         rb.drag = 3f;
        // joystickMove = GameObject.FindWithTag("JoystickMove").GetComponent<Joystick>();
        // joystickFire = GameObject.FindWithTag("JoystickFire").GetComponent<Joystick>();



    }
    // void Assign(){
    //     var ct =  GameObject.Find("Control");
    //     if(ct != null){
    //         joystickMove = GameObject.Find("Joystick Move").GetComponent<Joystick>();
    //         joystickFire = GameObject.Find("Joystick Fire").GetComponent<Joystick>();
    //     }
    // }

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) return;
        if(!joystickMove || !joystickFire) return;
            Move();
            Fire();
             RotateTank();
        LineDir();
        CoolDown();
        RunAnimation();

    }

    void Move(){
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0){
            Vector3 dirMove = new Vector3(horizontal,0,vertical).normalized;
            rb.velocity = dirMove * tankCurrent.Speed;
        }
        

         if(joystickMove.Horizontal != 0 || joystickMove.Vertical != 0){
            Vector3 dirMove = new Vector3(joystickMove.Horizontal,0,joystickMove.Vertical).normalized;
            rb.velocity = dirMove * tankCurrent.Speed;
        }

    }

    public abstract void Fire();

    public abstract void Skill();

    void RotateTank(){
        //UPPER BODY
        if(joystickFire.Horizontal != 0 || joystickFire.Vertical!= 0){
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(joystickFire.Horizontal,0,joystickFire.Vertical));
            UpperBody.transform.rotation = Quaternion.Slerp(UpperBody.transform.rotation, targetRotation,tankCurrent.SpeedRotation * 2);
        }


        //LOWER BODY
        if(horizontal != 0 || vertical != 0){
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(horizontal,0,vertical));
            LowerBody.transform.rotation = Quaternion.Slerp(LowerBody.transform.rotation, targetRotation,tankCurrent.SpeedRotation);
        }
        if(joystickMove.Horizontal != 0 || joystickMove.Vertical != 0){
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(joystickMove.Horizontal,0,joystickMove.Vertical));
            LowerBody.transform.rotation = Quaternion.Slerp(LowerBody.transform.rotation, targetRotation,tankCurrent.SpeedRotation);
        }


    }
    void LineDir(){
        RaycastHit hit;
        bool obj = Physics.Raycast(dirFireBar.transform.position,new Vector3(dir.x,0,dir.y), out hit, tankCurrent.ShootingRange, layerCheck);


        if(obj) Debug.DrawRay(dirFireBar.transform.position, hit.point - dirFireBar.transform.position, Color.red);
        else  Debug.DrawRay(dirFireBar.transform.position, dir * tankCurrent.ShootingRange, Color.green);

            LineDirFireBar.SetPosition(0, dirFireBar.transform.position);
            LineDirFireBar.startWidth = tankCurrent.WidthRange;
            LineDirFireBar.endWidth = tankCurrent.WidthRange;

        if(obj){
            LineDirFireBar.SetPosition(1,hit.point);
        }else{
            LineDirFireBar.SetPosition(1,dirFireBar.transform.position + new Vector3(dir.x,0,dir.y).normalized * tankCurrent.ShootingRange);
        }


    }
    public void TakeDamage(int damage, GameObject origin, Vector3  pos){
        if(timeTakeDmg<=0){
            timeTakeDmg = .1f;
            healCurrent  -= damage;
            uITank.UpdateHealBar(healCurrent,healMax);
            if(healCurrent<=0) Deal(origin);
            else{
                var dmg = PoolManager.instance.GetObj(Data.DamageShow);
                dmg.GetComponent<DamageShow>().damageS = damage;
                dmg.transform.position = transform.position + new Vector3(Random.Range(0f,1f),Random.Range(3f,4f),Random.Range(0f,1f));
                dmg.SetActive(true);
            }
        }
    }
    void Deal(GameObject origin){
        
        gameObject.SetActive(false);
        GameObject ef = PoolManager.instance.GetObj(Data.Died_Effect);
        ef.transform.position = transform.position;
        ef.SetActive(true);
        StatisticsBattle.instance.UpdateStatistic(origin,gameObject);
    }
    void CoolDown(){
        timeFire -= Time.deltaTime;
        timeTakeDmg -= Time.deltaTime;
        timeSkill += Time.deltaTime;


    }
    void RunAnimation(){
        anim.SetFloat("velocity",Mathf.Abs(rb.velocity.z + rb.velocity.x));
    }
   
     private void OnTriggerStay(Collider other) {
        if(other.tag == "DeathZone"){
            timeDZ -= Time.deltaTime;
            if(timeDZ<=0){
                TakeDamage(healMax/10,null,transform.position);
                timeDZ = timeTakeDamageDeathZone;
            }
        }
    }
}
