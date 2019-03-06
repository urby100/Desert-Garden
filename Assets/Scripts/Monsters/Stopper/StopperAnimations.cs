using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopperAnimations : MonoBehaviour
{
    Animator animator;
    GameObject stopperGameObject;
    public float maxThirst = 1;
    float thirst = 0;
    bool satisfiedBool = false;
    public float satisfiedLasts = 2f;
    float satisfiedTime;
    bool neutralBool = false;
    // Start is called before the first frame update
    void Start()
    {
        stopperGameObject = transform.parent.gameObject;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (neutralBool)
        {
            neutral();
        }
        if (thirst >= maxThirst && !neutralBool && !satisfiedBool)
        {
            stopperGameObject.GetComponent<StopperController>().attackBool = false;
            stopperGameObject.GetComponent<StopperController>().satisfiedBool = true;
            satisfiedBool = true;
            GetComponent<PolygonCollider2D>().enabled = false;
            satisfiedTime = Time.time + satisfiedLasts;
            satisfied();
        }
        if (Time.time > satisfiedTime && satisfiedBool && !neutralBool)
        {
            neutralBool = true;
        }
        if (stopperGameObject.GetComponent<StopperController>().attackBool && !satisfiedBool)
        {
            attack();
        }
        if(!stopperGameObject.GetComponent<StopperController>().attackBool && !satisfiedBool) {
            idle();
        }
    }
    void idle()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Neutral", false);
        animator.SetBool("Satisfied", false);

    }
    void attack()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", true);
        animator.SetBool("Neutral", false);
        animator.SetBool("Satisfied", false);
    }
    void neutral()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Neutral", true);
        animator.SetBool("Satisfied", false);
    }
    void satisfied()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Neutral", false);
        animator.SetBool("Satisfied", true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "WaterProjectile")
        {
            thirst++;
        }
    }
}
