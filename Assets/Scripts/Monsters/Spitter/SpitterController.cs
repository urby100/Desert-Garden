using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterController : MonoBehaviour
{
    public GameObject spitterProjectile;
    public GameObject spitterBody;
    public GameObject projectileSpawn;
    public GameObject playerObject;
    public bool Attack = false;
    public bool Neutral = false;
    public float fireRate = 0.7f;
    public float shotDirection = -1f;
    float fireTime;
    // Start is called before the first frame update
    void Start()
    {
        fireTime = Time.time + fireRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerObject.transform.position.x > spitterBody.transform.position.x)
        {
            spitterBody.transform.rotation = new Quaternion(0, 180, 0, 0);
            shotDirection = 1f;
        }
        else
        {
            spitterBody.transform.rotation = new Quaternion(0, 0, 0, 0);
            shotDirection = -1f;
        }
        if (Neutral) {
            return;
        }
        if (Time.time > fireTime)
        {
            GameObject projectile = Instantiate(spitterProjectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
            projectile.GetComponent<SpitterProjectileController>().globalDirection = shotDirection;
            projectile.name="SpitterProjectile";
            Attack = true;
            
            fireTime = Time.time + fireRate;

        }
    }
}
