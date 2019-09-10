using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonAudioController : MonoBehaviour
{
    public bool mute;
    ChameleonController ccScript;
    ChameleonAnimations caScript;
    public AudioClip Attack;
    public AudioClip Hide;
    public AudioClip Neutral;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        ccScript = GetComponent<ChameleonController>();
        caScript = transform.GetChild(0).gameObject.GetComponent<ChameleonAnimations>();
    }
    bool hideOnce=true;
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
                if (Time.time > attackTime)
                {
                    attackOnce = false;
                }
                if (!attackOnce)
                {
                    AudioSource.PlayOneShot(Attack);
                    attackTime = ccScript.attackTime;
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
}
