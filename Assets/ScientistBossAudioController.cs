using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistBossAudioController : MonoBehaviour
{
    public bool mute;
    ScientistBossScript sbsScript;
    public AudioClip Attack;
    public AudioClip Tired;
    public AudioClip Alive;
    AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        sbsScript = GetComponent<ScientistBossScript>();
        AudioSource = GetComponent<AudioSource>();
    }
    bool tiredOnce = false;
    bool attackOnce = false;
    float alivetime;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mute)
        {
            return;
        }
        if (sbsScript.throwAnimation)
        {
            if (!attackOnce)
            {
                AudioSource.PlayOneShot(Attack);
                attackOnce = true;
            }
        }
        else {
            attackOnce = false;
        }
        if (sbsScript.tired)
        {
            if (!tiredOnce)
            {
                AudioSource.PlayOneShot(Tired);
                tiredOnce = true;
            }
        }
        else {
            if (Time.time > alivetime)
            {
                AudioSource.PlayOneShot(Alive);
                alivetime = Time.time + Alive.length;

            }
        }

    }
}
