using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopperAudioController : MonoBehaviour
{
    public bool mute;
    StopperController scScript;
    public AudioClip Attack;
    public AudioClip Neutral;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        scScript = GetComponent<StopperController>();
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
        if (scScript.satisfiedBool)
        {
            if (!neutralOnce)
            {
                AudioSource.PlayOneShot(Neutral);
                neutralOnce = true;
            }
        }
        else
        {
            if (scScript.attackBool)
            {
                if (Time.time > attackTime) {
                    attackOnce = false;
                }
                if (!attackOnce)
                {
                    AudioSource.PlayOneShot(Attack);
                    attackTime = Time.time+scScript.fireRate;
                    attackOnce = true;
                }
            }
        }
    }
}
