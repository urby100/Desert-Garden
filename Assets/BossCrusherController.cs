using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrusherController : MonoBehaviour
{
    public GameObject crusherBody;
    public GameObject projectile;
    public GameObject playerObject;
    public GameObject points;
    public float attackLast = 4f;
    public float projectileSpawnDelay = 0.2f;
    public float projectileSpeed = 2f;
    public bool attack = false;
    public bool neutral = false;
    public bool giveAttack = true;

    float attackTime;
    public float attackLastTime;
    float projectileSpawnTime;
    public int counter = 0;
    public int maxProjectiles = 12;

    Animator crusherAnimator;

    // Start is called before the first frame update
    void Start()
    {
        crusherAnimator = crusherBody.GetComponent<Animator>();
    }
    public bool attackSet = false;
    public void setAttack(float delay) {
        attackTime = Time.time  + delay;
        attackSet = true;
    }
    public bool destroyProjectiles = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        //obrni proti playerju
        if (playerObject.transform.position.x > crusherBody.transform.position.x)
        {
            crusherBody.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            crusherBody.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        if (Time.time > attackTime && attackSet && !attack) {
            attack = true;
            projectileSpawnTime = Time.time + projectileSpawnDelay;
            attackLastTime = Time.time + attackLast;
        }

        if (attack && Time.time > attackLastTime)
        {
            attack = false;
            attackSet = false;
            counter = 0;
        }
        if (attack)
        {
            if (Time.time > projectileSpawnTime)
            {
                counter++;
                if (counter <= maxProjectiles)
                {
                    GameObject tempProjectile = Instantiate(projectile);
                    tempProjectile.name = "BossCrusherProjectile";
                    tempProjectile.GetComponent<BossCrusherProjectileController>().attackLastTime = attackLastTime;
                    tempProjectile.GetComponent<BossCrusherProjectileController>().crusher = gameObject;
                    tempProjectile.GetComponent<BossCrusherProjectileController>().points = points;
                    tempProjectile.GetComponent<BossCrusherProjectileController>().speed = projectileSpeed;
                    projectileSpawnTime = Time.time + projectileSpawnDelay;
                    
                }
            }
        }
    }
}
