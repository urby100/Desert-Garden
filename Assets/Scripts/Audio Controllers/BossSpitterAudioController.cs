using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpitterAudioController : MonoBehaviour
{
    public bool mute;
    SpitterController scScript;
    public AudioClip Attack;
    public AudioClip Neutral;
    AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        scScript = GetComponent<SpitterController>();
        AudioSource = GetComponent<AudioSource>();
    }
    bool attackOnce = false;
    float attackTime;
    bool neutralOnce = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mute)
        {
            return;
        }
        if (scScript.Neutral)
        {
            if (!neutralOnce)
            {
                AudioSource.PlayOneShot(Neutral);
                neutralOnce = true;
            }
        }
        else
        {
            if (scScript.Attack)
            {
                if (Time.time > attackTime)
                {
                    attackOnce = false;
                }
                if (!attackOnce)
                {
                    AudioSource.PlayOneShot(Attack);
                    attackTime = scScript.fireTime;
                    attackOnce = true;
                }
            }
        }
    }
}
