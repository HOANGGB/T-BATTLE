
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SaveName : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_InputField inputName;
    [SerializeField] Button saveButton;
    [SerializeField] TextMeshProUGUI messError;
    void Start(){
        if(PlayerPrefs.HasKey("NameAccouunt")) Destroy(gameObject);
    }
    public void Save()
    {
       PlayerPrefs.SetString("NameAccouunt","ok");
       Accurrent.instance.acc.Name = inputName.text;
    }
    public void OK(){
       Destroy(gameObject);
    }
     public void Change()
    {
       if(!string.IsNullOrEmpty(inputName.text) && inputName.text.Length > 2) {
            saveButton.interactable = true;
            messError.text = "";

       }else{
            saveButton.interactable = false;
            messError.text = "Character greater than 2";
       }
    }

}
