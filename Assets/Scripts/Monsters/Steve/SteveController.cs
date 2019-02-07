using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveController : MonoBehaviour
{

    public GameObject steveBody;
    public GameObject playerObject;
    public GameObject steveProjectile;
    public GameObject spawnProjectile;

    public bool arrived = false;
    Animator animator;
    public bool attack = false;
    float attackDelay = 4f;
    float attackTime;
    GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        animator = steveBody.GetComponent<Animator>();
        attackTime = Time.time + attackDelay;
        
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
        if (Time.time > attackTime && !attack) {
            attackAnimation();
            projectile = Instantiate(steveProjectile, spawnProjectile.transform.position, new Quaternion());
            projectile.GetComponent<SteveProjectileController>().steveBody = steveBody;
            projectile.GetComponent<SteveProjectileController>().playerObject = playerObject;
            projectile.GetComponent<SteveProjectileController>().steve = gameObject;
            attack = true;
        }
        if (arrived) {
                normalAnimation();
                attackTime = Time.time + attackDelay;
                attack = false;
            arrived = false;
        }

    }

    
    void normalAnimation() {
        animator.SetBool("normal",true);
        animator.SetBool("attack", false);
    }
    void attackAnimation()
    {
        animator.SetBool("normal", false);
        animator.SetBool("attack", true);
    }


}
