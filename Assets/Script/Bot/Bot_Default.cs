using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Default : Bot
{
    public override void Action()
    {
        if(target.gameObject.tag == "TankPlayer" || target.gameObject.tag == "BoxItem" || target.gameObject.tag == "Bot"){
            if(Vector3.Distance(transform.position,target.transform.position) < tankCurrent.ShootingRange && canFire){
                if(timeFire <=0){
                    StopMoveInPath();
                    var d = target.transform.position - transform.position;
                    var dir = new Vector2(d.x,d.z);
                    if(timeSkill > tankCurrent.SpeedReloadSkill){
                        var bullet =  PoolManager.instance.GetObj(Data.Default_BulletSkill);
                        if(bullet != null){
                            anim.SetTrigger("Skill");
                            bullet.GetComponent<Bullet>().originTank = gameObject;
                            bullet.GetComponent<Bullet>().v = tankCurrent.SpeedBullet;
                            bullet.GetComponent<Bullet>().s = tankCurrent.ShootingRange;
                            bullet.GetComponent<Bullet>().damage = tankCurrent.damage * 2 + damageBonus;
                            bullet.GetComponent<Bullet>().hitEffect = PoolManager.instance.GetObj(Data.HitDefault_Effect);
                            bullet.GetComponent<Bullet>().noHitEffect = PoolManager.instance.GetObj(Data.NoHitDefault_Effect);
                            tankSound.PlayFire(tankCurrent.audioSkill);
                            fireEffect.Play();

                            bullet.transform.position = firePoint.transform.position;
                            bullet.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.y));
                            bullet.GetComponent<Rigidbody>().velocity = new Vector3(dir.x,0,dir.y).normalized * tankCurrent.SpeedBullet;
                            bullet.SetActive(true);
                            timeFire = tankCurrent.SpeedReload;
                            timeSkill = 0;

                        }
                    }else{
                        var bullet =  PoolManager.instance.GetObj(Data.Default_Bullet);
                        if(bullet != null){
                            anim.SetTrigger("Fire");
                            bullet.GetComponent<Bullet>().originTank = gameObject;
                            bullet.GetComponent<Bullet>().v = tankCurrent.SpeedBullet;
                            bullet.GetComponent<Bullet>().s = tankCurrent.ShootingRange;
                            bullet.GetComponent<Bullet>().damage = tankCurrent.damage+ damageBonus;
                            bullet.GetComponent<Bullet>().hitEffect = PoolManager.instance.GetObj(Data.HitDefault_Effect);
                            bullet.GetComponent<Bullet>().noHitEffect = PoolManager.instance.GetObj(Data.NoHitDefault_Effect);
                            tankSound.PlayFire(tankCurrent.audioFire);
                            fireEffect.Play();

                            bullet.transform.position = firePoint.transform.position;
                            bullet.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.y));
                            bullet.GetComponent<Rigidbody>().velocity = new Vector3(dir.x,0,dir.y).normalized * tankCurrent.SpeedBullet;
                            bullet.SetActive(true);
                            timeFire = tankCurrent.SpeedReload;
                        }
                    }
                }
            }
        }
        
    
    }

    public override void DealModelSpawn()
    {
        GameObject dielModel = PoolManager.instance.GetObj(Data.Died_Default);
        dielModel.transform.position = transform.position;
        dielModel.SetActive(true);
    }
}
