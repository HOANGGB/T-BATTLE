using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PriceUI : MonoBehaviour
{
    public static PriceUI instance;
    [SerializeField] TextMeshProUGUI Gold;
    [SerializeField] TextMeshProUGUI Gem;
    void Awake(){
        instance = this;
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => { UpdatePrice(); };

    }        
    void OnDestroy()=>SceneManager.sceneLoaded -= (Scene scene, LoadSceneMode mode) => { UpdatePrice(); };
    
    void Start()
    {
        UpdatePrice();
    }

    // Update is called once per frame
    public void UpdatePrice()
    {

        this.Gold.text = Accurrent.instance.acc.Gold.ToString();
        this.Gem.text = Accurrent.instance.acc.Gem.ToString();
    }
}
