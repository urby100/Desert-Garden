using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterProjectileController : MonoBehaviour
{
    public GameObject spitterPuddle;
    public float globalDirection = -1;
    float shootVelocity = 4f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(globalDirection * Vector2.right * shootVelocity, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            GameObject puddle = Instantiate(spitterPuddle,collision.contacts[0].point, spitterPuddle.transform.rotation);
        }
        else if (collision.gameObject.name == "Puddle")
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<SpitterPuddleController>().increaseSize = true;
        }
        Destroy(gameObject);
    }
}
