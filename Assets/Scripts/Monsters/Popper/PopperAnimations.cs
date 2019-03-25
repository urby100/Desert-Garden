using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopperAnimations : MonoBehaviour
{
    GameObject popperGameobject;
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
        popperGameobject = transform.parent.gameObject;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (neutralBool) {
            neutral();
        }
        if (thirst >= maxThirst && !neutralBool && !satisfiedBool) {
            popperGameobject.GetComponent<PopperController>().neutral = true;
            if (!popperGameobject.GetComponent<PopperController>().direction) {
                popperGameobject.GetComponent<PopperController>().move();
            }
            GetComponent<PolygonCollider2D>().enabled = false;
            satisfiedBool = true;
            satisfiedTime = Time.time + satisfiedLasts;
            satisfied();
        }
        if (Time.time>satisfiedTime && satisfiedBool && !neutralBool) {
            neutralBool = true;
        }
    }
    void outside()
    {
        animator.SetBool("outside", true);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", false);
    }
    void neutral()
    {
        animator.SetBool("outside", false);
        animator.SetBool("neutral", true);
        animator.SetBool("satisfied", false);
    }
    void satisfied()
    {
        animator.SetBool("outside", false);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "WaterProjectile") {
            thirst++;
        }
    }
}
