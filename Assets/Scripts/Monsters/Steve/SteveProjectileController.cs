using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveProjectileController : MonoBehaviour
{
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
        if (playerObject.transform.position.x > steveBody.transform.position.x)
        {
            direction = 1;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            direction = -1;
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        
        rb.AddForce(Vector2.up * shootVelocityUp, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //better jump
        if (rb.velocity.y < 0 )
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
        else {
            if (neutral)
            {
                runningNeutralAnimation();
            }
            else {

            runningAnimation();
            }
            if (!running)
            {
                gameObject.transform.rotation = new Quaternion(0, gameObject.transform.rotation.y, 0, 0);
                rb.freezeRotation = true;
                runningTime = Time.time + runningLasts;
                running = true;
            }
        }
        //Debug.Log("RunningTime: "+runningTime + " , Time: "+Time.time+" , DirectionChange: "+ (runningTime - (runningLasts / 2)));
        //zamenjaj smer
        if (Time.time > (runningTime - (runningLasts / 2)) && running && !directionChange) {
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
        float dist = Vector2.Distance(steveBody.transform.position, transform.position);
        if (dist < 0.4f) {//če Steva zaliješ ni collisiona...

            steve.GetComponent<SteveController>().arrived = true;
            Destroy(gameObject);
        }
        if(directionChange && !jumpedBack && dist < 1.5f)
        {
            jumpedBack = true;
            rb.AddForce(Vector2.up * 2f *shootVelocityUp, ForceMode2D.Impulse);
        }
        if (jumpedBack)
        {
            inairNeutralAnimation();
            rb.freezeRotation = false;
            rb.MoveRotation(rb.rotation + revSpeed *15 * -direction * Time.deltaTime);
        }
        rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            inTheAir = false;
        }
        else if (collision.gameObject == steveBody) {
            steve.GetComponent<SteveController>().arrived = true;
            if (neutral) {

                steveBody.GetComponent<SteveAnimations>().thirst = steveBody.GetComponent<SteveAnimations>().maxThirst;
            }
            Destroy(gameObject);
        } else if (collision.gameObject.name == "WaterProjectile")
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
    void inairAnimation() {
        animator.SetBool("running",false);
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
