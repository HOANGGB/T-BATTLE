using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MessKill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowMessKill(GameObject killer,GameObject killed){
        GameObject messShow = null;
        foreach(Transform mess in transform){
            if(!mess.gameObject.activeInHierarchy){
                messShow = mess.gameObject;
                Debug.Log("NAME = "+messShow.name);
                break;
            }
        }
        if(messShow){
            messShow.SetActive(true);
            messShow.transform.Find("Killed").GetComponent<TextMeshProUGUI>().text = killed.name;
            if(killer == null) messShow.transform.Find("Killer").GetComponent<TextMeshProUGUI>().text = "X";
            else messShow.transform.Find("Killer").GetComponent<TextMeshProUGUI>().text = killer.name;
            Debug.Log("SHOW MESSS");
        }

        // var ui = PoolManager.instance.GetObj(Data.ShowMessKill);
        // ui.transform.SetParent(transform);
        // ui.transform.Find("Killed").GetComponent<TextMeshProUGUI>().text = killed.name;
        // if(killer == null) ui.transform.Find("Killer").GetComponent<TextMeshProUGUI>().text = "X";
        // else ui.transform.Find("Killer").GetComponent<TextMeshProUGUI>().text = killer.name;
       
        // ui.SetActive(true);

    }
}
