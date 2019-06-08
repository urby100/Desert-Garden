using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShadowAngle : MonoBehaviour
{
    public GameObject ShadowMin;
    public GameObject sunPoint;
    float shadowAngleMultiplier = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(sunPoint.transform.position.x- ShadowMin.transform.position.x,0,0);
            //new Vector3(shadowAngleMultiplier*(sunPoint.transform.position.x+ShadowMin.transform.position.x), 0,0);
        Debug.Log(ShadowMin.transform.position.x + " "+sunPoint.transform.position.x);
    }
}
