using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChameleonAudioController : MonoBehaviour
{
    public bool mute;
    BossChameleonController bccScript;
    public AudioClip Attack;
    public AudioClip Hide;
    AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        bccScript = GetComponent<BossChameleonController>();
        AudioSource = GetComponent<AudioSource>();
    }
    bool hideOnce = true;
    bool attackOnce = false;
    float attackTime;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mute)
        {
            return;
        }
        if (bccScript.attack)
        {
            if (Time.time > attackTime)
            {
                attackOnce = false;
            }
            if (!attackOnce)
            {
                AudioSource.PlayOneShot(Attack);
                attackTime = bccScript.attackTime;
                attackOnce = true;
                hideOnce = false;
            }
        }
        else
        {
            if (!hideOnce)
            {
                AudioSource.PlayOneShot(Hide);
                hideOnce = true;
            }
        }

    }
}
