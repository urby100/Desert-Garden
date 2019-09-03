using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueProjectileScript : MonoBehaviour
{
    public GameObject player;
    public GameObject bluePointStopPushBackPoint;
    public GameObject cam;
    float upForce = 100f;
    float sideForce = 250f;
    float rotation = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * upForce);
        GetComponent<Rigidbody2D>().AddForce(-transform.right * sideForce);
        GetComponent<Rigidbody2D>().AddTorque(-rotation);
        
    }
    void TurnWindOn() {
        bluePointStopPushBackPoint.GetComponent<BluePotionPushScript>().setPush();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TurnWindOn();
        Destroy(gameObject);
    }
}
