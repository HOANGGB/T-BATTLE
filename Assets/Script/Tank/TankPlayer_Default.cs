using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayer_Default : TankPlayer
{

    [SerializeField] float ShootingRangeSkill;
    public override void Fire()
    {
        if(joystickFire.Horizontal != 0 || joystickFire.Vertical!= 0){
            DragFire();
        }else if(joystickFire.Horizontal == 0 || joystickFire.Vertical == 0){
            DropFire();

        }
    }
    void DragFire(){
        dir = new Vector2(joystickFire.Horizontal,joystickFire.Vertical);
        dirFireBar.gameObject.SetActive(true);
        if(!isTap){
            isTap = true;
        }
    }
     void DropFire(){
        dirFireBar.gameObject.SetActive(false);
        StartFire();
    }
    void StartFire(){
        if(isTap){
            isTap = false;

        if(isTapSkill){
            isTapSkill = false;
            var bullet =  PoolManager.instance.GetObj(Data.Default_BulletSkill);
            if(bullet != null){
                bullet.GetComponent<Bullet>().originTank = gameObject;
                bullet.GetComponent<Bullet>().v = tankCurrent.SpeedBullet;
                bullet.GetComponent<Bullet>().s = tankCurrent.ShootingRange;
                bullet.GetComponent<Bullet>().damage = tankCurrent.damage * 3 + damageBonus;
                bullet.GetComponent<Bullet>().hitEffect = PoolManager.instance.GetObj(Data.HitDefault_Effect);
                bullet.GetComponent<Bullet>().noHitEffect = PoolManager.instance.GetObj(Data.NoHitDefault_Effect);
                tankSound.PlayFire(tankCurrent.audioSkill);
                fireEffect.Play();
                anim.SetTrigger("Skill");
                bullet.transform.position = bulletPoint.position;
                bullet.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.y));
                bullet.GetComponent<Rigidbody>().velocity = new Vector3(dir.x,0,dir.y).normalized * tankCurrent.SpeedBullet;
                bullet.SetActive(true);
                uITank.ShowReloadMagazine();
                timeSkill = 0;
                timeFire = tankCurrent.SpeedReload;
                return;
            }
        }
        else if(timeFire <=0){
            var bullet =  PoolManager.instance.GetObj(Data.Default_Bullet);
            if(bullet != null){
                bullet.GetComponent<Bullet>().originTank = gameObject;
                bullet.GetComponent<Bullet>().v = tankCurrent.SpeedBullet;
                bullet.GetComponent<Bullet>().s = tankCurrent.ShootingRange;
                bullet.GetComponent<Bullet>().damage = tankCurrent.damage + damageBonus;
                bullet.GetComponent<Bullet>().hitEffect = PoolManager.instance.GetObj(Data.HitDefault_Effect);
                bullet.GetComponent<Bullet>().noHitEffect = PoolManager.instance.GetObj(Data.NoHitDefault_Effect);

                tankSound.PlayFire(tankCurrent.audioFire);
                fireEffect.Play();

                anim.SetTrigger("Fire");
                bullet.transform.position = bulletPoint.position;
                bullet.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.y));
                bullet.GetComponent<Rigidbody>().velocity = new Vector3(dir.x,0,dir.y).normalized * tankCurrent.SpeedBullet;
                bullet.SetActive(true);
                uITank.ShowReloadMagazine();
                timeFire = tankCurrent.SpeedReload;
            }
        }

        }
            dirFireBar.gameObject.SetActive(false);


    }


    public override void Skill()
    {
        if(timeSkill >=tankCurrent.SpeedReloadSkill){
            isTapSkill = true;
        }
    }
}
