﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopperProjectileController : MonoBehaviour
{
    public float globalDirection = -1;
    public float shootVelocityDirection = 2.5f;
    public float shootVelocityUp = 2.5f;
    float revSpeed = 360f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(globalDirection * Vector2.right * shootVelocityDirection, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * shootVelocityUp, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        GetComponent<Rigidbody2D>().MoveRotation(GetComponent<Rigidbody2D>().rotation + revSpeed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
