using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public GameObject stuffObject;
    public GameObject bombObject;
    public GameObject playerObject;
    public GameObject wingsObject;

    Rigidbody2D wingsObjectRigidbody;
    Rigidbody2D stuffObjectRigidbody;


    bool shoot = false;
    float shootVelocityLeft = 20f;
    float shootVelocityRight = 20f;
    float shootVelocityUp = 10f;
    float shootTriggerX = 8.76f;
    float timeShot;
    bool shootOnce = false;
    float delayShot = 2f;
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
        if (playerObject.transform.position.x >= shootTriggerX && !shoot)
        {
            shoot = true;
            timeShot = Time.time + delayShot;
        }
        if (shoot && Time.time < timeShot) {
            bombObject.GetComponent<SpriteRenderer>().color = bombObject.GetComponent<SpriteRenderer>().color+ new Color(Time.deltaTime,0,0);
        }
    }
    void FixedUpdate()
    {
        if (shoot && Time.time > timeShot && !rotate && !shootOnce)
        {
            bombObject.SetActive(false);

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
