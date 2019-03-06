using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProjectile : MonoBehaviour
{
    public float direction=1;
    public float horizontalVelocity = 2.5f;
    public float verticalVelocity = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Rigidbody2D>().AddForce(direction * Vector2.right * horizontalVelocity, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * verticalVelocity, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
