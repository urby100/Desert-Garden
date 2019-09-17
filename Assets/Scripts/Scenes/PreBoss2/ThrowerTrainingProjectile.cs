using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerTrainingProjectile : MonoBehaviour
{
    float direction;

    Rigidbody2D rb;
    float speed = 5f;
    float revSpeed = 720f;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, transform.localRotation.eulerAngles.z + revSpeed * Time.deltaTime));
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    public void setDirection(float d) {
        direction = d;
    }
    public void setSpeed(float s)
    {
        speed = s;
    }
}
