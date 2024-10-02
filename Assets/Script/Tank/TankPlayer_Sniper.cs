using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class TankPlayer_Sniper : TankPlayer
{
   [SerializeField] float ShootingRangeSkill;
   float view = 70;
   float targetView = 80;
    [SerializeField] float elapsedTime;

    public override void Fire()
    {
        if(joystickFire.Horizontal != 0 || joystickFire.Vertical!= 0){
            DragFire();
        }else if(joystickFire.Horizontal == 0 || joystickFire.Vertical == 0){
            DropFire();
        }
    }

    void DragFire()
    {
        dir = new Vector2(joystickFire.Horizontal,joystickFire.Vertical);
        dirFireBar.gameObject.SetActive(true);
      

        if(!isTap){
            isTap = true;
        }

        if(isTapSkill){
            elapsedTime += Time.deltaTime;
            GameObject.FindGameObjectWithTag("CameraVirtual").GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView
            = Mathf.Lerp(view, targetView, elapsedTime /1);
            if (elapsedTime > 1) elapsedTime = 1;
        }
    }
    void DropFire()
    {
            dirFireBar.gameObject.SetActive(false);
            if(isTap){
                    isTap = false;
                    if(isTapSkill){
                        isTapSkill = false;
                        var bullet =  PoolManager.instance.GetObj(Data.Sniper_BulletSKill);
                        if(bullet != null){
                            bullet.GetComponent<Bullet>().originTank = gameObject;
                            bullet.GetComponent<Bullet>().v = tankCurrent.SpeedBullet;
                            bullet.GetComponent<Bullet>().s = tankCurrent.ShootingRange;
                            bullet.GetComponent<Bullet>().damage = tankCurrent.damage * 3 + damageBonus;
                            bullet.GetComponent<Bullet>().hitEffect = PoolManager.instance.GetObj(Data.HitSniper_Effect);
                            bullet.GetComponent<Bullet>().noHitEffect = PoolManager.instance.GetObj(Data.NoHitSniper_Effect);

                            tankSound.PlayFire(tankCurrent.audioSkill);
                            anim.SetTrigger("Skill");
                            fireEffect.Play();
                            
                            bullet.transform.position = bulletPoint.position;
                            bullet.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.y));
                            bullet.GetComponent<Rigidbody>().velocity = new Vector3(dir.x,0,dir.y).normalized * tankCurrent.SpeedBullet;
                            bullet.SetActive(true);

                            uITank.ShowReloadMagazine();
                            GameObject.FindGameObjectWithTag("CameraVirtual").GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView
                            = Mathf.Lerp(targetView,view,1f);
;
                            timeSkill = 0;
                            elapsedTime = 0;
                            return;
                        }

                    }
                    else if(timeFire <=0){
                        var bullet =  PoolManager.instance.GetObj(Data.Sniper_Bullet);
                        if(bullet != null){
                            bullet.GetComponent<Bullet>().originTank = gameObject;
                            bullet.GetComponent<Bullet>().v = tankCurrent.SpeedBullet;
                            bullet.GetComponent<Bullet>().s = tankCurrent.ShootingRange;
                            bullet.GetComponent<Bullet>().damage = tankCurrent.damage + damageBonus;
                            bullet.GetComponent<Bullet>().hitEffect = PoolManager.instance.GetObj(Data.HitSniper_Effect);
                            bullet.GetComponent<Bullet>().noHitEffect = PoolManager.instance.GetObj(Data.NoHitSniper_Effect);
                            
                            tankSound.PlayFire(tankCurrent.audioFire);
                            anim.SetTrigger("Fire");
                            fireEffect.Play();
                            bullet.transform.position = bulletPoint.position;
                            bullet.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.y));
                            bullet.GetComponent<Rigidbody>().velocity = new Vector3(dir.x,0,dir.y).normalized * tankCurrent.SpeedBullet;
                            bullet.SetActive(true);

                            uITank.ShowReloadMagazine();

                            timeFire = tankCurrent.SpeedReload;
                        }
                    }
            }
            dir = Vector3.zero;
            dirFireBar.gameObject.SetActive(false);
    }


    public override void Skill()
    {
        if(timeSkill >= tankCurrent.SpeedReloadSkill){
            isTapSkill = true;
            // GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView
            // = Mathf.Lerp(view,targetView,4f);
            
        }
    }


}
