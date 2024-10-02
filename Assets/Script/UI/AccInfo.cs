using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccInfo : MonoBehaviour
{
    [SerializeField] Account acc;
    [Header("INFO")]
    [SerializeField] Image AvataAcc;
    [SerializeField] TextMeshProUGUI NameAcc;
    [SerializeField] TextMeshProUGUI CupAcc;

    [Header("INFO ORTHER")]
    [SerializeField] Transform listInforOther;

    void Awake(){

        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => { LoadInfo(); };
    }
    void OnDestroy()=>SceneManager.sceneLoaded -= (Scene scene, LoadSceneMode mode) => { LoadInfo(); };
    void Start()
    {
        LoadInfo();
    }


    // Update is called once per frame
    public void LoadInfo()
    {
        AvataAcc.sprite = Accurrent.instance.acc.AvataImg;
        NameAcc.text = Accurrent.instance.acc.Name;
        CupAcc.text = Accurrent.instance.acc.CupNumber.ToString();

        foreach(var tank in ListTank.instance.AllTank){
            if(tank.Status){
                Debug.Log("trueeeeeeee");
                foreach(Transform it in listInforOther){
                    if(!it.gameObject.activeInHierarchy){
                        Debug.Log("int ++++++");
                        var ii = it.GetChild(0).gameObject;
                        ii.transform.GetChild(0).GetComponent<Image>().sprite = tank.Image;
                        ii.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tank.Name;
                        ii.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text ="Lv " +tank.Level.ToString();

                        it.gameObject.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
}
