using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowerAudioController : MonoBehaviour
{
    public bool mute;
    BossThrowerController btcScript;
    public AudioClip Attack;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        btcScript = GetComponent<BossThrowerController>();
    }
    float attackTime;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mute)
        {
            return;
        }
        if (Time.time > attackTime)
        {
            AudioSource.PlayOneShot(Attack);
            attackTime = Time.time + btcScript.attackSpeed;
        }

    }
}
