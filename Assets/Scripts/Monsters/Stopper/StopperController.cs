using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopperController : MonoBehaviour
{
    public GameObject stopperBody;
    public GameObject stopperProjectile;
    public GameObject projectileSpawn;
    public GameObject playerObject;
    public bool attackBool = false;
    public bool satisfiedBool = false;
    public bool giveAttack = true;

    public float projectileSideForce=2.5f;
    public float projectileUpForce = 2.5f;
    public float fireRate = 0.7f;
    public float popDelay = 0f;

    public float fireTime;
    float popTime;
    Vector3 targetUp;
    Vector3 targetDown;
    Vector3 velocity = Vector3.zero;
    float moveSpeedUp = 0.04f;
    bool outside = false;
    //sprite change delay
    float animationDelay;
    float animationTime;
    // Start is called before the first frame update
    void Start()
    {
        animationDelay = fireRate / 2;
        if (animationDelay > 0.4f) {
            animationDelay = 0.4f;
        }
        popTime = Time.time + popDelay;
        targetUp = stopperBody.transform.position + new Vector3(0f, 0.7f, 0f);
        targetDown = stopperBody.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (satisfiedBool && giveAttack) {
            playerObject.GetComponent<UseAbility>().projectileList.Add(stopperProjectile);
            giveAttack = false;
        }
        //obrni proti playerju
        if (playerObject.transform.position.x > stopperBody.transform.position.x)
        {
            stopperBody.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            stopperBody.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        if (Time.time > popTime && !outside)
        {
            stopperBody.transform.position = Vector3.SmoothDamp(stopperBody.transform.position,
                                                                targetUp,
                                                                ref velocity,
                                                                moveSpeedUp);
            
            
        }
        if (stopperBody.transform.position==targetUp && !outside) {
            outside = true;
            fireTime = Time.time + fireRate;
        }
        if (Time.time > fireTime && outside && !satisfiedBool) {
            attackBool = true;
            animationTime = Time.time + animationDelay;
            stopperBody.GetComponent<PolygonCollider2D>().offset = new Vector2(0, -0.06f);
            float dir = 1;
            for (int i = 0; i < 2; i++) {
                GameObject projectile = Instantiate(stopperProjectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
                projectile.name = "StopperProjectile";
                projectile.GetComponent<StopperProjectileController>().globalDirection = dir;
                projectile.GetComponent<StopperProjectileController>().shootVelocityDirection = projectileSideForce;
                projectile.GetComponent<StopperProjectileController>().shootVelocityUp = projectileUpForce;
                dir = dir * (-1);
            }
            fireTime = Time.time + fireRate;

        }
        if (Time.time > animationTime && attackBool)
        {
            attackBool = false;
            stopperBody.GetComponent<PolygonCollider2D>().offset = new Vector2(0, 0);
        }
    }
}
