using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopperController : MonoBehaviour
{
    float globalDelay = 0f;//za drugačni delay pri streljanju ko je več enakih 
    public GameObject stopperBody;
    public GameObject stopperProjectile;
    public GameObject projectileSpawn;
    float fireRate = 0.7f;

    float fireTime;
    float popTime;
    float popDelay = 0f;
    Vector3 targetUp;
    Vector3 targetDown;
    Vector3 velocity = Vector3.zero;
    float moveSpeedUp = 0.04f;
    bool outside = false;
    // Start is called before the first frame update
    void Start()
    {
        popTime = Time.time + popDelay;
        targetUp = stopperBody.transform.position + new Vector3(0f, 0.7f, 0f);
        targetDown = stopperBody.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > popTime && !outside)
        {
            stopperBody.transform.position = Vector3.SmoothDamp(stopperBody.transform.position,
                                                                targetUp,
                                                                ref velocity,
                                                                moveSpeedUp);
            
            
        }
        if (stopperBody.transform.position==targetUp && !outside) {
            outside = true;
            fireTime = Time.time + fireRate + globalDelay;
        }
        if (Time.time > fireTime && outside) {
            float dir = 1;
            for (int i = 0; i < 2; i++) {
                GameObject projectile = Instantiate(stopperProjectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
                projectile.GetComponent<StopperProjectileController>().globalDirection = 1f*dir;
                dir = dir * (-1);
            }
            fireTime = Time.time + fireRate + globalDelay;

        }
    }
}
