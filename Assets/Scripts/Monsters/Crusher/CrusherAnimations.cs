using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherAnimations : MonoBehaviour
{
    GameObject crusherGameobject;
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

        crusherGameobject = transform.parent.gameObject;
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
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
            crusherGameobject.GetComponent<CrusherController>().neutral = true;
        }
        if (Time.time > satisfiedTime && satisfiedBool && !neutralBool)
        {
            neutralBool = true;
        }
        if (crusherGameobject.GetComponent<CrusherController>().attack && !neutralBool && !satisfiedBool)
        {
            attacking();
        }
        if(!crusherGameobject.GetComponent<CrusherController>().attack && !neutralBool && !satisfiedBool) {
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "WaterProjectile")
        {
            thirst++;
        }
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
