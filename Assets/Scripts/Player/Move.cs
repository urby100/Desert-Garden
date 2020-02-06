using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Move : MonoBehaviour
{
    Rigidbody2D rb;
    float moveInput;
    public GameObject gm;
    Keybindings kb;
    public bool jump;
    bool jumpRequest;
    public bool crouchRequest;
    public bool tiredRequest;
    public bool hurtRequest;
    public bool sceneDontMoveRequest;
    public bool invincible = false;
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
        groundObjects = GameObject.FindGameObjectsWithTag("Ground");
        tiredRequest = false;
    }
    void Update()
    {
        if (tiredRequest)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                                                    new Vector3(transform.position.x, FindClosesGround().position.y + 1f, transform.position.z),
                                                    6 * Time.deltaTime);
            return;
        }
    }
    public GameObject[] groundObjects;
    Transform FindClosesGround()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in groundObjects)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

    void FixedUpdate()
    {
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
        else if (rb.velocity.y > 0 && !Input.GetKey(kb.jump))//normal jump
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = gravity;
        }
        if (tiredRequest || hurtRequest || sceneDontMoveRequest)
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
        if (crouchRequest && rb.velocity.y == 0)
        {
            moveInput = 0;
        }
        if (sceneDontMoveRequest)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);
        }
        //jump
        if (tiredRequest || hurtRequest)
        {
            return;
        }
        if (jumpRequest && jumpCounter <= maxJumps)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jump = true;
            jumpRequest = false;
        }
        if (rb.velocity.y == 0)
        {
            jumpCounter = 0;
            jumpRequest = false;
        }
    }
}
