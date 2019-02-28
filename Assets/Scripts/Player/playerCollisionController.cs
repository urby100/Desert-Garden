using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollisionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PopperBody") {
            GetComponent<Move>().hurtRequest = true;
        }
        if (collision.gameObject.name == "StopperBody")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "CrusherBody")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "SteveBody")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "StopperProjectile")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "CrusherProjectile")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "LittleSteve")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "ThrowerProjectile")
        {
            GetComponent<Move>().hurtRequest = true;

        }
    }
}
