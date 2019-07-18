using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherTraining : MonoBehaviour
{
    Animator animator;
    public bool fast=false;
    bool setFast = false;
    public GameObject projectile;
    public GameObject points;
    public float projectileSpawnDelay = 0.2f;
    public float projectileSpeed = 2f;
    bool attack = true;
    List<GameObject> projectiles;
    float attackTime;
    float projectileSpawnTime;
    int counter = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        projectiles = new List<GameObject>();
        projectileSpawnTime = Time.time + projectileSpawnDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fast)
        {
            Fast();
        }
        else {
            Normal();
        }
        if (fast && !setFast) {
            setFast = true;
            SetProjectilesFast();
        }
        if (!fast && setFast)
        {
            setFast = false;
            SetProjectilesSlow();

        }
        if (attack)
        {
            if (Time.time > projectileSpawnTime)
            {
                counter++;
                if (counter <= 12)
                {
                    GameObject tempProjectile = Instantiate(projectile);
                    tempProjectile.name = "CrusherTrainingProjectile";
                    tempProjectile.GetComponent<CrusherTrainingProjectile>().crusher = gameObject;
                    tempProjectile.GetComponent<CrusherTrainingProjectile>().points = points;
                    tempProjectile.GetComponent<CrusherTrainingProjectile>().speed = projectileSpeed;
                    projectiles.Add(tempProjectile);
                    projectileSpawnTime = Time.time + projectileSpawnDelay;

                }
                else {
                    attack = false;
                }
            }
        }
    }
    void SetProjectilesFast() {
        foreach (GameObject go in projectiles) {
            go.GetComponent<CrusherTrainingProjectile>().speed = projectileSpeed * 1.5f;
        }
    }
    void SetProjectilesSlow()
    {
        foreach (GameObject go in projectiles)
        {
            go.GetComponent<CrusherTrainingProjectile>().speed = projectileSpeed;
        }
    }


    void Normal()
    {
        animator.SetBool("Normal", true);
        animator.SetBool("Fast", false);
    }
    void Fast()
    {
        animator.SetBool("Normal", false);
        animator.SetBool("Fast", true);
    }
}
