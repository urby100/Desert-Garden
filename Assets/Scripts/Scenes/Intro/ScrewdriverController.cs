﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverController : MonoBehaviour
{
    public GameObject signStage1;
    public bool goToSign=false;
    bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (goToSign && !done) {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            gameObject.transform.parent = signStage1.transform;
            gameObject.transform.localPosition = signStage1.transform.GetChild(0).transform.localPosition;
            gameObject.transform.localRotation = signStage1.transform.GetChild(0).transform.localRotation;
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            done = true;
        }
    }
}