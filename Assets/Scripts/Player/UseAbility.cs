using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : MonoBehaviour
{
    public GameObject projectileSpawn;
    public GameObject projectilePrefab;
    
    public bool useWater = false;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            useWater=true;
        }
    }
    private void FixedUpdate()
    {
        if (useWater) {
            GameObject projectile = Instantiate(projectilePrefab,projectileSpawn.transform.position,new Quaternion());
            projectile.name = "WaterProjectile";
            if (transform.rotation == new Quaternion(0, 180, 0, 0))
            {
                projectile.GetComponent<WaterProjectile>().direction = -1;
            }
            else if (transform.rotation == new Quaternion(0, 0, 0, 0))
            {
                projectile.GetComponent<WaterProjectile>().direction = 1;
            }
        }
    }
}
