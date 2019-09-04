using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraController : MonoBehaviour
{
    public GameObject cameraPoints;
    List<Transform> CameraPositions = new List<Transform>();
    public int positionName;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform t in cameraPoints.transform)
        {
            CameraPositions.Add(t);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(0, CameraPositions[positionName - 1].transform.position.y, transform.position.z);
    }
    public void setPosition(int positionInt)
    {
        positionName= positionInt;
    }
}
