﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopperController : MonoBehaviour
{
    public float spawnPopUpDelay = 0f;
    public float popDelayUp = 0.7f;
    public float popDelayDown = 0.7f;
    public float moveSpeed = 0.04f;
    public GameObject playerObject;
    public GameObject popperBody;
    public bool neutral = false;
    float popTime;
    public bool moveIt = true;
    public bool direction = true;// gor ali dol
    Vector3 targetUp ;
    Vector3 targetDown;
    Vector3 velocity = Vector3.zero;
    //obrnjen proti playerju

    // Start is called before the first frame update
    void Start()
    {
        popTime = Time.time+ spawnPopUpDelay + popDelayUp;
        targetUp = popperBody.transform.position+ new Vector3(0f,1f,0f);
        targetDown = popperBody.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //obrni proti playerju
        if (playerObject.transform.position.x > popperBody.transform.position.x)
        {
            popperBody.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            popperBody.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        if (Time.time > popTime && !neutral) {
            if (direction)//more iti gor
            {
                popTime = Time.time  + popDelayUp;
            }
            else//more iti dol
            {
                popTime = Time.time  + popDelayDown;
            }
            direction = !direction;
            moveIt = true;
        }
        if (moveIt) {
            move();
        }
    }
    public void move() {
        if (direction)
        {
            popperBody.transform.position = Vector3.SmoothDamp(popperBody.transform.position,
                                                                targetUp,
                                                                ref velocity,
                                                                moveSpeed);
        }
        else
        {
            popperBody.transform.position = Vector3.SmoothDamp(popperBody.transform.position,
                                                                targetDown,
                                                                ref velocity,
                                                                moveSpeed);
        }
        
    }
}