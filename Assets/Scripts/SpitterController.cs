using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterController : MonoBehaviour
{
    public float publicDelay = 0f;//za drugačni delay pri streljanju ko je več enakih 
    public GameObject spitterProjectile;
    public GameObject projectileSpawn;
    float fireRate = 0.7f;

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
            GameObject projectile = Instantiate(spitterProjectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
            fireTime = Time.time + fireRate + publicDelay;

        }
    }
}
