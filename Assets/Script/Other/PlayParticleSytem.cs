using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleSytem : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable() {
        PlayEffect();
        StartCoroutine(DisableEffect());
    }
    void PlayEffect()
    {
        foreach(Transform  pa in transform){
            ParticleSystem p = pa.GetComponent<ParticleSystem>();
            if(p) p.Play();
        }
    }
    IEnumerator DisableEffect(){
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
