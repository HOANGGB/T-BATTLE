using System.Collections;
using System.Collections.Generic;
using Dan.Main;
using TMPro;
using UnityEngine;

public class Ranking : MonoBehaviour
{
   public List<TextMeshProUGUI> Top;
   public List<TextMeshProUGUI> Names;
    public List<TextMeshProUGUI> Score;
    public string publicKey = "903c58902f0ff259496035d6734a6ed3db322b83405d63d3bfbb35b302ede160";

    void Start()
    {
        if(PlayerPrefs.HasKey("NameAccouunt")){
            SetEntry(Accurrent.instance.acc.Name,Accurrent.instance.acc.CupNumber);
        }
    }

    // Update is called once per frame
    public void LoadEntries()
    {
        Leaderboards.GameLeaderboard.GetEntries(entries =>{
            foreach(TextMeshProUGUI top in Top){
                top.text = "";
             }
             foreach(TextMeshProUGUI name in Names){
                name.text = "";
             }
             foreach(TextMeshProUGUI score in Score){
                score.text = "";
             }

             float lenght = Mathf.Min(Names.Count,entries.Length);
             for(int i=0; i<lenght; i++){
                Top[i].text = (i + 1).ToString();
                Names[i].text = entries[i].Username;
                Score[i].text = entries[i].Score.ToString();
             }
        });
    }
    public void SetEntry(string name, int score)
    {
        Leaderboards.GameLeaderboard.UploadNewEntry(name,score,isSuccessfull =>{
            if(isSuccessfull){
                LoadEntries();
            }
        });
    }
}
