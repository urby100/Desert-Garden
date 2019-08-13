using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerAnimations : MonoBehaviour
{
    public GameObject playerObject;
    Animator animator;
    public bool NeutralBool = false;
    bool CollidersSet = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NeutralBool)
        {
            if (!CollidersSet)
            {

                GetComponent < Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                GetComponent<PolygonCollider2D>().enabled = false;
                foreach (Collider2D c2d in playerObject.GetComponents<Collider2D>())
                {
                    Physics2D.IgnoreCollision(c2d, GetComponent<Collider2D>());
                }
                CollidersSet = true;
            }
            
            Neutral();
        }
        else {
            Monster();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "CrusherProjectile")
        {
            if (!collision.gameObject.GetComponent<CrusherProjectileController>().onPlayer)
            {
                NeutralBool = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "ThrowerProjectile")
        {
            NeutralBool = true;
        }
        if (collision.gameObject.name == "LittleSteve")
        {
            NeutralBool = true;
        }
        if (collision.gameObject.name == "StopperProjectile")
        {
            NeutralBool = true;
        }
    }
    void Monster()
    {
        animator.SetBool("Monster", true);
        animator.SetBool("Neutral", false);
    }
    void Neutral()
    {
        animator.SetBool("Monster", false);
        animator.SetBool("Neutral", true);
    }
}
