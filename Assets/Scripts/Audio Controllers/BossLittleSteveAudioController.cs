using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLittleSteveAudioController : MonoBehaviour
{
    public bool mute;
    LittleSteveBossController lsbcScript;
    public AudioClip Running;
    AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        lsbcScript = GetComponent<LittleSteveBossController>();
        AudioSource = GetComponent<AudioSource>();
    }
    float runningSoundTime;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mute)
        {
            return;
        }
        if (lsbcScript.changedSpeed)
        {
            AudioSource.pitch = 1.5f;
        }
        else
        {
            AudioSource.pitch = 1f;
        }
        if (Time.time > runningSoundTime)
        {
            AudioSource.PlayOneShot(Running);
            runningSoundTime = Time.time + Running.length;
        }

    }
}
