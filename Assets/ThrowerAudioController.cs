﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerAudioController : MonoBehaviour
{
    public bool mute;
    ThrowerController tcScript;
    public AudioClip Attack;
    public AudioClip Neutral;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        tcScript = GetComponent<ThrowerController>();
    }
    bool attackOnce = false;
    float attackTime;
    bool neutralOnce = false;
    // Update is called once per frame
    void Update()
    {
        if (mute)
        {
            return;
        }
        if (tcScript.neutral)
        {
            if (!neutralOnce)
            {
                AudioSource.PlayOneShot(Neutral);
                neutralOnce = true;
            }
        }
        else
        {
            if (tcScript.attack)
            {
                if (Time.time > attackTime)
                {
                    attackOnce = false;
                }
                if (!attackOnce)
                {
                    AudioSource.PlayOneShot(Attack);
                    attackTime = Time.time + tcScript.attackSpeed;
                    attackOnce = true;
                }
            }
        }
    }
}
