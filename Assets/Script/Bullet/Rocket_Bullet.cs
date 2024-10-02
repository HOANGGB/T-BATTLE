using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Bullet : Bullet
{
    // public float firingAngle = 45.0f;
    // public float gravity = 9.8f;
    // private Transform myTransform;
    // public override void OnEnableThis()
    // {
    //     myTransform = dirCurrent;
    //     if(coroutine != null) StopCoroutine(coroutine);
    //     coroutine =  StartCoroutine(SimulateProjectile());

    // }

    // IEnumerator SimulateProjectile()
    // {
    //     // Lưu vị trí ban đầu của đạn
    //     Vector3 startPos = myTransform.position;

    //     // Tính toán khoảng cách đến mục tiêu
    //     float dirTarget_Distance = Vector3.Distance(startPos, dirTarget.position);

    //     // Tính toán độ cao mà đạn cần đạt được theo góc bắn
    //     float projectile_Velocity = dirTarget_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

    //     // Tính toán các thành phần tốc độ trên trục x và y
    //     float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
    //     float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

    //     // Thời gian đạn bay đến mục tiêu
    //     float flightDuration = dirTarget_Distance / Vx;

    //     // Xoay đạn về phía mục tiêu
    //     myTransform.rotation = Quaternion.LookRotation(dirTarget.position - myTransform.position);

    //     float elapse_time = 0;

    //     while (elapse_time < flightDuration)
    //     {
    //         myTransform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

    //         elapse_time += Time.deltaTime;

    //         yield return null;
    //     }
    // }
    public override void OnEnableThis()
    {
        throw new System.NotImplementedException();
    }
}
