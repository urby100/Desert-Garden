using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraController : MonoBehaviour
{
    public GameObject cameraPoints;
    List<Transform> CameraPositions = new List<Transform>();
    public string positionName;
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
    }
    public void setPosition(int positionInt)
    {
        positionName=positionInt.ToString();
        transform.position = new Vector3(0, CameraPositions[positionInt - 1].transform.position.y, transform.position.z);
    }
}
