using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveAnimations : MonoBehaviour
{
    GameObject steveGameobject;
    Animator animator;
    public float maxThirst = 1;
    public float thirst = 0;
    bool satisfiedBool = false;
    public float satisfiedLasts = 2f;
    float satisfiedTime;
    bool neutralBool = false;
    // Start is called before the first frame update
    void Start()
    {

        steveGameobject = transform.parent.gameObject;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (neutralBool)
        {
            if (steveGameobject.GetComponent<SteveController>().arrived)
            {
                neutral();
            }
            else
            {
                neutralNoLittle();
            }
        }
        if (satisfiedBool && !neutralBool)
        {
            if (steveGameobject.GetComponent<SteveController>().arrived)
            {
                satisfied();
            }
            else {
                satisfiedNoLittle();
            }
        }
        if (thirst >= maxThirst && !neutralBool && !satisfiedBool)
        {
            GetComponent<PolygonCollider2D>().enabled= false;
            GetComponent<BoxCollider2D>().enabled = false;
            satisfiedBool = true;
            satisfiedTime = Time.time + satisfiedLasts;
            steveGameobject.GetComponent<SteveController>().neutral = true;
        }
        if (Time.time > satisfiedTime && satisfiedBool && !neutralBool)
        {
            neutralBool = true;
        }
        if (steveGameobject.GetComponent<SteveController>().attack && !neutralBool && !satisfiedBool)
        {
            attack();
        }
        if (!steveGameobject.GetComponent<SteveController>().attack && !neutralBool && !satisfiedBool)
        {
            normal();
        }
    }
    void normal()
    {
        animator.SetBool("normal", true);
        animator.SetBool("attack", false);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", false);
        animator.SetBool("satisfied-no-little", false);
        animator.SetBool("neutral-no-little", false);
    }
    void attack()
    {
        animator.SetBool("normal", false);
        animator.SetBool("attack", true);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", false);
        animator.SetBool("satisfied-no-little", false);
        animator.SetBool("neutral-no-little", false);

    }
    void neutral()
    {
        animator.SetBool("normal", false);
        animator.SetBool("attack", false);
        animator.SetBool("neutral", true);
        animator.SetBool("satisfied", false);
        animator.SetBool("satisfied-no-little", false);
        animator.SetBool("neutral-no-little", false);
    }
    void satisfied()
    {
        animator.SetBool("normal", false);
        animator.SetBool("attack", false);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", true);
        animator.SetBool("satisfied-no-little", false);
        animator.SetBool("neutral-no-little", false);
    }
    void satisfiedNoLittle()
    {
        animator.SetBool("normal", false);
        animator.SetBool("attack", false);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", false);
        animator.SetBool("satisfied-no-little", true);
        animator.SetBool("neutral-no-little", false);
    }
    void neutralNoLittle()
    {
        animator.SetBool("normal", false);
        animator.SetBool("attack", false);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", false);
        animator.SetBool("satisfied-no-little", false);
        animator.SetBool("neutral-no-little", true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "WaterProjectile")
        {
            thirst++;
        }
    }
}
