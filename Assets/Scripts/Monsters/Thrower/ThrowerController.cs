using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerController : MonoBehaviour
{
    public GameObject throwerBody;
    public GameObject projectileSpawn;
    public GameObject throwerProjectile;
    public GameObject playerObject;
    public List<Sprite> sprites;
    public float projectileSpeed = 5f;
    public float projectileAliveDelay = 2f;
    public float attackSpeed = 0.7f;
    public float attackDelay = 4f;
    public float attackLastsDelay = 2f;
    Animator animator;
    
    float attackTime;
    float attackLastsTime;
    float attackSpeedTime;
    bool attack = false;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = throwerBody.GetComponent<Animator>();
        attackTime = Time.time + attackDelay;
    }
     void Update()
    {
        if (Time.time > attackSpeedTime-(attackSpeed/2)) {//animacija
            normal();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //obrni proti playerju
        if (playerObject.transform.position.x > throwerBody.transform.position.x)
        {
            throwerBody.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            throwerBody.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        if (Time.time > attackTime && !attack) {
            attack = true;
            attackLastsTime= Time.time + attackLastsDelay;
            attackSpeedTime = Time.time;
        }
        if (attack && Time.time<attackLastsTime) {
            if (Time.time > attackSpeedTime) {
                GameObject projectile = Instantiate(throwerProjectile, projectileSpawn.transform.position, new Quaternion());
                projectile.name = "ThrowerProjectile";
                projectile.GetComponent<ThrowerProjectileController>().speed = projectileSpeed;
                projectile.GetComponent<ThrowerProjectileController>().aliveDelay = projectileAliveDelay;
                if (playerObject.transform.position.x > throwerBody.transform.position.x)
                {
                    projectile.GetComponent<ThrowerProjectileController>().direction = 1;
                }
                else
                {
                    projectile.GetComponent<ThrowerProjectileController>().direction = -1;
                }
                attackSpeedTime = Time.time + attackSpeed;
                attacking();
            }
            
        }
        if (Time.time > attackLastsTime && attack) {
            attack = false;
            attackTime = Time.time + attackDelay;
            normal();
        }
    }
    void normal() {
        animator.SetBool("normal",true);
        animator.SetBool("attacking", false);
    }

    void attacking()
    {
        animator.SetBool("normal", false);
        animator.SetBool("attacking", true);

    }
}
