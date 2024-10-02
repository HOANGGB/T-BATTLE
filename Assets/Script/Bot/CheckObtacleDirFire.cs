
using UnityEngine;

public class CheckObtacleDirFire : MonoBehaviour
{
    [SerializeField] LayerMask layerRadaCheck;   
    [SerializeField] Bot bot;
    void Start()
    {
        
    }
    void Update(){
        CheckDirFire();
    }

   void CheckDirFire(){
        RaycastHit hit;
        bool obj = Physics.Raycast(transform.position, transform.forward, out hit, bot.tankCurrent.ShootingRange, layerRadaCheck);

        if(obj) Debug.DrawRay(transform.position, hit.point - transform.position, Color.red);
        else  Debug.DrawRay(transform.position, transform.forward * bot.tankCurrent.ShootingRange, Color.green);

        if(obj){
            if(hit.collider.gameObject.tag == "BoxItem" || hit.collider.gameObject.tag == "TankPlayer")  bot.canFire = true;
            else bot.canFire = false;
        }else  bot.canFire = true;
        
   }

    
}
