using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    public GameObject player;
    float maxY = -0.5f;
    float jumpShadowMultiplier = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            maxY - jumpShadowMultiplier * (player.transform.position.y - 0.515f)
            , 0);
    }
}
