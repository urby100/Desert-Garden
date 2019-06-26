using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Move moveScript;
    UseAbility useAbilityScript;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sr;
    float moving;
    public GameObject gm;
    Keybindings kb;

    private bool hurting = false;
    private float hurtLenght = 0.3f;
    private float hurtStart;

    bool useAbilityBool = false;
    float useAbilityLenght = 0.2f;
    float useAbilityStart;



    // Start is called before the first frame update
    void Start()
    {
        kb = gm.GetComponent<Keybindings>();
        useAbilityScript = GetComponent<UseAbility>();
        moveScript = GetComponent<Move>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(getAnimationState("Running"));
        moving = 0;
        if (Input.GetKey(kb.forward))
        {
            moving += 1;
        }
        if (Input.GetKey(kb.backward))
        {
            moving -= 1;
        }
        
        //hurt
        if (hurting && Time.time >= hurtStart)
        {
            moveScript.hurtRequest = false;
            hurting = false;
        }
        if (moveScript.hurtRequest)
        {
            hurt();
            if (!hurting)
            {
                hurtStart = Time.time + hurtLenght;
                hurting = true;
            }
            return;
        }

        //use ability
        if (useAbilityBool && Time.time >= useAbilityStart)
        {
            useAbilityScript.useWater = false;
            useAbilityBool = false;
        }
        if (useAbilityScript.useWater)
        {
            if (!useAbilityBool)
            {
                useAbilityStart = Time.time + useAbilityLenght;
                useAbilityBool = true;
            }
        }


        if (rb.velocity.y < 0)// falling
        {
            if (useAbilityScript.useWater)
            {
                useWaterWhileInAir();
            }
            else
            {
                falling();
            }
        }
        else if (rb.velocity.y > 0)//jump
        {
            if (!Input.GetKey(kb.jump))
            {
                if (useAbilityScript.useWater)
                {
                    useWaterWhileInAir();
                }
                else
                {
                    jumping();
                }
            }
            else
            {
                if (useAbilityScript.useWater)
                {
                    useWaterWhileInAir();
                }
                else
                {
                    movingMidAir();
                }
            }
        }
        else//landed
        {
            if (moveScript.tiredRequest)
            {
                tired();
                return;
            }
            if (rb.velocity.x != 0)
            {
                if (useAbilityScript.useWater)
                {
                    useWaterWhileRunning();
                }
                else
                {
                    running();
                }
            }
            else
            {
                if (moveScript.crouchRequest)
                {
                    if (useAbilityScript.useWater)
                    {
                        useWaterWhileCrouching();
                    }
                    else
                    {
                        crouching();
                    }
                }
                else
                {
                    if (useAbilityScript.useWater)
                    {
                        useWaterWhileStanding();
                    }
                    else
                    {
                        standing();
                    }
                }
            }
        }

    }

    void standing()
    {
        animator.SetBool("Standing", true);
        animator.SetBool("Crouching", false);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("MovingMidAir", false);
        animator.SetBool("Hurt", false);
        animator.SetBool("Tired", false);
        animator.SetBool("Use Water While Crouching", false);
        animator.SetBool("Use Water While In Air", false);
        animator.SetBool("Use Water While Standing", false);
        animator.SetBool("Use Water While Running", false);

    }
    void crouching()
    {
        animator.SetBool("Standing", false);
        animator.SetBool("Crouching", true);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("MovingMidAir", false);
        animator.SetBool("Hurt", false);
        animator.SetBool("Tired", false);
        animator.SetBool("Use Water While Crouching", false);
        animator.SetBool("Use Water While In Air", false);
        animator.SetBool("Use Water While Standing", false);
        animator.SetBool("Use Water While Running", false);
    }
    void running()
    {
        
        animator.SetBool("Standing", false);
        animator.SetBool("Crouching", false);
        animator.SetBool("Running", true);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("MovingMidAir", false);
        animator.SetBool("Hurt", false);
        animator.SetBool("Tired", false);
        animator.SetBool("Use Water While Crouching", false);
        animator.SetBool("Use Water While In Air", false);
        animator.SetBool("Use Water While Standing", false);
        animator.SetBool("Use Water While Running", false);

    }
    void jumping()
    {
        animator.SetBool("Standing", false);
        animator.SetBool("Crouching", false);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", true);
        animator.SetBool("Falling", false);
        animator.SetBool("MovingMidAir", false);
        animator.SetBool("Hurt", false);
        animator.SetBool("Tired", false);
        animator.SetBool("Use Water While Crouching", false);
        animator.SetBool("Use Water While In Air", false);
        animator.SetBool("Use Water While Standing", false);
        animator.SetBool("Use Water While Running", false);

    }
    void falling()
    {
        animator.SetBool("Standing", false);
        animator.SetBool("Crouching", false);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", true);
        animator.SetBool("MovingMidAir", false);
        animator.SetBool("Hurt", false);
        animator.SetBool("Tired", false);
        animator.SetBool("Use Water While Crouching", false);
        animator.SetBool("Use Water While In Air", false);
        animator.SetBool("Use Water While Standing", false);
        animator.SetBool("Use Water While Running", false);

    }
    void movingMidAir()
    {
        animator.SetBool("Standing", false);
        animator.SetBool("Crouching", false);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("MovingMidAir", true);
        animator.SetBool("Hurt", false);
        animator.SetBool("Tired", false);
        animator.SetBool("Use Water While Crouching", false);
        animator.SetBool("Use Water While In Air", false);
        animator.SetBool("Use Water While Standing", false);
        animator.SetBool("Use Water While Running", false);

    }
    void hurt()
    {
        animator.SetBool("Standing", false);
        animator.SetBool("Crouching", false);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("MovingMidAir", false);
        animator.SetBool("Hurt", true);
        animator.SetBool("Tired", false);
        animator.SetBool("Use Water While Crouching", false);
        animator.SetBool("Use Water While In Air", false);
        animator.SetBool("Use Water While Standing", false);
        animator.SetBool("Use Water While Running", false);
    }
    void tired()
    {
        animator.SetBool("Standing", false);
        animator.SetBool("Crouching", false);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("MovingMidAir", false);
        animator.SetBool("Hurt", false);
        animator.SetBool("Tired", true);
        animator.SetBool("Use Water While Crouching", false);
        animator.SetBool("Use Water While In Air", false);
        animator.SetBool("Use Water While Standing", false);
        animator.SetBool("Use Water While Running", false);
    }
    void useWaterWhileCrouching()
    {
        animator.SetBool("Standing", false);
        animator.SetBool("Crouching", false);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("MovingMidAir", false);
        animator.SetBool("Hurt", false);
        animator.SetBool("Tired", false);
        animator.SetBool("Use Water While Crouching", true);
        animator.SetBool("Use Water While In Air", false);
        animator.SetBool("Use Water While Standing", false);
        animator.SetBool("Use Water While Running", false);
    }
    void useWaterWhileInAir()
    {
        animator.SetBool("Standing", false);
        animator.SetBool("Crouching", false);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("MovingMidAir", false);
        animator.SetBool("Hurt", false);
        animator.SetBool("Tired", false);
        animator.SetBool("Use Water While Crouching", false);
        animator.SetBool("Use Water While In Air", true);
        animator.SetBool("Use Water While Standing", false);
        animator.SetBool("Use Water While Running", false);
    }
    void useWaterWhileStanding()
    {
        animator.SetBool("Standing", false);
        animator.SetBool("Crouching", false);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("MovingMidAir", false);
        animator.SetBool("Hurt", false);
        animator.SetBool("Tired", false);
        animator.SetBool("Use Water While Crouching", false);
        animator.SetBool("Use Water While In Air", false);
        animator.SetBool("Use Water While Standing", true);
        animator.SetBool("Use Water While Running", false);
    }
    void useWaterWhileRunning()
    {
        animator.SetBool("Standing", false);
        animator.SetBool("Crouching", false);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("MovingMidAir", false);
        animator.SetBool("Hurt", false);
        animator.SetBool("Tired", false);
        animator.SetBool("Use Water While Crouching", false);
        animator.SetBool("Use Water While In Air", false);
        animator.SetBool("Use Water While Standing", false);
        animator.SetBool("Use Water While Running", true);
    }
    void animationControl(string animationName)
    {


    }
    bool getAnimationState(string name) {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
