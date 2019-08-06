using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdyAnimations : MonoBehaviour
{
    public GameObject landEffect;
    float prev_vel;
    public GameObject birdyGameObject;
    public GameObject playerObject;
    Rigidbody2D birdyBodyRb;
    Animator animator;
    public bool NeutralBool = false;
    bool CollidersSet = false;
    // Start is called before the first frame update
    void Start()
    {
        birdyBodyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (NeutralBool)
        {
            Neutral();
            birdyGameObject.GetComponent<BirdyController>().neutral = true;
            if (!CollidersSet)
            {
                if (birdyBodyRb.velocity.y != 0)
                {
                    transform.position = new Vector3(transform.position.x, 0.5f, 0);
                }
                birdyBodyRb.velocity=new Vector2(0,0);
                birdyBodyRb.bodyType = RigidbodyType2D.Kinematic;
                GetComponent<PolygonCollider2D>().enabled = false;
                CollidersSet = true;
            }
        }
        else
        {
            if (birdyBodyRb.velocity.y != 0)
            {
                Attack();
            }
            else
            {
                Monster();
            }
        }
    }
    void FixedUpdate()
    {
        //jump effect
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 0 && prev_vel == 0)
        {
            var em = landEffect.GetComponent<ParticleSystem>().emission;
            em.rateOverTime = 100;
            var gm = landEffect.GetComponent<ParticleSystem>().main.gravityModifier;
            gm.constant = 1f;
            var sh = landEffect.GetComponent<ParticleSystem>().shape;
            sh.shapeType = ParticleSystemShapeType.Cone;
            GameObject particle = Instantiate(landEffect, gameObject.transform.position + new Vector3(0, -0.5f, 0), gameObject.transform.rotation);
            particle.name = "JumpEffectBirdy";
            Destroy(particle, 0.4f);
        }
        prev_vel = gameObject.GetComponent<Rigidbody2D>().velocity.y;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "CrusherProjectile")
        {
            NeutralBool = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //land effect
        if (collision.gameObject.name == "Ground")
        {
            var em = landEffect.GetComponent<ParticleSystem>().emission;
            em.rateOverTime = 100 + Mathf.Clamp(collision.relativeVelocity.magnitude - 10, 1, 6) * 20;
            var gm = landEffect.GetComponent<ParticleSystem>().main.gravityModifier;
            gm.constant = Random.Range(1.5f, 2);
            var sh = landEffect.GetComponent<ParticleSystem>().shape;
            sh.shapeType = ParticleSystemShapeType.Sphere;
            GameObject particle = Instantiate(landEffect, collision.contacts[0].point, gameObject.transform.rotation);
            particle.name = "LandEffectBirdy";
            Destroy(particle, 0.4f);
        }
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
        animator.SetBool("Attack", false);
    }
    void Neutral()
    {
        animator.SetBool("Monster", false);
        animator.SetBool("Neutral", true);
        animator.SetBool("Attack", false);
    }
    void Attack()
    {
        animator.SetBool("Monster", false);
        animator.SetBool("Neutral", false);
        animator.SetBool("Attack", true);
    }
}
