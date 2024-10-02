using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UITank : MonoBehaviour
{
    // Start is called before the first frame update
    public Image reload;
    public Image heal;
    public Image healDelay;


    public TextMeshProUGUI numberHeal;

    TankPlayer tankPlayer;
    float timeReload;

    void Start()
    {
        tankPlayer = GetComponentInParent<TankPlayer>();
        timeReload = 0;
        tankPlayer.healMax = tankPlayer.tankCurrent.heal;
        UpdateHealBar(tankPlayer.tankCurrent.heal,tankPlayer.tankCurrent.heal);
    }

    // Update is called once per frame
    void Update()
    { 
        if(SceneManager.GetActiveScene().buildIndex == 0){
            transform.GetChild(0).gameObject.SetActive(false);
        }else{
            transform.GetChild(0).gameObject.SetActive(true);
            timeReload += Time.deltaTime;
            UpdateMagazineReload();
        }
        if(heal.fillAmount != healDelay.fillAmount){
            healDelay.fillAmount = Mathf.Lerp(healDelay.fillAmount,heal.fillAmount,.01f);
        }

    }
    
    public void UpdateHealBar(int healChange, int healMax){
        numberHeal.text = healChange.ToString();
        heal.fillAmount = (float)healChange/healMax;

        healDelay.fillAmount = Mathf.Lerp(healDelay.fillAmount,heal.fillAmount,1f);
    }
    public void ShowReloadMagazine(){
        timeReload = 0;
        reload.gameObject.SetActive(true);
    }
    void UpdateMagazineReload(){
        if(timeReload >= tankPlayer.tankCurrent.SpeedReload)
        {
            reload.gameObject.SetActive(false);
            return;
        }
        reload.gameObject.SetActive(true);
        reload.fillAmount = timeReload / tankPlayer.tankCurrent.SpeedReload;
    }
}
