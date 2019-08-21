using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorController : MonoBehaviour
{
    public GameObject TeleportTo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Guy") {
            collision.gameObject.transform.position = TeleportTo.transform.position;
            GameObject.Find("Main Camera").GetComponent<BossCameraController>().ypos = TeleportTo.transform.position.y+2;
        }
    }
}
