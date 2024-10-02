using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip fire;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(string nameAudio){
        if(nameAudio == Audio.Fire){
            audioSource.clip = fire;
        }

        audioSource.Play();
    }
    
    public void PlayFire(AudioClip fire){
        audioSource.clip = fire;
        audioSource.Play();
    }
}