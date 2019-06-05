using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFlowerScript : MonoBehaviour
{
    public int rotation=1;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, rotation*2, Space.Self);
    }
}
