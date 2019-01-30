using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sr;
    ScriptableObject so;
    float moving;

    private bool hurting = false;
    private float hurtLenght = 0.3f;
    private float hurtStart;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //obračanje
        moving = Input.GetAxisRaw("Horizontal");
        if (gameObject.GetComponent<Move>().hurtRequest)
        {
            moving = 0;
        }
        else if (gameObject.GetComponent<Move>().tiredRequest) {

            moving = 0;
        }
        if (moving == -1)
        {
            sr.flipX = true;
        }
        else if (moving == 1)
        {
            sr.flipX = false;

        }

        //hurt
        if (hurting && Time.time >= hurtStart)
        {
            gameObject.GetComponent<Move>().hurtRequest = false;
            hurting = false;
        }
        if (gameObject.GetComponent<Move>().hurtRequest)
        {
            hurt();
            if (!hurting)
            {
                hurtStart = Time.time + hurtLenght;
                hurting = true;
            }
            return;
        }

        if (rb.velocity.y < 0)// falling
        {
            falling();
        }
        else if (rb.velocity.y > 0)//jump
        {
            if (!Input.GetKey(KeyCode.UpArrow))
            {
                jumping();
            }
            else
            {
                movingMidAir();
            }
        }
        else//landed
        {
            if (gameObject.GetComponent<Move>().tiredRequest)
            {
                tired();
                return;
            }
            if (rb.velocity.x != 0)
            {
                running();
            }
            else
            {
                if (gameObject.GetComponent<Move>().crouchRequest)
                {
                    crouching();
                }
                else
                {
                    standing();
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

    }
    void animationControl(string animationName)
    {


    }
}
