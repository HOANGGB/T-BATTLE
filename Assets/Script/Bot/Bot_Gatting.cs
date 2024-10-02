using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Gatting : Bot
{
    Coroutine coroutine;
    bool isFire = false;
    public override void Action()
    {
        if(target.gameObject.tag == "TankPlayer" || target.gameObject.tag == "BoxItem" || target.gameObject.tag == "Bot"){
            if(Vector3.Distance(transform.position,target.transform.position) < tankCurrent.ShootingRange && canFire){
                if(timeSkill > tankCurrent.SpeedReloadSkill  && timeFire <=0){
                    if(coroutine == null){
                        anim.SetBool("Skill",true);
                        coroutine = StartCoroutine(SeriesSkill());
                    }
                }else if(timeFire <=0 && !isFire){
                    StopMoveInPath();
                    anim.SetTrigger("Fire");
                    StartCoroutine(SeriesFire());
                }
            }
        }
    }



    IEnumerator SeriesFire(){
        isFire = true;
        for(int i =0; i<3; i++){
                var bullet =  PoolManager.instance.GetObj(Data.Gatting_Bullet);
                if(bullet != null){
                    var d = target.transform.position - transform.position;
                    var dir = new Vector2(d.x,d.z);
                    bullet.GetComponent<Bullet>().originTank = gameObject;
                    bullet.GetComponent<Bullet>().v = tankCurrent.SpeedBullet;
                    bullet.GetComponent<Bullet>().s = tankCurrent.ShootingRange;
                    bullet.GetComponent<Bullet>().damage = tankCurrent.damage + damageBonus;
                    bullet.GetComponent<Bullet>().hitEffect = PoolManager.instance.GetObj(Data.HitGatting_Effect);
                    bullet.GetComponent<Bullet>().noHitEffect = PoolManager.instance.GetObj(Data.NoHitGatting_Effect);
                    tankSound.PlayFire(tankCurrent.audioFire);
                    fireEffect.Play();

                    bullet.transform.position = firePoint.transform.position;
                    bullet.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.y));
                    bullet.GetComponent<Rigidbody>().velocity = new Vector3(dir.x,0,dir.y).normalized * tankCurrent.SpeedBullet;
                    bullet.SetActive(true);
                }
                yield return new WaitForSeconds(.1f);
            }
        isFire = false;
        timeFire = tankCurrent.SpeedReload;
    }

    IEnumerator SeriesSkill(){
        isFire = true;
        timeSkill = 0;
        while(timeSkill <3){
            var bullet =  PoolManager.instance.GetObj(Data.Gatting_BulletSkill);
                if(bullet != null){
                    var d = target.transform.position - transform.position;
                    var dir = new Vector2(d.x,d.z);
                    bullet.GetComponent<Bullet>().originTank = gameObject;
                    bullet.GetComponent<Bullet>().v = tankCurrent.SpeedBullet;
                    bullet.GetComponent<Bullet>().s = tankCurrent.ShootingRange;
                    bullet.GetComponent<Bullet>().damage = tankCurrent.damage + damageBonus;
                    bullet.GetComponent<Bullet>().hitEffect = PoolManager.instance.GetObj(Data.HitGatting_Effect);
                    bullet.GetComponent<Bullet>().noHitEffect = PoolManager.instance.GetObj(Data.NoHitGatting_Effect);
                    tankSound.PlayFire(tankCurrent.audioSkill);
                    fireEffect.Play();
                    bullet.transform.position = firePoint.transform.position;
                    bullet.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.y));
                    bullet.GetComponent<Rigidbody>().velocity = new Vector3(dir.x,0,dir.y).normalized * tankCurrent.SpeedBullet;
                    bullet.SetActive(true);
                    yield return new WaitForSeconds(.1f);
                }
        }
        isFire = false;
        coroutine = null;
        timeFire = tankCurrent.SpeedReload;
        anim.SetBool("Skill",false);
    }
   public override void DealModelSpawn()
    {
        GameObject dielModel = PoolManager.instance.GetObj(Data.Died_Gatting);
        dielModel.transform.position = transform.position;
        dielModel.SetActive(true);
    }

}
