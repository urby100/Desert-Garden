using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStopperAudioController : MonoBehaviour
{
    public bool mute;
    BossStopperController bscScript;
    public AudioClip Attack;
    public AudioClip Receive;
    AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        bscScript = GetComponent<BossStopperController>();
        AudioSource = GetComponent<AudioSource>();
    }
    bool attackOnce = false;
    bool ReceiveOnce = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mute)
        {
            return;
        }
        if (bscScript.hasProjectile)
        {
            if (!ReceiveOnce)
            {
                AudioSource.PlayOneShot(Receive);
                attackOnce = false;
                ReceiveOnce = true;
            }
        }
        if (Time.time > bscScript.animationTime && bscScript.attackBool)
        {
            if (!attackOnce)
            {
                AudioSource.PlayOneShot(Attack);
                attackOnce = true;
                ReceiveOnce = false;
            }
        }



    }
}
