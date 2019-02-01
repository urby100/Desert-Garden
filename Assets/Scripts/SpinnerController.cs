using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerController : MonoBehaviour
{
    public GameObject spinnerBody;
    Rigidbody2D spinnerBodyRb;
    HingeJoint2D pivotPoint;
    float direction = -1f;
    float minForce=100f;
    float force = 100f;
    float wingFlapRate = 0.4f;
    float wingFlapDelay;
    float multiplier = 1f;
    // Start is called before the first frame update
    void Start()
    {
        pivotPoint = spinnerBody.GetComponent<HingeJoint2D>();
        spinnerBodyRb = spinnerBody.GetComponent<Rigidbody2D>();
        wingFlapDelay = Time.time + wingFlapRate;
        spinnerBodyRb.AddForce(4*force * direction * spinnerBody.transform.right * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > wingFlapDelay) {
            spinnerBodyRb.AddForce(force * direction * spinnerBody.transform.right * Time.deltaTime, ForceMode2D.Impulse);
            wingFlapDelay = Time.time + wingFlapRate;
        }
        float normalizedAngle =pivotPoint.jointAngle/360-Mathf.Floor(pivotPoint.jointAngle / 360);
        if (normalizedAngle * 360 > 0 && normalizedAngle * 360 < 180)//ko gre gor rabi več moči
        {
            if (pivotPoint.jointSpeed < 120)
            {
                wingFlapRate = 0.3f;
                multiplier = 2f;
            }
            else if (pivotPoint.jointSpeed>130)
            {
                wingFlapRate = 0.7f;
                multiplier = 1f;

            }
            else
            {
                wingFlapRate = 0.5f;
                multiplier = 1.2f;
            }
            force = minForce * multiplier;
        }
        else {
            force = 1;
            wingFlapRate = 1f;
        }
        Debug.Log("Angle: " + normalizedAngle * 360 + " -Speed: " + pivotPoint.jointSpeed + " -Velocity: " + spinnerBodyRb.velocity);
    }
}
