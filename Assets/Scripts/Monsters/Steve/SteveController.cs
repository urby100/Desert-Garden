using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveController : MonoBehaviour
{

    public GameObject steveBody;
    public GameObject playerObject;
    public GameObject steveProjectile;
    public GameObject spawnProjectile;
    public float littleSteveMovementSpeed = 3f;
    public float littleSteveShootVelocityUp = 2.5f;
    public float littleSteveRunningLasts = 3f;
    public float attackDelay = 4f;
    public bool neutral = false;
    
    public bool arrived = true;
    public bool attack = false;
    float attackTime;
    GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //obrni proti playerju
        if (playerObject.transform.position.x > steveBody.transform.position.x)
        {
            steveBody.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            steveBody.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        if (neutral) {
            steveBody.GetComponent<BoxCollider2D>().enabled = false;
            Physics2D.IgnoreCollision(playerObject.GetComponent<Collider2D>(), steveBody.GetComponent<Collider2D>());
            return;
        }
        if (arrived && attack)
        {
            attackTime = Time.time + attackDelay;
            attack = false;
        }
        if (Time.time > attackTime && !attack)
        {
            arrived = false;
            projectile = Instantiate(steveProjectile, spawnProjectile.transform.position, new Quaternion());
            projectile.name = "LittleSteve";
            projectile.GetComponent<SteveProjectileController>().steveBody = steveBody;
            projectile.GetComponent<SteveProjectileController>().playerObject = playerObject;
            projectile.GetComponent<SteveProjectileController>().steve = gameObject;
            projectile.GetComponent<SteveProjectileController>().movementSpeed = littleSteveMovementSpeed;
            projectile.GetComponent<SteveProjectileController>().shootVelocityUp = littleSteveShootVelocityUp;
            projectile.GetComponent<SteveProjectileController>().runningLasts = littleSteveRunningLasts;
            attack = true;
        }

    }
    

}
