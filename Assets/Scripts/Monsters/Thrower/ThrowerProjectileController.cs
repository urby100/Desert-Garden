using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerProjectileController : MonoBehaviour
{
    public float direction;

    Rigidbody2D rb;
    public float speed = 5f;
    public float aliveDelay=2f;
    public float aliveTime;
    float revSpeed = 720f;
    // Start is called before the first frame update
    void Start()
    {
        aliveTime = Time.time + aliveDelay;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > aliveTime)
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
