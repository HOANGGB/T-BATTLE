using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTank : MonoBehaviour
{
    [SerializeField] List<Transform> ListPosSpawn = new List<Transform>();
    [SerializeField] List<GameObject> ListBot = new List<GameObject>();
    void Start()
    {
        foreach(Transform pos in transform){
            ListPosSpawn.Add(pos);
        }
        StartCoroutine(RandomPosForTank());
    }
    void Update(){
        // if(Input.GetKeyDown(KeyCode.R)){
        //     RandomPosForTank();
        // }
    }
    

    IEnumerator RandomPosForTank()
    {
        yield return new WaitForSeconds(2f);
        Rigidbody rb = TankPlayer.instance.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;  // Tạm thời tắt vật lý

        int i =  Random.Range(0,ListPosSpawn.Count);
        TankPlayer.instance.gameObject.transform.position =  ListPosSpawn[i].position;
        TankPlayer.instance.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(1f);
        for(int a=0; a<ListPosSpawn.Count;a++){
            if(a == i) continue;
            Instantiate(ListBot[Random.Range(0,ListBot.Count)],ListPosSpawn[a].position,Quaternion.identity);
        }

        yield return new WaitForSeconds(1);
        rb.isKinematic = false; 
        StatisticsBattle.instance.AliveCount();

    }
    
}
