using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public CapsuleCollider2D upright;
    public CapsuleCollider2D crouching;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameObject.GetComponent<Move>().tiredRequest) {

            GetComponent<CapsuleCollider2D>().enabled = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeRotation;
        }
        if (gameObject.GetComponent<Move>().hurtRequest) {//odstrani collider
            //iščem idejo
        }
        else if (gameObject.GetComponent<Move>().tiredRequest) {//odstrani collider
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else if (gameObject.GetComponent<Move>().crouchRequest) {//zmanjša collider
            upright.enabled = false;
            crouching.enabled = true;
        }else {
            upright.enabled = true;
            crouching.enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log(collision.gameObject.name);
    }
}
