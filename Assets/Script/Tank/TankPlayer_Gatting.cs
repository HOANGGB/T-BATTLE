using System.Collections;
using UnityEngine;

public class TankPlayer_Gatting : TankPlayer
{
  
    [SerializeField] float ShootingRangeSkill;
    [SerializeField] bool isFire;
    [SerializeField] bool isSkillTime;
    Coroutine coroutine;


    public override void Fire()
    {
        if(joystickFire.Horizontal != 0 || joystickFire.Vertical!= 0){
            DragFire();
        }else if(joystickFire.Horizontal == 0 || joystickFire.Vertical == 0){
            DropFire();
        }

    }
    void DragFire(){
            dirFireBar.gameObject.SetActive(true);
        dir = new Vector2(joystickFire.Horizontal,joystickFire.Vertical);
        if(!isTap){
            isTap = true;
        }
    }
     void DropFire(){
            dirFireBar.gameObject.SetActive(false);
        StartFire();
    }
    void StartFire(){
        if(isTap && !isFire){
            if(isTapSkill){
                if(coroutine == null){
                    anim.SetBool("Skill",true);
                    coroutine =  StartCoroutine(SeriesSkill());
                }
            }
            else if(timeFire <=0 && !isFire && !isTapSkill){
                anim.SetTrigger("Fire");
                StartCoroutine(SeriesFire());
            }
        }
    }
    IEnumerator SeriesFire(){
        isFire = true;
        for(int i =0; i<3; i++){
                var bullet =  PoolManager.instance.GetObj(Data.Gatting_Bullet);
                if(bullet != null){
                    bullet.GetComponent<Bullet>().originTank = gameObject;
                    bullet.GetComponent<Bullet>().v = tankCurrent.SpeedBullet;
                    bullet.GetComponent<Bullet>().s = tankCurrent.ShootingRange;
                    bullet.GetComponent<Bullet>().damage = tankCurrent.damage + damageBonus;
                    bullet.GetComponent<Bullet>().hitEffect = PoolManager.instance.GetObj(Data.HitGatting_Effect);
                    bullet.GetComponent<Bullet>().noHitEffect = PoolManager.instance.GetObj(Data.NoHitGatting_Effect);

                    tankSound.PlayFire(tankCurrent.audioFire);
                    fireEffect.Play();

                    bullet.transform.position = bulletPoint.position;
                    bullet.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.y));
                    bullet.GetComponent<Rigidbody>().velocity = new Vector3(dir.x,0,dir.y).normalized * tankCurrent.SpeedBullet;
                    bullet.SetActive(true);
                }
                yield return new WaitForSeconds(.1f);
            }
        isTap = false;

        isFire = false;
        timeFire = tankCurrent.SpeedReload;
        uITank.ShowReloadMagazine();
    }
    IEnumerator SeriesSkill(){
        isFire = true;
        while(timeSkill <3){
            var bullet =  PoolManager.instance.GetObj(Data.Gatting_BulletSkill);
                if(bullet != null){
                    bullet.GetComponent<Bullet>().originTank = gameObject;
                    bullet.GetComponent<Bullet>().v = tankCurrent.SpeedBullet;
                    bullet.GetComponent<Bullet>().s = tankCurrent.ShootingRange;
                    bullet.GetComponent<Bullet>().damage = tankCurrent.damage  + damageBonus;
                    bullet.GetComponent<Bullet>().hitEffect = PoolManager.instance.GetObj(Data.HitGatting_Effect);
                    bullet.GetComponent<Bullet>().noHitEffect = PoolManager.instance.GetObj(Data.NoHitGatting_Effect);

                    tankSound.PlayFire(tankCurrent.audioSkill);
                    fireEffect.Play();
                    
                    bullet.transform.position = bulletPoint.position;
                    bullet.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.y));
                    bullet.GetComponent<Rigidbody>().velocity = new Vector3(dir.x,0,dir.y).normalized * tankCurrent.SpeedBullet;
                    bullet.SetActive(true);
                    yield return new WaitForSeconds(.1f);
                }
        }
        isFire = false;
        isTap = false;
        isTapSkill = false;
        coroutine = null;
        uITank.ShowReloadMagazine();
        timeFire = tankCurrent.SpeedReload;
        anim.SetBool("Skill",false);
    }

    


    public override void Skill()
    {
        if(timeSkill >=tankCurrent.SpeedReloadSkill){
            isTapSkill = true;
            isTap = true;
            timeSkill = 0;
        }
    }

}
