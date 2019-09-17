using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBirdFlowerCollider : MonoBehaviour
{
    GameObject BossBirdy;
    // Start is called before the first frame update
    void Start()
    {
        BossBirdy = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BossThrowerProjectile")
        {
            BossBirdy.GetComponent<BossBirdyController>().followFlower = true;
            BossBirdy.GetComponent<BossBirdyController>().flower = collision.gameObject;

        }
        else
        {
            BossBirdy.GetComponent<BossBirdyController>().followFlower = false;
        }
    }
}
