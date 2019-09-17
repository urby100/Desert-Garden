using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowerProjectileController : MonoBehaviour
{
    float direction;
    public GameObject maxDistance;
    float maxDiff = 0.1f;

    Rigidbody2D rb;
    public float speed = 5f;

    float revSpeed = 720f;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.position.x > maxDistance.transform.position.x)
        {
            direction = -1;
        }
        else {
            direction = 1;
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Mathf.Abs(gameObject.transform.position.x-maxDistance.transform.position.x);
        if (distance>-maxDiff && distance< maxDiff)
        {
            Destroy(gameObject);
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, transform.localRotation.eulerAngles.z + revSpeed * Time.deltaTime));
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
