using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartFight : MonoBehaviour
{
    [SerializeField] Button fightButton;
    void Start(){
        fightButton.onClick.AddListener(()=>SurvivalMode());
    }
    public void SelectMode(int i){

            fightButton.onClick.RemoveAllListeners();
            Debug.Log("dddddddđ");
        if(i == 0){
            fightButton.onClick.AddListener(()=>SurvivalMode());
            
        }else if(i ==1){
            fightButton.onClick.AddListener(()=>SoloMode());
        }
        Debug.Log("i === "+i);
    }
    void  SurvivalMode(){
        TankPlayer.instance.gameObject.transform.SetParent(null);
        DontDestroyOnLoad(TankPlayer.instance.gameObject);
        int rd = Random.Range(1,4);
        SceneManager.LoadScene(rd);
    }
    void SoloMode(){
        TankPlayer.instance.gameObject.transform.SetParent(null);
        DontDestroyOnLoad(TankPlayer.instance.gameObject);
        SceneManager.LoadScene(4);

    }
}
