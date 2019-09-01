using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueProjectileScript : MonoBehaviour
{
    float upForce = 100f;
    float sideForce = 250f;
    float rotation = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * upForce);
        GetComponent<Rigidbody2D>().AddForce(-transform.right * sideForce);
        GetComponent<Rigidbody2D>().AddTorque(-rotation);
        Destroy(gameObject, 1.5f);
    }
}
