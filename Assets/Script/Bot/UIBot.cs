
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image healImg;
    [SerializeField] Image healDelay;
    [SerializeField ] TextMeshProUGUI numberHeal;
    Bot bot;



    void Start()
    {
        bot = GetComponentInParent<Bot>();
        UpdateHeal(bot.heal,bot.heal);
    }
    void Update()
    {
        if(healImg.fillAmount != healDelay.fillAmount){
            healDelay.fillAmount = Mathf.Lerp(healDelay.fillAmount,healImg.fillAmount,.01f);
        }
     }

    // Update is called once per frame
    public void UpdateHeal(int heal,int healMax)
    {
        numberHeal.text  = heal.ToString();
        healImg.fillAmount = (float)heal/healMax;
    }
}
