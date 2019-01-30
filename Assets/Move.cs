using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;
    float moveInput;

    float movementSpeed = 6f;

    bool jumpRequest;
    public bool crouchRequest;
    public bool tiredRequest;
    public bool hurtRequest;
    bool moveAfterHurt;


    float minXPosition = -2.5f;
    float maxXPosition = 1000f;
    float xPosition;


    float jumpVelocity = 9f;
    float jumpCounter = 0f;
    float fallMultiplier = 5f;
    float lowJumpMultiplier = 5f;
    int maxJumps = 1;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        tiredRequest = false;
        moveAfterHurt = false;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            hurtRequest= true;
            moveAfterHurt = true;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            tiredRequest = true;
        }else if (Input.GetKey(KeyCode.X))
        {
            tiredRequest = false;
        }
        if (tiredRequest)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpCounter++;
            jumpRequest = true;
        }
        if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.DownArrow)) {
            crouchRequest = true;
        } else if (Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.DownArrow)) {
            crouchRequest = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //better jump
        if (rb.velocity.y < 0 || hurtRequest)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }

        //move
        moveInput = Input.GetAxisRaw("Horizontal");
        if (crouchRequest && rb.velocity.y == 0)//če je na tleh in se hoče skloniti se ne sme premikati...
        {
            moveInput = 0;
        }
        if (tiredRequest || hurtRequest )//če je umrl ali bil zadet
        {
            moveInput = 0;
        }
        if (moveAfterHurt) {//počakam da je animacija ko je bil zadet končana
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
            moveAfterHurt = false;
            jumpCounter = 0;
            jumpRequest = false;
        }
    }
}
