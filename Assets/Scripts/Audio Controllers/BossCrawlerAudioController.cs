using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrawlerAudioController : MonoBehaviour
{
    public bool mute;
    CrawlerAnimations caScript;
    public AudioClip Running;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        caScript = transform.GetChild(0).gameObject.GetComponent<CrawlerAnimations>();
    }
    float runningSoundTime;
    // Update is called once per frame
    void Update()
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
