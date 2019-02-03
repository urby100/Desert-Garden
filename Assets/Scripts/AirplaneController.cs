using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public GameObject stuffObject;
    public GameObject bombObject;
    public GameObject playerObject;
    public GameObject wingsObject;
    public List<Sprite> bombSprites;

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
            bombObject.GetComponent<SpriteRenderer>().color = 
                new Color(bombObject.GetComponent<SpriteRenderer>().color.r,
                Mathf.Clamp(bombObject.GetComponent<SpriteRenderer>().color.g-(Time.deltaTime*0.4f*(timeShot-Time.time)),0,255),
                bombObject.GetComponent<SpriteRenderer>().color.b);
            
            //spremeni obraz bombe
            if ((timeShot - Time.time) < (delayShot/4*3) && 
                (timeShot - Time.time) > (delayShot / 4 * 2))//4 sličice, če delay 2s... more biti med 1.5s in 1s
            {
                bombObject.GetComponent<SpriteRenderer>().sprite = bombSprites[1];
            }
            else if ((timeShot - Time.time) < (delayShot / 4 * 2) &&
                (timeShot - Time.time) > (delayShot / 4 ))
            {
                bombObject.GetComponent<SpriteRenderer>().sprite = bombSprites[2];
            }
            else if ((timeShot - Time.time) < (delayShot / 4 ) &&
                (timeShot - Time.time) > 0f)
            {
                bombObject.GetComponent<SpriteRenderer>().sprite = bombSprites[3];
            }
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
