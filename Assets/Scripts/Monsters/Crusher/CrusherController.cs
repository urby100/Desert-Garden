using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherController : MonoBehaviour
{
    public GameObject crusherBody;
    public GameObject projectile;
    public GameObject playerObject;
    public GameObject points;
    public float attackDelay = 2f;
    public float attackLast = 8f;
    public float projectileSpawnDelay = 0.2f;
    public float projectileSpeed = 2f;
    public bool attack = false;
    public bool neutral = false;

    float attackTime;
    float attackLastTime;
    float projectileSpawnTime;
    int counter = 0;

    Animator crusherAnimator;

    // Start is called before the first frame update
    void Start()
    {
        crusherAnimator = crusherBody.GetComponent<Animator>();
        attackTime = Time.time + attackDelay;
    }

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
        if (neutral) {
            return;
        }
        if (Time.time > attackTime && !attack) {
            crusherBody.GetComponent<PolygonCollider2D>().isTrigger = true;
            attack = true;
            projectileSpawnTime = Time.time + projectileSpawnDelay;
            attackLastTime = Time.time + attackLast;
        }
        if (attack && Time.time>attackLastTime) {
            crusherBody.GetComponent<PolygonCollider2D>().isTrigger = false;
            attack = false;
            counter = 0;
            attackTime = Time.time + attackDelay;
        }
        if (attack) {
            if (Time.time > projectileSpawnTime)
            {
                counter++;
                if (counter <= 12)
                {
                    GameObject tempProjectile = Instantiate(projectile);
                    tempProjectile.name = "CrusherProjectile";
                    tempProjectile.GetComponent<CrusherProjectileController>().attackLastTime = attackLastTime;
                    tempProjectile.GetComponent<CrusherProjectileController>().crusher = gameObject;
                    tempProjectile.GetComponent<CrusherProjectileController>().points = points;
                    tempProjectile.GetComponent<CrusherProjectileController>().speed = projectileSpeed;
                    projectileSpawnTime = Time.time + projectileSpawnDelay;
                    
                }
            }
        }
    }
}
