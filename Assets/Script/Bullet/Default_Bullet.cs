using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default_Bullet : Bullet
{
    public override void OnEnableThis()
    {
        float t = s/v;
        if (float.IsNaN(t) || t <= 0) return; 
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine =  StartCoroutine(DeactivateAfterTime(t));
    } 
    public IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        noHitEffect.transform.position = transform.position;
        noHitEffect.SetActive(true);
        gameObject.SetActive(false);
    }
}
