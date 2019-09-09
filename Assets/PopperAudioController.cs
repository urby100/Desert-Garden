using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopperAudioController : MonoBehaviour
{
    public bool mute;
    PopperController pcScript;
    public AudioClip Pop;
    public AudioClip Hide;
    public AudioClip Neutral;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        pcScript = GetComponent<PopperController>();
    }
    bool popOnce = false;
    bool hideOnce = false;
    bool neutralOnce = false;
    // Update is called once per frame
    void Update()
    {
        if (mute) {
            return;
        }
        if (pcScript.neutral)
        {
            if (!neutralOnce)
            {
                AudioSource.PlayOneShot(Neutral);
                neutralOnce = true;
            }
        }
        else
        {
            if (pcScript.direction)
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
