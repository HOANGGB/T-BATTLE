using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{

    [SerializeField] int number;
    [SerializeField] int healCurrent;
    int numberHit;

    float timeTakeDmg;
    void Start()
    {              
    }
    void Update()
    {
        timeTakeDmg -= Time.deltaTime;
    }
    public void TakeDamage(int damage,Vector3 pos){
        if(timeTakeDmg<=0){
            numberHit++;
            timeTakeDmg = 0.05f;
            healCurrent  -= damage;
            if(healCurrent<=0 || numberHit >= 5) RandomItem();
        }
    }
    void RandomItem(){
        GameObject breaking = PoolManager.instance.GetObj(Data.BoxBreaking +number+"(Clone)");
        breaking.transform.position = transform.position;
        breaking.SetActive(true);

        int rd = Random.Range(0,2);
        GameObject item = PoolManager.instance.GetObj(Data.ItemAttack);
        if(rd == 0) item = PoolManager.instance.GetObj(Data.ItemHeal);
        item.transform.position = transform.position+ Vector3.up *2;
        item.transform.position = transform.position + Vector3.up + Vector3.right * 3;
        item.SetActive(true);
        gameObject.SetActive(false);
    }
   
}
