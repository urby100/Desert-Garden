using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBirdyAudioController : MonoBehaviour
{
    public bool mute;
    BirdyController bcScript;
    public AudioClip Running;
    AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        bcScript = GetComponent<BirdyController>();
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
        if (Time.time > runningSoundTime)
        {
            AudioSource.PlayOneShot(Running);
            runningSoundTime = Time.time + Running.length;
        }

    }
}
