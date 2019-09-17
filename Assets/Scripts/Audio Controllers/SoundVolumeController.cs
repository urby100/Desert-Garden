using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Awake()
    {
        SetAudioSourcesVolume();
        if (gameObject.name == "MusicVolumeSlider") {
            gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume", 1);
        }
        if (gameObject.name == "SoundEffectsSlider")
        {
            gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFXVolume", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAudioSourcesVolume()
    {
        if (gameObject.name == "LittleSteve")
        {
            gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume", 1);
            return;
        }

        var foundObjects = FindObjectsOfType<AudioSource>();
        foreach (AudioSource s in foundObjects) {
            if (s.gameObject.name == "Main Camera")
            {
                s.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
            }
            else
            {
                s.volume = PlayerPrefs.GetFloat("SFXVolume", 1);
            }
        }
    }
    public void SetMusicVolume(float newValue) {
        PlayerPrefs.SetFloat("MusicVolume", newValue);
        SetAudioSourcesVolume();
    }
    public void SetSFXVolume(float newValue)
    {
        PlayerPrefs.SetFloat("SFXVolume", newValue);
        SetAudioSourcesVolume();

    }


}
