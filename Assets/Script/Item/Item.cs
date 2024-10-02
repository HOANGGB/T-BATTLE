using UnityEngine;

public abstract class Item : MonoBehaviour
{
    Animator anim;
 

    void Awake(){
        anim = GetComponent<Animator>();


    }
    private void OnEnable() {
        anim.SetTrigger("Spawn");
    }
    private void OnDisable(){
        anim.SetTrigger("off");
    }
    public abstract void TakeByPlayer(GameObject player);
    public abstract void TakeByBot(GameObject bot);



    private void OnTriggerEnter(Collider other) {
        if(other.tag == "TankPlayer"){
            TakeByPlayer(other.gameObject);
            gameObject.SetActive(false);
        }else if(other.tag == "Bot"){
            TakeByBot(other.gameObject);
            gameObject.SetActive(false);
        }
    }
     
}
