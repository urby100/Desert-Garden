using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowerController : MonoBehaviour
{
    public GameObject throwerBody;
    public GameObject projectileSpawn;
    public GameObject projectileMaxDistance;
    public GameObject throwerProjectile;
    public float projectileSpeed = 5f;
    public float attackSpeed = 0.7f;
    public float attackSpeedTime;
    Animator animator;




    // Start is called before the first frame update
    void Start()
    {
        animator = throwerBody.GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > attackSpeedTime - (attackSpeed / 2))
        {//animacija
            normal();
        }
        else
        {
            attacking();
        }

        if (Time.time > attackSpeedTime)
        {
            GameObject projectile = Instantiate(throwerProjectile, projectileSpawn.transform.position, new Quaternion());
            projectile.name = "BossThrowerProjectile";
            projectile.GetComponent<BossThrowerProjectileController>().speed = projectileSpeed;
            projectile.GetComponent<BossThrowerProjectileController>().maxDistance = projectileMaxDistance;
            attackSpeedTime = Time.time + attackSpeed;
        }
    }
    void normal()
    {
        animator.SetBool("normal", true);
        animator.SetBool("attacking", false);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", false);
    }

    void attacking()
    {
        animator.SetBool("normal", false);
        animator.SetBool("attacking", true);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", false);

    }
}
