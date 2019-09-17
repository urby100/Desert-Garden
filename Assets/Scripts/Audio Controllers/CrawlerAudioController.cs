using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerAudioController : MonoBehaviour
{
    public bool mute;
    CrawlerAnimations caScript;
    public AudioClip Running;
    public AudioClip RunningNeutral;
    public AudioClip Neutral;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        caScript = transform.GetChild(0).gameObject.GetComponent<CrawlerAnimations>();
    }
    float runningSoundTime;
    bool neutralOnce = false;
    // Update is called once per frame
    void Update()
    {
        if (mute)
        {
            return;
        }
        if (caScript.NeutralBool)
        {
            if (!neutralOnce)
            {
                AudioSource.PlayOneShot(Neutral);
                neutralOnce = true;
            }
        }
        if (Time.time > runningSoundTime)
        {
            if (caScript.NeutralBool)
            {
                AudioSource.PlayOneShot(RunningNeutral);
            }
            else
            {
                AudioSource.PlayOneShot(Running);
            }
            runningSoundTime = Time.time + Running.length;
        }
        
    }
}
