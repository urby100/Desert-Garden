using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public GameObject stuffObject;
    public GameObject wingsObject;

    Rigidbody2D wingsObjectRigidbody;
    Rigidbody2D stuffObjectRigidbody;


    public bool shoot = false;
    
    float shootVelocityLeft = 20f;
    float shootVelocityRight = 20f;
    float shootVelocityUp = 10f;
    bool shootOnce = false;
    bool rotate = false;
    float revSpeed = -360.0f;

    // Start is called before the first frame update
    void Start()
    {

        wingsObjectRigidbody = wingsObject.GetComponent<Rigidbody2D>();
        stuffObjectRigidbody = stuffObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void FixedUpdate()
    {
        if (shoot && !shootOnce)
        {

            shootOnce = true;
            stuffObjectRigidbody.bodyType = RigidbodyType2D.Dynamic;
            stuffObjectRigidbody.AddForce(Vector2.right * shootVelocityRight, ForceMode2D.Impulse);
            stuffObjectRigidbody.AddForce(Vector2.up * shootVelocityUp, ForceMode2D.Impulse);

            wingsObjectRigidbody.bodyType = RigidbodyType2D.Dynamic;
            wingsObjectRigidbody.AddForce(Vector2.left * shootVelocityLeft, ForceMode2D.Impulse);
            wingsObjectRigidbody.AddForce(Vector2.up, ForceMode2D.Impulse);

            rotate = true;
        }
        if (rotate)
        {
            wingsObjectRigidbody.MoveRotation(wingsObjectRigidbody.rotation + revSpeed * Time.fixedDeltaTime);
        }
        if (wingsObjectRigidbody.transform.position.x <= -20f)
        {
            rotate = false;
            wingsObjectRigidbody.bodyType = RigidbodyType2D.Static;
            stuffObjectRigidbody.bodyType = RigidbodyType2D.Static;
            wingsObject.SetActive(false);
            stuffObject.SetActive(false);

        }
    }
}
