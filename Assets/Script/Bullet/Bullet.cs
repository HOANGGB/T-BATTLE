using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public GameObject originTank;
    public Rigidbody rb;
    public int damage = 1;
    public Coroutine coroutine;
    public float v = 1;
    public float s = 1;
    int Count = 0;
    public GameObject hitEffect;
    public GameObject noHitEffect;

    void OnEnable()
    {
        Count++;
        if(Count >1){
            OnEnableThis();
        }
    }
    public abstract void OnEnableThis();


    private void OnTriggerEnter(Collider other) {
        if(other.tag == "TankPlayer" || other.tag == "Bot" || other.tag == "Ground" || other.tag == "Box" || other.tag == "Wall"  || other.tag == "BoxItem"){
            if(other.tag == "TankPlayer"){
                other.GetComponent<TankPlayer>().TakeDamage(damage,originTank,transform.position);
            }else if(other.tag == "Bot"){
                other.GetComponent<Bot>().TakeDamage(damage,originTank,transform.position);
            }else if(other.tag == "BoxItem"){
                other.GetComponent<BoxItem>().TakeDamage(damage);
            } 
            else if(other.tag == "Box"){
                other.GetComponent<Box>().TakeDamage(damage,transform.position);
            } 
            hitEffect.transform.position = transform.position;
            hitEffect.SetActive(true);
            
            gameObject.SetActive(false);
        }
    }

}

