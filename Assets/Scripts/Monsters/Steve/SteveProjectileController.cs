using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveProjectileController : MonoBehaviour
{
    public GameObject sandEffect;
    bool landEffect = false;
    bool jumpEffect = false;
    public bool onPlayer = false;

    public GameObject steveBody;
    public GameObject playerObject;
    public GameObject steve;

    public float movementSpeed = 3f;
    public float shootVelocityUp = 2.5f;
    public float runningLasts = 3f;
    public bool neutral = false;

    Animator animator;
    Rigidbody2D rb;
    float direction = 1;
    float revSpeed = 70f;
    bool inTheAir = true;
    float fallMultiplier = 5f;
    float runningTime;
    bool running = false;
    bool jumpedBack = false;
    bool directionChange = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (onPlayer)
        {
            if (playerObject.transform.rotation == new Quaternion(0, 1, 0, 0))
            {
                direction = -1;
                transform.rotation = new Quaternion(0, 1, 0, 0);
            }
            else if (playerObject.transform.rotation == new Quaternion(0, 0, 0, 1))
            {
                direction = 1;
                transform.rotation = new Quaternion(0, 0, 0, 1);
            }
        }
        else
        {
            if (playerObject.transform.position.x > steveBody.transform.position.x)
            {
                direction = 1;
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                direction = -1;
                transform.rotation = new Quaternion(0, 1, 0, 0);
            }
        }

        rb.AddForce(Vector2.up * shootVelocityUp, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //better jump
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
        if (inTheAir)
        {
            if (neutral)
            {
                inairNeutralAnimation();
            }
            else
            {
                inairAnimation();
            }
            rb.MoveRotation(rb.rotation + revSpeed * -direction * Time.deltaTime);
        }
        else
        {
            if (neutral)
            {
                runningNeutralAnimation();
            }
            else
            {
                runningAnimation();
            }
            if (!running)
            {
                if (direction == 1)
                {
                    gameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
                }
                else
                {
                    gameObject.transform.rotation = new Quaternion(0, 1, 0, 0);
                }
                rb.freezeRotation = true;
                runningTime = Time.time + runningLasts;
                running = true;
            }
        }
        //Debug.Log("RunningTime: "+runningTime + " , Time: "+Time.time+" , DirectionChange: "+ (runningTime - (runningLasts / 2)));
        //zamenjaj smer
        if (Time.time > (runningTime - (runningLasts / 2)) && running && !directionChange)
        {
            if (onPlayer)
            {//če ga uporabi player gre samo do polovice smeri
                Destroy(gameObject);
            }
            directionChange = true;
            direction = (-1) * direction;
            if (direction == 1)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }

        }
        float dist = 2;
        if (!onPlayer)
        {
            dist = Vector2.Distance(steveBody.transform.position, transform.position);
        }
        if (dist < 0.4f && directionChange)
        {//če Steva zaliješ ni collisiona...

            steve.GetComponent<SteveController>().arrived = true;
            Destroy(gameObject);
        }
        if (directionChange && !jumpedBack && dist < 1.5f)
        {

            if (!jumpEffect)
            {
                var em = sandEffect.GetComponent<ParticleSystem>().emission;
                em.rateOverTime = 200;
                var gm = sandEffect.GetComponent<ParticleSystem>().main.gravityModifier;
                gm.constant = 1f;
                var sh = sandEffect.GetComponent<ParticleSystem>().shape;
                sh.shapeType = ParticleSystemShapeType.Cone;
                GameObject particle = Instantiate(sandEffect, gameObject.transform.position + new Vector3(0, -0.25f, 0), gameObject.transform.rotation);
                particle.name = "JumpEffectLittleSteve";
                particle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Destroy(particle, 0.4f);
                jumpEffect = true;
            }
            jumpedBack = true;
            rb.AddForce(Vector2.up * 2f * shootVelocityUp, ForceMode2D.Impulse);
        }
        if (jumpedBack)
        {
            inairNeutralAnimation();
            rb.freezeRotation = false;
            rb.MoveRotation(rb.rotation + revSpeed * 15 * -direction * Time.deltaTime);
        }
        rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            //land effect
            if (!landEffect)
            {
                var em = sandEffect.GetComponent<ParticleSystem>().emission;
                em.rateOverTime = 100 + Mathf.Clamp(collision.relativeVelocity.magnitude - 10, 1, 6) * 20;
                var gm = sandEffect.GetComponent<ParticleSystem>().main.gravityModifier;
                gm.constant = Random.Range(1.5f, 2);
                var sh = sandEffect.GetComponent<ParticleSystem>().shape;
                sh.shapeType = ParticleSystemShapeType.Sphere;
                GameObject particle = Instantiate(sandEffect, collision.contacts[0].point, gameObject.transform.rotation);
                particle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                particle.name = "LandEffectLittleSteve";
                Destroy(particle, 0.4f);
                landEffect = true;
            }

            inTheAir = false;
        }
        if (onPlayer)
        {
            return;
        }
        if (collision.gameObject == steveBody)
        {
            steve.GetComponent<SteveController>().arrived = true;
            if (neutral)
            {

                steveBody.GetComponent<SteveAnimations>().thirst = steveBody.GetComponent<SteveAnimations>().maxThirst;
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.name == "WaterProjectile")
        {
            Physics2D.IgnoreCollision(playerObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            if (!directionChange)
            {
                directionChange = true;
                direction = (-1) * direction;
                if (direction == 1)
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                else
                {
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                }
            }
            runningNeutralAnimation();
            neutral = true;
        }
    }
    void inairAnimation()
    {
        animator.SetBool("running", false);
        animator.SetBool("inair", true);
        animator.SetBool("neutral-running", false);
        animator.SetBool("neutral-inair", false);
    }
    void runningAnimation()
    {
        animator.SetBool("running", true);
        animator.SetBool("inair", false);
        animator.SetBool("neutral-running", false);
        animator.SetBool("neutral-inair", false);
    }
    void runningNeutralAnimation()
    {
        animator.SetBool("running", false);
        animator.SetBool("inair", false);
        animator.SetBool("neutral-running", true);
        animator.SetBool("neutral-inair", false);
    }
    void inairNeutralAnimation()
    {
        animator.SetBool("running", false);
        animator.SetBool("inair", false);
        animator.SetBool("neutral-running", false);
        animator.SetBool("neutral-inair", true);
    }

}
