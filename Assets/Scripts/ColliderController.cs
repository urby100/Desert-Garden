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
        if (gameObject.GetComponent<Move>().hurtRequest) {//odstrani collider

        }else if (gameObject.GetComponent<Move>().tiredRequest) {//odstrani collider

        }else if (gameObject.GetComponent<Move>().crouchRequest) {//zmanjša collider
            upright.enabled = false;
            crouching.enabled = true;
        }else {
            upright.enabled = true;
            crouching.enabled = false;
        }
    }
}
