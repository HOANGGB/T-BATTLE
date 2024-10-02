using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListTankUI : MonoBehaviour
{
    [SerializeField] GameObject TankUI;
    [SerializeField] GameObject DetailUI;
    [Header("TANK DETAIL")]
    [SerializeField] Image ImgTank;
    [SerializeField] Image ImgSkill;

    [SerializeField] TextMeshProUGUI NameTank;
    [SerializeField] TextMeshProUGUI HealTank;
    [SerializeField] TextMeshProUGUI DamageTank;
    [SerializeField] TextMeshProUGUI DamageSkillTank;
    [SerializeField] TextMeshProUGUI AttackRangeTank;
    [SerializeField] TextMeshProUGUI SpeedRealoadTank;
    [SerializeField] TextMeshProUGUI SkillRealoadTank;
    [SerializeField] TextMeshProUGUI DescribeSkill;
    [SerializeField] TextMeshProUGUI GemUpgrade;


    [SerializeField] Button SelectTank;
    [SerializeField] Button UpgradeTank;
    [SerializeField] Button BuyTank;
    [SerializeField] TextMeshProUGUI PriceTank;



    


    void Start()
    {
        ShowListTank();
    }
    
    public void ShowListTank(){
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for(int i =0; i< ListTank.instance.AllTank.Count;i++){
            var tank =  ListTank.instance.AllTank[i];
            if(!tank.Status) continue;
            var ui = Instantiate(TankUI,transform);
            var button = ui.transform.Find("TankButton");

            // button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(()=>ClickDetail(tank));

            button.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = tank.TankRarity.ToString();
            var img = ui.transform.Find("ImageTank");
            img.GetComponent<Image>().sprite = tank.Image;


            // button.GetComponent<Button>().onClick.AddListener(()=>ClickDetail(tank));
            // button.GetComponent<Image>().GetComponent<Image>().sprite = tank.Image;
        }
        for(int i =0; i< ListTank.instance.AllTank.Count;i++){
            var tank =  ListTank.instance.AllTank[i];
            if(tank.Status) continue;
            var ui = Instantiate(TankUI,transform);
            var button = ui.transform.Find("TankButton");

            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(()=>ClickDetailBuy(tank));

            button.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = tank.TankRarity.ToString();
            var img = ui.transform.Find("ImageTank");
            img.GetComponent<Image>().sprite = tank.Image;

            ui.transform.Find("ImageTankBackgr").gameObject.SetActive(true);


            // button.GetChild(0).GetComponent<Button>().onClick.AddListener(()=>ClickSelect(tank));
            // button.GetChild(1).GetComponent<Button>().onClick.AddListener(()=>ClickDetail(tank));
            // var img = ui.transform.Find("TankButton");
        }
    }

    void ClickSelect(Tank tank){
        TankCurrent.instance.ChangeTankCurrent(tank);
        PlayerPrefs.SetInt(Data.TankCurrentId,tank.ID);
        Mess("SELECTED TANK");

    }
    void ClickDetail(Tank tank){
        ImgTank.GetComponent<Image>().sprite = tank.Image;
        ImgSkill.GetComponent<Image>().sprite = tank.ImageSkill;
        NameTank.text = tank.Name;
        HealTank.text = tank.heal +" + "+ (tank.heal / 10).ToString();
        DamageTank.text = tank.damage+" + "+ (tank.damage / 10).ToString();
        DamageSkillTank.text = tank.damageSkill+" + "+ (tank.damageSkill / 10).ToString();

        AttackRangeTank.text = tank.ShootingRange.ToString();
        SpeedRealoadTank.text = tank.SpeedReload.ToString();
        SkillRealoadTank.text = tank.SpeedReloadSkill.ToString();
        DescribeSkill.text = tank.Describe;
        GemUpgrade.text = tank.ListPriceUpgrade[tank.Level-1].ToString();

        UpgradeTank.gameObject.SetActive(true);
        SelectTank.gameObject.SetActive(true);
        BuyTank.gameObject.SetActive(false);

        UpgradeTank.onClick.RemoveAllListeners();
        UpgradeTank.onClick.AddListener(()=>UpgradeTankOnclick(tank));

        SelectTank.onClick.RemoveAllListeners();
        SelectTank.onClick.AddListener(()=>ClickSelect(tank));


        DetailUI.SetActive(true);
    }
    void ClickDetailBuy(Tank tank){
        ClickDetail(tank);
        UpgradeTank.gameObject.SetActive(false);
        SelectTank.gameObject.SetActive(false);
        BuyTank.gameObject.SetActive(true);
        PriceTank.text = tank.Pirce.ToString();

        BuyTank.onClick.RemoveAllListeners();
        BuyTank.onClick.AddListener(()=>ClickBuy(tank));
    }
    void ClickBuy(Tank tank){
        if(Accurrent.instance.acc.Gold >= tank.Pirce){
            tank.Status = true;
            Accurrent.instance.acc.Gold -= tank.Pirce;
            PriceUI.instance.UpdatePrice();
            Mess("BUY SUCCESSFULLY");
            ShowListTank();
            ClickDetail(tank);
        }else{
            Mess("NOT ENOUGH GOLD");
        }
    }
    void Mess(string mess){
        
        var messUi = PoolManager.instance.GetObj(Data.MessUITank);
        if(messUi){
            var child =  messUi.transform.Find("BackGr");
            child.GetComponentInChildren<TextMeshProUGUI>().text = mess;
            messUi.transform.SetParent(null);
            messUi.SetActive(true);

        }
    }
    void UpgradeTankOnclick(Tank tank){
        if(Accurrent.instance.acc.Gem >= tank.ListPriceUpgrade[tank.Level-1]){
            Accurrent.instance.acc.Gem -= tank.ListPriceUpgrade[tank.Level-1];
            PriceUI.instance.UpdatePrice();
            tank.Level ++;
            tank.heal += tank.heal / 20;
            tank.damage += tank.damage / 20;
            tank.damageSkill += tank.damageSkill / 20;
            Mess("UPGRADE SUCCESSFULLY");
            ClickDetail(tank);
        }else{
            Mess("NOT ENOUGH GEM");
        }
    }
    










    // void ClickButtonBuy(Tank tank,GameObject ui){
    //     if(PlayerPrefs.GetInt(Data.Gold) >= tank.Pirce){
    //         PlayerPrefs.SetInt(Data.Gold,PlayerPrefs.GetInt(Data.Gold)-tank.Pirce);
    //         tank.Status = true;
    //         ui.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SELECT";
    //         ui.GetComponentInChildren<Button>().onClick.AddListener(()=>ClickButtonSelect(tank,ui));
    //         PriceUI.instance.UpdatePrice(tank.Pirce,0);
    //     }
    // }
    // void ClickButtonSelect(Tank tank, GameObject ui){
    //     TankCurrent.instance.ChangeTankCurrent(tank);
    //     ui.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SELECTED";
    // }

    // public void ShowListTank(){
    //     foreach (Transform child in transform)
    //     {
    //         Destroy(child.gameObject);
    //     }

    //     for(int i =0; i< ListTank.instance.AllTank.Count;i++){
    //         var ui = Instantiate(TankUI);
    //         ui.transform.SetParent(transform);
    //         var tank =  ListTank.instance.AllTank[i];

    //         ui.GetComponentInChildren<TextMeshProUGUI>().text = tank.name;
    //         ui.GetComponentInChildren<Image>().sprite = tank.Image;
    //         if(!tank.Status){
    //             ui.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = tank.Pirce.ToString();
    //             ui.GetComponentInChildren<Button>().onClick.AddListener(() => ClickButtonBuy(tank,ui));
    //         }else{
    //             ui.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SELECT";
    //             ui.GetComponentInChildren<Button>().onClick.AddListener(() => ClickButtonSelect(tank,ui));
    //         }
    //     }
    // }

    // void ClickButtonBuy(Tank tank,GameObject ui){
    //     if(PlayerPrefs.GetInt(Data.Gold) >= tank.Pirce){
    //         PlayerPrefs.SetInt(Data.Gold,PlayerPrefs.GetInt(Data.Gold)-tank.Pirce);
    //         tank.Status = true;
    //         ui.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SELECT";
    //         ui.GetComponentInChildren<Button>().onClick.AddListener(()=>ClickButtonSelect(tank,ui));
    //         PriceUI.instance.UpdatePrice(tank.Pirce,0);
    //     }
    // }
    // void ClickButtonSelect(Tank tank, GameObject ui){
    //     TankCurrent.instance.ChangeTankCurrent(tank);
    //     ui.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "SELECTED";
    // }
}
