using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVolume : MonoBehaviour
{
   AudioSource au;
   void Awake(){
    au = GetComponent<AudioSource>();
   }
    public void VolumeTest(){
        au.Play();
    }
}
