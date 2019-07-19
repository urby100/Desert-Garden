using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopperTraining : MonoBehaviour
{
    public GameObject projectileSpawn;
    public GameObject projectile;
    Animator animator;

    float fireRate = 1.5f;

    float fireTime;
    bool attackBool = false;
    float animationDelay;
    float animationTime;

    // Start is called before the first frame update
    void Start()
    {
        animationDelay = fireRate / 2;
        if (animationDelay > 0.4f)
        {
            animationDelay = 0.4f;
        }
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackBool && Time.time < animationTime)
        {
            Attack();
        }
        else
        {
            Normal();
            attackBool = false;
        }
        if (Time.time > fireTime)
        {
            Attack();
            attackBool = true;
            float dir = -1;
            GameObject p = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
            p.name = "StopperTrainingProjectile";
            p.GetComponent<StopperTrainingProjectileController>().globalDirection = dir;
            fireTime = Time.time + fireRate;
            animationTime = Time.time + animationDelay;

        }
    }

    void Normal()
    {
        animator.SetBool("Normal", true);
        animator.SetBool("Attack", false);
    }
    void Attack()
    {
        animator.SetBool("Normal", false);
        animator.SetBool("Attack", true);
    }
}
