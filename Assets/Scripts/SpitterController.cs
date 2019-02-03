using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterController : MonoBehaviour
{
    public float publicDelay = 0f;//za drugačni delay pri streljanju ko je več enakih 
    public GameObject spitterProjectile;
    public GameObject spitterBody;
    public GameObject projectileSpawn;
    public GameObject playerObject;
    float fireRate = 0.7f;
    float shotDirection = -1f;
    float fireTime;
    // Start is called before the first frame update
    void Start()
    {
        fireTime = Time.time + fireRate+ publicDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > fireTime)
        {
            if (playerObject.transform.position.x> spitterBody.transform.position.x)
            {
                spitterBody.transform.rotation = new Quaternion(0, 180, 0, 0);
                shotDirection = 1f;
            }
            else
            {
                spitterBody.transform.rotation = new Quaternion(0, 0, 0, 0);
                shotDirection = -1f;
            }
            GameObject projectile = Instantiate(spitterProjectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
            projectile.GetComponent<SpitterProjectileController>().globalDirection = shotDirection;
            fireTime = Time.time + fireRate + publicDelay;

        }
    }
}
