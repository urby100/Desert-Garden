using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStopperController : MonoBehaviour
{

    public GameObject stopperBody;
    Animator animator;
    public GameObject stopperProjectile;
    public GameObject projectileSpawn;
    public GameObject playerObject;
    public bool attackBool = false;
    public bool hasProjectile;
    public float projectileSpeed = 3f;
    
    //sprite change delay
    float animationDelay;
    public float animationTime;
    float verticalDir;
    public void setFireTime() {
        hasProjectile = true;
        setAttackReceiveAnim();
    }
    public void setAttackReceiveAnim()
    {
        attackBool = true;
        animationTime = Time.time + animationDelay;

    }
    // Start is called before the first frame update
    void Start()
    {
        if (stopperBody.transform.position.y > projectileSpawn.transform.position.y) {
            verticalDir = -1;
        } else {
            verticalDir = 1;
        }
        animator = stopperBody.GetComponent<Animator>();
        animationDelay = 0.4f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //obrni proti playerju
        if (stopperBody.transform.position.y > projectileSpawn.transform.position.y)
        {
            if (playerObject.transform.position.x > stopperBody.transform.position.x)
            {
                stopperBody.transform.eulerAngles = new Vector3(
                                                                stopperBody.transform.eulerAngles.x,
                                                                -180,
                                                                stopperBody.transform.eulerAngles.z);
            }
            else
            {
                stopperBody.transform.eulerAngles = new Vector3(
                                                              stopperBody.transform.eulerAngles.x,
                                                              0,
                                                              stopperBody.transform.eulerAngles.z);
            }
        }
        else
        {
            if (playerObject.transform.position.x > stopperBody.transform.position.x)
            {
                stopperBody.transform.eulerAngles = new Vector3(
                                                                stopperBody.transform.eulerAngles.x,
                                                                0,
                                                                stopperBody.transform.eulerAngles.z);
            }
            else
            {
                stopperBody.transform.eulerAngles = new Vector3(
                                                              stopperBody.transform.eulerAngles.x,
                                                              -180,
                                                              stopperBody.transform.eulerAngles.z);
            }
        }
        if (hasProjectile)
        {
            attackBool = true;
            animationTime = Time.time + animationDelay;
            stopperBody.GetComponent<PolygonCollider2D>().offset = new Vector2(0, -0.06f);
            
            GameObject projectile = Instantiate(stopperProjectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
            projectile.name = "BossStopperProjectile";
            projectile.GetComponent<BossStopperProjectileController>().dir = verticalDir;
            projectile.GetComponent<BossStopperProjectileController>().speed= projectileSpeed;
            
            hasProjectile = false;

        }
        if (Time.time > animationTime && attackBool)
        {
            attackBool = false;
            stopperBody.GetComponent<PolygonCollider2D>().offset = new Vector2(0, 0);
        }
        if (attackBool)
        {
            attack();
        }
        if (!attackBool)
        {
            idle();
        }
    }
    void idle()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Neutral", false);
        animator.SetBool("Satisfied", false);

    }
    void attack()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", true);
        animator.SetBool("Neutral", false);
        animator.SetBool("Satisfied", false);
    }
}
