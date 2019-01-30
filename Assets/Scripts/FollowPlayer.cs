using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerObject;
    float smooth = 7f;
    float minXPosition = 2.5f;
    float maxXPosition = 1000f;
    float dynamicPosXmin;
    float dynamicPosXmax;
    float xPosition;
    // Start is called before the first frame update
    void Start()
    {

        dynamicPosXmin=minXPosition;
        dynamicPosXmax=maxXPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xPosition = Mathf.Clamp(playerObject.transform.position.x, dynamicPosXmin, dynamicPosXmax);
        transform.position = Vector3.Lerp(transform.position, 
                                    new Vector3(xPosition, 3.5f, -1), 
                                    smooth * Time.deltaTime);
    }
}
