using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RadaBot : MonoBehaviour
{

    [SerializeField] float disRada, speedRotate, timeScan, timeCount, distanceRada = 30;
    [SerializeField] LayerMask layerRadaCheck;
    
    Transform centerMapPoint;
    Bot bot;

    void Start()
    {
        bot = GetComponentInParent<Bot>();
        centerMapPoint = GameObject.Find("CenterMapPoint").transform;
        
    }

    void Update()
    {
        RadaCheck();
        CoolDown();
    }

    void RadaCheck(){
        RaycastHit hit;
        bool obj = Physics.Raycast(transform.position, transform.forward, out hit, distanceRada, layerRadaCheck);

        ///DRAW RAY
        if(obj) Debug.DrawRay(transform.position, hit.point - transform.position, Color.red);
        else  Debug.DrawRay(transform.position, transform.forward * distanceRada, Color.green);
        //END
        if (obj && timeCount<=0){
            if(hit.collider.gameObject.tag == "TankPlayer" || hit.collider.gameObject.tag == "Bot"){
                if(bot.target.tag == "TankPlayer" || bot.target.tag == "Bot"){
                    float disTargetCurrent = Vector3.Distance(transform.position, bot.target.transform.position);
                    float disTargetNew = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);
                    if(disTargetCurrent > disTargetNew){
                         bot.target = hit.collider.gameObject;
                        timeCount = timeScan;
                    }
                }else{
                    bot.target = hit.collider.gameObject;
                    timeCount = timeScan;
                }
            }else{
                float disTargetCurrent = Vector3.Distance(transform.position, bot.target.transform.position);
                float disTargetNew = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);
                if(disTargetCurrent > disTargetNew){
                    bot.target = hit.collider.gameObject;
                    timeCount = timeScan;
                }
            }
        }
        
        if(!bot.target.activeInHierarchy || bot.target == null){
                bot.target = centerMapPoint.gameObject;
                timeCount = timeScan;
        }

        transform.Rotate(Vector3.up * speedRotate * Time.deltaTime );

    }

    void CoolDown(){
        timeCount -= Time.deltaTime;
    }
}
