using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopperTrainingProjectileController : MonoBehaviour
{
    public float globalDirection = -1;
    float revSpeed = 360f;
    float[,] throwingPower = new float[,] {
        { 1.5f, 6 },
        { 2, 5 },
        {2.5f, 5 },
        { 2.7f, 5 },
        { 3.5f, 4.8f },
        { 2, 4.5f }
    };
    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, throwingPower.GetLength(0));
        //* 0.8f scaling ...
        
        GetComponent<Rigidbody2D>().AddForce(globalDirection * Vector2.right * throwingPower[i,0]*0.8f, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * throwingPower[i, 1] *0.9f, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddTorque(-3, ForceMode2D.Force);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

       // GetComponent<Rigidbody2D>().MoveRotation(GetComponent<Rigidbody2D>().rotation + revSpeed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground Collider Top") {
            Destroy(gameObject);
        }
    }
}
