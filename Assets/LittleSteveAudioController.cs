using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSteveAudioController : MonoBehaviour
{
    public bool mute;
    SteveProjectileController spcScript;
    public AudioClip Land;
    public AudioClip Jump;
    public AudioClip Running;
    public AudioClip Neutral;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        spcScript = GetComponent<SteveProjectileController>();
    }
    bool LandOnce;
    bool JumpOnce;
    bool neutralOnce;
    float runningTime;
    bool needsToLand;
    // Update is called once per frame
    void Update()
    {
        if (mute)
        {
            return;
        }
        if (spcScript.neutral)
        {
            if (!neutralOnce)
            {
                AudioSource.PlayOneShot(Neutral);
                neutralOnce = true;
            }
        }
        if (Time.time > runningTime && spcScript.running && !spcScript.inTheAir)
        {
            runningTime = Time.time + Running.length;
            AudioSource.PlayOneShot(Running);
        }
        if (spcScript.running && !spcScript.inTheAir && !spcScript.jumpedBack)
        {
            if (!LandOnce)
            {
                AudioSource.PlayOneShot(Land);
                LandOnce= true;
            }
        }
        if (spcScript.jumpedBack)
        {
            if (!JumpOnce)
            {
                AudioSource.PlayOneShot(Jump);
                JumpOnce= true;
            }
        }


    }
}
