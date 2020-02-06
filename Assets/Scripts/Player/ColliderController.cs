using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public CapsuleCollider2D upright;
    public CapsuleCollider2D crouching;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameObject.GetComponent<Move>().tiredRequest) {

            GetComponent<CapsuleCollider2D>().enabled = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeRotation;
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
}
