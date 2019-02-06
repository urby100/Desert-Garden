using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherController : MonoBehaviour
{
    public GameObject crusherBody;
    public GameObject projectile;
    public GameObject points;
    public bool destroyProjectiles=false;
    
    float attackDelay = 2f;
    float attackTime;
    float attackLast = 6f;
    float attackLastTime;
    float projectileSpawnDelay = 0.4f;
    float projectileSpawnTime;
    bool attack = false;
    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        attackTime = Time.time + attackDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > attackTime && !attack) {
            destroyProjectiles = false;
            attack = true;
            projectileSpawnTime = Time.time + projectileSpawnDelay;
            attackLastTime = Time.time + attackLast;
        }
        if (attack && Time.time>attackLastTime) {
            attack = false;
            counter = 0;
            destroyProjectiles = true;
            attackTime = Time.time + attackDelay;
        }
        if (attack) {
            if (Time.time > projectileSpawnTime)
            {
                counter++;
                if (counter <= 12)
                {
                    GameObject tempProjectile = Instantiate(projectile);
                    tempProjectile.GetComponent<CrusherProjectileController>().crusher = gameObject;
                    tempProjectile.GetComponent<CrusherProjectileController>().points = points;
                    projectileSpawnTime = Time.time + projectileSpawnDelay;
                    
                }
            }
        }
    }
}
