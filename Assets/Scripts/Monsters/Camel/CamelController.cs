using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamelController : MonoBehaviour
{
    public GameObject CamelBody;
    public GameObject ProjectileSpawnPoint;
    public GameObject ProjectilePrefab;
    public GameObject PlayerObject;

    public float attackSpeed = 2f;
    float attackTime;
    float shootDirection = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > attackTime)
        {
            if (PlayerObject.transform.position.x > CamelBody.transform.position.x)
            {
                CamelBody.transform.rotation = new Quaternion(0, 180, 0, 0);
                shootDirection = 1f;
            }
            else
            {
                CamelBody.transform.rotation = new Quaternion(0, 0, 0, 0);
                shootDirection = -1f;
            }
            GameObject projectile = Instantiate(ProjectilePrefab, ProjectileSpawnPoint.transform.position, ProjectileSpawnPoint.transform.rotation);
            projectile.GetComponent<CamelProjectileController>().direction = shootDirection;
            attackTime = Time.time + attackSpeed ;

        }
    }
}
