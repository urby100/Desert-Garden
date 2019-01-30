using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingScript : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject wingsObject;
    Rigidbody2D wingsObjectRigidbody;


    bool shoot = false;
    float shootVelocityLeft = 20f;
    float shootVelocityUp = 1f;
    float shootTriggerX = 8.76f;
    float timeShot;
    bool shootOnce = false;
    float delayShot=2f;
    bool rotate = false;
    float revSpeed = -360.0f;

    // Start is called before the first frame update
    void Start()
    {

        wingsObjectRigidbody = wingsObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject.transform.position.x >= shootTriggerX && !shoot)
        {
            shoot = true;
            timeShot = Time.time + delayShot;
        }
    }
    void FixedUpdate()
    {
        if (shoot && Time.time>timeShot && !rotate && !shootOnce)
        {
            shootOnce = true;
            wingsObjectRigidbody.bodyType = RigidbodyType2D.Dynamic;
            //wingsObjectRigidbody.transform.position = new Vector2(wingsObjectRigidbody.transform.position.x+0.4f,5.2f);
            wingsObjectRigidbody.AddForce(Vector2.left * shootVelocityLeft, ForceMode2D.Impulse);
            wingsObjectRigidbody.AddForce(Vector2.up * shootVelocityUp, ForceMode2D.Impulse);
            rotate = true;
        }
        if (rotate) {
            wingsObjectRigidbody.MoveRotation(wingsObjectRigidbody.rotation + revSpeed * Time.fixedDeltaTime);
        }
        if (wingsObjectRigidbody.transform.position.x <= -20f) {
            rotate = false;
            wingsObjectRigidbody.bodyType = RigidbodyType2D.Static;

        }
    }
}
