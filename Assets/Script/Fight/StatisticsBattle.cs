
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatisticsBattle : MonoBehaviour
{
    public static StatisticsBattle instance;
    [Header("STATISTICS BATTLNG")] 
    public int kill, alive;
    public GameObject DeathZone;
    [SerializeField] TextMeshProUGUI killNumber, aliveNumber;

    [Header("STATISTICS RESULT")] 
    [SerializeField] TextMeshProUGUI killNumberResult, goldReward, top,cupNumber;

    [SerializeField] MessKill messKill;
    public Animator anim;
    void Awake(){
        instance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        kill = 0;
    }
    public void AliveCount(){
        GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");
        alive += bots.Length;
        GameObject[] tank = GameObject.FindGameObjectsWithTag("TankPlayer");
        alive += tank.Length;
        killNumber.text = kill.ToString();
        aliveNumber.text = alive.ToString();  
        DeathZone.SetActive(true);
    }

    public void UpdateStatistic(GameObject killer, GameObject killed)
    {
        alive--;
        if(killer !=null) if(killer.tag == "TankPlayer") kill++;
        killNumber.text = kill.ToString();
        aliveNumber.text = alive.ToString();
        messKill.ShowMessKill(killer,killed);
        if(killed.tag =="TankPlayer"){
            anim.SetTrigger("Died");
            LoadStatistics();
        }
        if(alive <=1){
            anim.SetTrigger("Complete");
            LoadStatistics();
        }
    }
    public void LoadStatistics(){
        top.text ="Top "+  alive.ToString(); 
        killNumberResult.text ="Kill : "+  kill.ToString();
        int goldRewardNumber = (alive == 1 ? 50 : (11 - alive) * 4) + kill;
        goldReward.text ="Gold + "+  goldRewardNumber.ToString();
        Accurrent.instance.acc.Gold += goldRewardNumber;
        if(alive == 1) Accurrent.instance.acc.Gem++;


        int rs = (alive == 1 ? 10 : 11 - alive) + kill;
        Accurrent.instance.acc.CupNumber += rs;
        cupNumber.text = "Cup + "+  rs.ToString();
    }
    public void BackHome(){
        SceneManager.LoadScene("Home");
        UIManager.instance.anim.SetTrigger("Home");
    }
   
}
