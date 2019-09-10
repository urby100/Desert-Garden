using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdyAudioController : MonoBehaviour
{
    public bool mute;
    BirdyController bcScript;
    public AudioClip Running;
    public AudioClip RunningNeutral;
    public AudioClip Neutral;
    public AudioClip Attack;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        bcScript = GetComponent<BirdyController>();
    }
    float runningSoundTime;
    bool AttackOnce = false;
    bool neutralOnce = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mute)
        {
            return;
        }
        if (bcScript.neutral)
        {
            if (!neutralOnce)
            {
                AudioSource.PlayOneShot(Neutral);
                neutralOnce = true;
            }
        }
        if (bcScript.attack)
        {
            if (!AttackOnce)
            {
                AudioSource.PlayOneShot(Attack);
                AttackOnce = true;
            }
        }
        else
        {
            AttackOnce = false;
        }
        if (Time.time > runningSoundTime && !bcScript.attack)
        {
            if (bcScript.neutral)
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
