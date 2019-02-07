using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveProjectileController : MonoBehaviour
{
    public GameObject steveBody;
    public GameObject playerObject;
    public GameObject steve;


    Animator animator;
    SpriteRenderer sr;
    CapsuleCollider2D cc2d;
    Rigidbody2D rb;
    float direction = 1;
    float movementSpeed = 3f;
    float shootVelocityUp = 2.5f;
    float revSpeed = 720f;
    bool inTheAir = true;
    float fallMultiplier = 5f;
    float runningLasts = 3f;
    float runningTime;
    bool running = false;
    bool jumpedBack = false;
    bool directionChange = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "LittleSteve";
        sr = GetComponent<SpriteRenderer>();
        cc2d = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (playerObject.transform.position.x > steveBody.transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
            sr.flipX = true;
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
            inairAnimation();
            rb.MoveRotation(rb.rotation + revSpeed *-direction* Time.deltaTime);
        }
        else {
            runningAnimation();
            if (!running)
            {
                cc2d.offset = new Vector2(cc2d.offset.x, -0.1551523f);
                cc2d.size = new Vector2(cc2d.size.x, 0.6914924f);
                gameObject.transform.rotation = new Quaternion(0, gameObject.transform.rotation.y, 0, 0);
                rb.freezeRotation = true;
                runningTime = Time.time + runningLasts;
                running = true;
            }
        }
        Debug.Log(direction);
        //Debug.Log("RunningTime: "+runningTime + " , Time: "+Time.time+" , DirectionChange: "+ (runningTime - (runningLasts / 2)));
        if (Time.time > (runningTime - (runningLasts / 2)) && running && !directionChange) {
            directionChange = true;
            direction = (-1) * direction;
            sr.flipX = !sr.flipX;
            cc2d.offset = new Vector2(-1*cc2d.offset.x, cc2d.offset.y);

        }
        float dist = Vector2.Distance(steveBody.transform.position, transform.position);
        if(directionChange && !jumpedBack && dist < 1.2f)
        {
            jumpedBack = true;
            rb.AddForce(Vector2.up * 2f *shootVelocityUp, ForceMode2D.Impulse);
        }
        if (jumpedBack)
        {
            rb.freezeRotation = false;
            rb.MoveRotation(rb.rotation + revSpeed *4 * -direction * Time.deltaTime);
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
            Destroy(gameObject);
        }
    }
    void inairAnimation() {
        animator.SetBool("running",false);
        animator.SetBool("inair", true);
    }
    void runningAnimation()
    {
        animator.SetBool("running", true);
        animator.SetBool("inair", false);
    }

}
