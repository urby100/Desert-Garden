using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStopperProjectileController : MonoBehaviour
{
    public float dir;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed*dir*Time.deltaTime, 0), Space.World);
        transform.Rotate(0, 0, dir*10, Space.Self);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "StopperBody") {
            collision.gameObject.transform.parent.gameObject.GetComponent<BossStopperController>().setAttackReceiveAnim();
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Guy") {
            Destroy(gameObject);
        }
    }
}
