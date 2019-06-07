using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;
    float moveInput;
    public GameObject gm;
    Keybindings kb;

    bool jumpRequest;
    public bool crouchRequest;
    public bool tiredRequest;
    public bool hurtRequest;
    public bool sceneDontMoveRequest;


    float minXPosition = -2.5f;
    float maxXPosition = 1000f;
    float xPosition;


    public float movementSpeed = 6f;
    public float jumpVelocity = 7f;
    public float fallMultiplier = 10f;
    public float lowJumpMultiplier = 10f;
    public float gravity = 1f;
    float jumpCounter = 0f;
    int maxJumps = 1;
    

    void Awake()
    {
        kb = gm.GetComponent<Keybindings>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        tiredRequest = false;
    }
    void Update()
    {
       
        if (tiredRequest)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x,0.5f), 6*Time.deltaTime);
            return;
        }
    }

   
    void FixedUpdate()
    {
        /* if (Input.GetKeyDown(KeyCode.T)) {
            Time.timeScale = Time.timeScale+0.1f;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale = Time.timeScale-0.1f;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            tiredRequest = true;
        }else if (Input.GetKey(KeyCode.X))
        {
            tiredRequest = false;
        }*/
        if (Input.GetKey(kb.jump))
        {
            jumpCounter++;
            jumpRequest = true;
        }
        if (Input.GetKey(kb.crouch))
        {
            crouchRequest = true;
        }
        else
        {
            crouchRequest = false;
        }
        //move
        moveInput = 0;
        if (Input.GetKey(kb.forward))
        {
            moveInput += 1;
        }
        if (Input.GetKey(kb.backward))
        {
            moveInput -= 1;
        }
        //better jump
        if (rb.velocity.y < 0 || hurtRequest)//falling 
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))//normal jump
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = gravity;
        }
        if (tiredRequest || hurtRequest || sceneDontMoveRequest)//če je umrl ali bil zadet ali je v sceni
        {
            moveInput = 0;
        }
        if (moveInput == -1)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (moveInput == 1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
        }
        if (crouchRequest && rb.velocity.y == 0)//če je na tleh in se hoče skloniti se ne sme premikati...
        {
            moveInput = 0;
        }
        if ((transform.position.x <=minXPosition && moveInput==-1) 
            || (transform.position.x >= maxXPosition && moveInput==1))
        {//limitiram premik levo in desno
            moveInput = 0;
        }
        rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);
        //jump

        if (tiredRequest || hurtRequest)
        {

            return;
        }
        if (jumpRequest && jumpCounter <= maxJumps)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        }
        if (rb.velocity.y == 0)
        {
            jumpCounter = 0;
            jumpRequest = false;
        }
    }
}
