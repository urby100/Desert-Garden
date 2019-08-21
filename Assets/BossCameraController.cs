using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraController : MonoBehaviour
{
    public GameObject cameraPoints;
    List<Transform> CameraPositions=new List<Transform>();
    public float  ypos;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform t in cameraPoints.transform) {
            CameraPositions.Add(t);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float closestDiff= Mathf.Abs(CameraPositions[0].transform.position.y - ypos);
        Transform closest = CameraPositions[0];
        for (int i = 1; i < CameraPositions.Count; i++)
        {
            if (Mathf.Abs(CameraPositions[i].transform.position.y - ypos) < closestDiff) {
                closestDiff = Mathf.Abs(CameraPositions[i].transform.position.y - ypos);
                 closest = CameraPositions[i];
            }
        }
        transform.position = new Vector3(0, closest.transform.position.y,transform.position.z) ;

    }
}
