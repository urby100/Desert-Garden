using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    public GameObject planeObject;
    public GameObject planeStartPoint;
    public GameObject planeEndPoint;

    float planeRespawnTime;
    float planeRespawnDelay=5f;
    float planeSpeed = 2f;
    bool resetPlane = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time> planeRespawnTime && resetPlane)
        {
            resetPlane = false;
            planeObject.transform.position = planeStartPoint.transform.position;
        }
        planeObject.transform.position =
            Vector3.MoveTowards(planeObject.transform.position, planeEndPoint.transform.position, planeSpeed * Time.deltaTime);
        if (planeObject.transform.position == planeEndPoint.transform.position && !resetPlane) {
            resetPlane = true;
            planeRespawnTime = Time.time + planeRespawnDelay;
        }
    }
}
