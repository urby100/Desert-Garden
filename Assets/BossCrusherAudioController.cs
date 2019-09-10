using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrusherAudioController : MonoBehaviour
{
    public bool mute;
    BossCrusherController bccScript;
    public AudioClip Attack;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        bccScript = GetComponent<BossCrusherController>();
    }
    float repeatSound;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mute)
        {
            return;
        }
        if (bccScript.attack)
        {
            if (Time.time > repeatSound)
            {
                repeatSound = Time.time + Attack.length;
                AudioSource.PlayOneShot(Attack);
            }
        }

    }
}
