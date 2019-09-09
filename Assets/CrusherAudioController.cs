using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherAudioController : MonoBehaviour
{
    public bool mute;
    CrusherController ccScript;
    public AudioClip Attack;
    public AudioClip Neutral;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        ccScript = GetComponent<CrusherController>();
    }
    bool attackOnce = false;
    float repeatSound;
    bool neutralOnce = false;
    // Update is called once per frame
    void Update()
    {
        if (mute)
        {
            return;
        }
        if (ccScript.neutral)
        {
            if (!neutralOnce)
            {
                AudioSource.PlayOneShot(Neutral);
                neutralOnce = true;
            }
        }
        else
        {
            if (ccScript.attack)
            {
                if (Time.time > repeatSound) {
                    repeatSound = Time.time + Attack.length; 
                    attackOnce = false;
                }
                if (!attackOnce)
                {
                    AudioSource.PlayOneShot(Attack);
                    attackOnce = true;
                }
            }
        }
    }
}
