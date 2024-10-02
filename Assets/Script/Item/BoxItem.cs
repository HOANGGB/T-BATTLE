
using UnityEngine;
using UnityEngine.UI;

public class BoxItem : MonoBehaviour
{
    [SerializeField] int healMax;
    [SerializeField] int healCurrent;
    [SerializeField] Image heal;
    [SerializeField] Image healDelay;
    [SerializeField] GameObject ui;

    float timeTakeDmg;
    void Start()
    {
        healCurrent = healMax;
              
    }
    void Update()
    {
        timeTakeDmg -= Time.deltaTime;
        ui.transform.LookAt(ui.transform.position + Camera.main.transform.forward);
        if(heal.fillAmount != healDelay.fillAmount){
            healDelay.fillAmount = Mathf.Lerp(healDelay.fillAmount,heal.fillAmount,.01f);
        }
    }
    public void TakeDamage(int damage){
        if(timeTakeDmg<=0){
            timeTakeDmg = 0.05f;
            healCurrent  -= damage;
            heal.fillAmount = (float)healCurrent/healMax;
            if(healCurrent<=0) RandomItem();
            else{
                var dmg = PoolManager.instance.GetObj(Data.DamageShow);
                dmg.GetComponent<DamageShow>().damageS = damage;
                dmg.transform.position = transform.position + new Vector3(Random.Range(0f,1f),Random.Range(3f,4f),Random.Range(0f,1f));
                dmg.SetActive(true);
            }
        }
    }
    void RandomItem(){
        GameObject breaking = PoolManager.instance.GetObj(Data.BoxItemBreaking);
        breaking.transform.position = transform.position;
        breaking.SetActive(true);

        int rd = Random.Range(0,2);
        GameObject item = PoolManager.instance.GetObj(Data.ItemAttack);;
        if(rd == 0) item = PoolManager.instance.GetObj(Data.ItemHeal);
        item.transform.position = transform.position + Vector3.up + Vector3.right * 3;
        item.SetActive(true);
        gameObject.SetActive(false);
    }
   
}
