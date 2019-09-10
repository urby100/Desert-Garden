using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveAudioController : MonoBehaviour
{
    public bool mute;
    SteveController scScript;
    public AudioClip Attack;
    public AudioClip Waiting;
    public AudioClip Arrived;
    public AudioClip Neutral;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        scScript = GetComponent<SteveController>();
    }
    bool attackOnce;
    bool arrivedOnce;
    bool waitingOnce;
    bool neutralOnce;
    float waitingTime;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mute)
        {
            return;
        }
        if (scScript.neutral)
        {
            if (!neutralOnce)
            {
                AudioSource.PlayOneShot(Neutral);
                neutralOnce = true;
            }
        }
        else
        {

            if (Time.time > waitingTime && scScript.attack && !scScript.arrived)
            {
                waitingTime = Time.time + Waiting.length;
                AudioSource.PlayOneShot(Waiting);
            }
            if (scScript.arrived)
            {
                if (!arrivedOnce)
                {
                    AudioSource.PlayOneShot(Arrived);
                    arrivedOnce = true;
                }
            }
            else
            {
                arrivedOnce = false;
            }
            if (scScript.attack)
            {
                if (!attackOnce)
                {
                    AudioSource.PlayOneShot(Attack);
                    attackOnce = true;
                }
            }
            else
            {
                attackOnce = false;
            }
        }
    }
}
