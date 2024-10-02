using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxItemBreaking : MonoBehaviour
{
    List<GameObject> child = new List<GameObject>();
    List<Vector3> listPos = new List<Vector3>();
    List<Vector3> listRota = new List<Vector3>();

    List<Rigidbody> listRb  = new List<Rigidbody>();
    Coroutine coroutine;

     private void Awake() {
        Add();
    }
    void Start()
    {
        foreach(Rigidbody rb in listRb){
            rb.velocity = new Vector3(Random.Range(-10,5),Random.Range(1,5),Random.Range(-5,10));
        }
        coroutine = null;
        coroutine = StartCoroutine(MoveDownAndHide());
    }
    void Add(){
        foreach(Transform i in transform){
            child.Add(i.gameObject);
            listPos.Add(i.localPosition);
            listRota.Add(i.localEulerAngles);
            listRb.Add(i.GetComponent<Rigidbody>());
        }
    }
    private void OnEnable() {
        foreach(Rigidbody rb in listRb){
            rb.isKinematic = false;
            rb.velocity = new Vector3(Random.Range(-10,5),Random.Range(1,5),Random.Range(-5,10));
        }
        coroutine = null;
        coroutine = StartCoroutine(MoveDownAndHide());
        
    }
    private void OnDisable() {
        for(int i = 0; i<child.Count;i++){
            child[i].transform.localPosition = listPos[i];
            child[i].transform.rotation = Quaternion.Euler(listRota[i]);
            child[i].GetComponent<MeshCollider>().isTrigger = false;
            child[i].GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator MoveDownAndHide(){
        yield return new WaitForSeconds(2);
        foreach(GameObject child in child){
            child.GetComponent<MeshCollider>().isTrigger = true;
        }
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
