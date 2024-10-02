using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
     public static Sound instance;
     
    [SerializeField] AudioMixer au;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider musicSlider;
        private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    void Start(){

        volumeSlider.value = PlayerPrefs.GetFloat("Volume",1);
        musicSlider.value = PlayerPrefs.GetFloat("Music",1);
    }

    public void ChangeVolume(){
        au.SetFloat("volume",Mathf.Log10(volumeSlider.value) * 50);
        PlayerPrefs.SetFloat("Volume",volumeSlider.value);
    }
    public void ChangeMusic(){
        au.SetFloat("music",Mathf.Log10(musicSlider.value) * 50);
        PlayerPrefs.SetFloat("Music",musicSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
