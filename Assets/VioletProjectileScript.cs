using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VioletProjectileScript : MonoBehaviour
{
    float upForce = 100f;
    float sideForce = 250f;
    float rotation = 10f;

    public GameObject stevies;
    float time = 2f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * upForce);
        GetComponent<Rigidbody2D>().AddForce(-transform.right * sideForce);
        GetComponent<Rigidbody2D>().AddTorque(-rotation);
    }
    void ChangeSteviesDirection()
    {
        foreach (Transform littleSteve in stevies.transform)
        {
            littleSteve.gameObject.GetComponent<LittleSteveBossController>().ChangeDirection(time);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeSteviesDirection();
        Destroy(gameObject);
    }
}
