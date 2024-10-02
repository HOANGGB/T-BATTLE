using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoOnclickSkill : MonoBehaviour
{
    [SerializeField] Image imgSkill;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource au;

    void Start(){
        ChangeImgSkill();
    }
    void Update(){
        if(SceneManager.GetActiveScene().buildIndex != 0){
            imgSkill.fillAmount = TankPlayer.instance.timeSkill / TankPlayer.instance.tankCurrent.SpeedReloadSkill;
            if(!anim){
                anim = GetComponent<Animator>();
            }else anim.SetFloat("coolDown",imgSkill.fillAmount);
           
        }
    }
    public void ChangeImgSkill(){
        if(!imgSkill) imgSkill = GetComponent<Image>();
        imgSkill.sprite = TankPlayer.instance.tankCurrent.ImageSkill;
    }
    public void UseSkillOnclick(){
        TankPlayer.instance.Skill();
    }
    public void PlaySound(){
        au.Play();
    }
}
