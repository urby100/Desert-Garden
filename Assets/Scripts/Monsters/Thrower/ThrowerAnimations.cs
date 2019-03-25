using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerAnimations : MonoBehaviour
{
    GameObject throwerGameobject;
    Animator animator;
    public float maxThirst = 1;
    float thirst = 0;
    bool satisfiedBool = false;
    public float satisfiedLasts = 2f;
    float satisfiedTime;
    bool neutralBool = false;
    // Start is called before the first frame update
    void Start()
    {

        throwerGameobject = transform.parent.gameObject;
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
            GetComponent<PolygonCollider2D>().enabled = false;
            satisfiedBool = true;
            satisfiedTime = Time.time + satisfiedLasts;
            satisfied();
            throwerGameobject.GetComponent<ThrowerController>().neutral = true;
        }
        if (Time.time > satisfiedTime && satisfiedBool && !neutralBool)
        {
            neutralBool = true;
        }
        if (throwerGameobject.GetComponent<ThrowerController>().attack && !neutralBool && !satisfiedBool)
        {

            if (Time.time > throwerGameobject.GetComponent<ThrowerController>().attackSpeedTime -
                            (throwerGameobject.GetComponent<ThrowerController>().attackSpeed / 2))
            {//animacija
                normal();
            }
            else
            {
                attacking();
            }
        }
        if (!throwerGameobject.GetComponent<ThrowerController>().attack && !neutralBool && !satisfiedBool)
        {
            normal();
        }
    }
    void normal()
    {
        animator.SetBool("normal", true);
        animator.SetBool("attacking", false);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", false);
    }

    void attacking()
    {
        animator.SetBool("normal", false);
        animator.SetBool("attacking", true);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", false);

    }
    void neutral()
    {
        animator.SetBool("normal", false);
        animator.SetBool("attacking", false);
        animator.SetBool("neutral", true);
        animator.SetBool("satisfied", false);
    }
    void satisfied()
    {
        animator.SetBool("normal", false);
        animator.SetBool("attacking", false);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "WaterProjectile")
        {
            thirst++;
            Destroy(collision.gameObject);
        }
    }
}
