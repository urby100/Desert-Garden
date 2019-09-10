using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPopperAudioController : MonoBehaviour
{
    public bool mute;
    BossPopperController bpcScript;
    public AudioClip Pop;
    public AudioClip Hide;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        bpcScript = GetComponent<BossPopperController>();
    }
    bool popOnce = false;
    bool hideOnce = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mute)
        {
            return;
        }
        else
        {
            if (bpcScript.direction)
            {
                if (!popOnce)
                {
                    AudioSource.PlayOneShot(Pop);
                    popOnce = true;
                    hideOnce = false;
                }
            }
            else
            {
                if (!hideOnce)
                {
                    AudioSource.PlayOneShot(Hide);
                    popOnce = false;
                    hideOnce = true;
                }
            }
        }
    }
}
