
using System.Collections;
using UnityEngine;

public class TankCurrent : MonoBehaviour
{
    public static TankCurrent instance;
    public Tank tankCurrent;
    [SerializeField] float speedRotate;
    private void Awake() {
        instance = this;
    }
    void Start(){
        if(!PlayerPrefs.HasKey(Data.TankCurrentId)){
            ChangeTankCurrent(ListTank.instance.AllTank[0]);
        }else{
            ChangeTankCurrent(ListTank.instance.AllTank[PlayerPrefs.GetInt(Data.TankCurrentId)]);
        }
    }
    void Update()
    {
        // transform.Rotate(Vector3.up * speedRotate * Time.deltaTime);
    }
    public void ChangeTankCurrent(Tank Tank){
        foreach(Transform child in transform){
            if(child.gameObject.tag =="TankPlayer"){
                Destroy(child.gameObject);
                Debug.Log("DESTROY NEEE + "+ child.gameObject.name);
            }
        }
        StartCoroutine(SetPosTank(Tank));
       
    }
    IEnumerator SetPosTank(Tank Tank){
        yield return new WaitForSeconds(.1f);
        var tank = Instantiate(Tank.OBJ_Tank);
        tank.transform.SetParent(transform);
        tank.GetComponent<Rigidbody>().isKinematic = true;
        tank.transform.localPosition =  Vector3.zero;
        tank.transform.localRotation = Quaternion.Euler(0,0,0);
        yield return new WaitForSeconds(.1f);
        tank.GetComponent<Rigidbody>().isKinematic = false;
    }
}
