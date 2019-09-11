using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaterBottleAudioController : MonoBehaviour
{
    BossWaterBottle bwbScript;
    AudioSource audioSource;
    public AudioClip PickedUpClip;
    // Start is called before the first frame update
    void Start()
    {
        bwbScript = GetComponent<BossWaterBottle>();
        audioSource = GetComponent<AudioSource>();
    }
    bool playOnce = false;
    public bool mute = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mute) {
            return;
        }
        if (bwbScript.PickedUpBottle)
        {
            if (!playOnce)
            {
                audioSource.PlayOneShot(PickedUpClip);
                playOnce = true;
            }
        }
    }
}
