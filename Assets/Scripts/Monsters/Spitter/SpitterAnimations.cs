using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterAnimations : MonoBehaviour
{
    public GameObject spitterGameObject;
    SpitterController sc;
    Animator animator;

    float attackFrameLasts = 0.1f;
    float attackFrameLastsTime;
    bool attackFrame = false;
    // Start is called before the first frame update
    void Start()
    {
        sc = spitterGameObject.GetComponent<SpitterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attackFrame) {
            Attack();
        }
        if (sc.Neutral)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            Neutral();
        }
        if (!sc.Neutral && !attackFrame)
        {
            Monster();
        }
        if (sc.Attack && !attackFrame) {
            attackFrame = true;
            attackFrameLastsTime = Time.time + attackFrameLasts;
            
        }
        if (Time.time > attackFrameLastsTime && attackFrame) {
            attackFrame = false;
            sc.Attack = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "CrusherProjectile")
        {
            sc.Neutral = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "ThrowerProjectile")
        {
            sc.Neutral = true;
        }
        if (collision.gameObject.name == "LittleSteve")
        {
            sc.Neutral = true;
        }
        if (collision.gameObject.name == "StopperProjectile")
        {
            sc.Neutral = true;
        }
    }
    void Attack() {
        animator.SetBool("Attack",true);
        animator.SetBool("Monster", false);
        animator.SetBool("Neutral", false);
    }
    void Monster()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Monster", true);
        animator.SetBool("Neutral", false);

    }
    void Neutral()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Monster", false);
        animator.SetBool("Neutral", true);

    }
}
