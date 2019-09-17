using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{

    public bool mute;
    Move moveScript;
    UseAbility useAbilityScript;
    Rigidbody2D rb;
    public AudioClip Running;
    public AudioClip Jump;
    public AudioClip Land;
    public AudioClip Crouch;
    public AudioClip Tired;
    public AudioClip Hurt;
    public AudioClip AbilityAttack;
    public AudioClip WaterAttack;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<Move>();
        useAbilityScript = GetComponent<UseAbility>();
        rb = GetComponent<Rigidbody2D>();

    }
    float runningSoundTime;
    bool JumpOnce = false;
    bool LandOnce = true;
    bool CrouchOnce= false;
    bool TiredOnce = false;
    bool HurtOnce = false;
    bool AbilityAttackOnce = false;
    bool WaterAttackOnce = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mute)
        {
            return;
        }
        if (moveScript.jump)
        {
            if (!JumpOnce)
            {
                AudioSource.PlayOneShot(Jump);
                JumpOnce = true;
            }
            LandOnce = false;
        }
        else
        {
            if (!LandOnce)
            {
                AudioSource.PlayOneShot(Land);
                LandOnce = true;
            }
            JumpOnce = false;
        }
        if (moveScript.crouchRequest)
        {
            if (!CrouchOnce)
            {
                AudioSource.PlayOneShot(Crouch);
                CrouchOnce = true;
            }
        }
        else
        {
            CrouchOnce = false;
        }
        if (moveScript.tiredRequest)
        {
            if (!TiredOnce)
            {
                AudioSource.PlayOneShot(Tired);
                TiredOnce = true;
            }
        }
        else
        {
            TiredOnce = false;
        }
        if (moveScript.hurtRequest)
        {
            if (!HurtOnce)
            {
                AudioSource.PlayOneShot(Hurt);
                HurtOnce = true;
            }
        }
        else
        {
            HurtOnce = false;
        }
        if (useAbilityScript.useAbility)
        {
            if (!AbilityAttackOnce)
            {
                AudioSource.PlayOneShot(AbilityAttack);
                AbilityAttackOnce = true;
            }
        }
        else
        {
            AbilityAttackOnce = false;
        }
        if (useAbilityScript.useWater)
        {
            if (!WaterAttackOnce)
            {
                AudioSource.PlayOneShot(WaterAttack);
                WaterAttackOnce = true;
            }
        }
        else
        {
            WaterAttackOnce = false;
        }
        if (rb.velocity.x!=0 && rb.velocity.y==0)
        {
            if (Time.time > runningSoundTime) {
                AudioSource.PlayOneShot(Running);
                runningSoundTime = Time.time + Running.length;
            }
        }

    }
}
