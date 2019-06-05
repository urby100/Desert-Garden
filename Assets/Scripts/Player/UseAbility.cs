using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : MonoBehaviour
{
    public GameObject gm;
    Keybindings kb;
    public GameObject projectileSpawn;
    public GameObject projectileSpawn2;
    public GameObject pointsSpawn;
    public GameObject projectilePrefab;
    public List<GameObject> projectileList;
    
    public bool useWater = false;
    public bool useAbility = false;

    //thrower
    public GameObject throwerProjectile;
    public bool throwerAbility = false;
    public float throwerProjectileSpeed = 5f;
    public float throwerProjectileAliveDelay = 2f;
    public float throwerAttackSpeed = 0.7f;
    public float throwerAttackDelay = 4f;
    public float throwerAttackLastsDelay = 2f;
    public float throwerAttackTime;
    public float throwerAttackLastsTime;
    public float throwerAttackSpeedTime;
    public bool throwerAttack = false;

    //stopper
    public GameObject stopperProjectile;
    public bool stopperAbility = false;
    public bool stopperAttack = false;
    public float stopperAttackLastsTime;
    public float stopperAttackLastsDelay = 2.5f;
    public float stopperAttackFireTime;
    public float stopperFireRate = 0.7f;
    public float stopperProjectileSideForce = 2.5f;
    public float stopperProjectileUpForce = 2.5f;

    //crusher
    public GameObject crusherProjectile;
    public GameObject ground;
    public bool crusherAbility = false;
    public bool crusherAttack = false;

    public float crusherAttackLasts = 8f;
    public float crusherAttackLastsTime;
    public float crusherProjectileSpawnDelay = 0.2f;
    public float crusherProjectileSpawnTime;
    public int crusherProjectileCounter = 0;
    public float crusherProjectileSpeed = 2f;

    //steve
    public bool steveAbility = false;
    public GameObject steveProjectile;
    public float littleSteveMovementSpeed = 3f;
    public float littleSteveShootVelocityUp = 2.5f;
    public float littleSteveRunningLasts = 3f;
    public float steveAttackDelay = 4f;

    // Start is called before the first frame update
    void Awake()
    {
        kb = gm.GetComponent<Keybindings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(kb.attack1))
        {
            useWater=true;
        }
        if (Input.GetKeyDown(kb.attack2)) {
            useAbility = true;
        }
    }
    private void FixedUpdate()
    {
        if (useWater) {//set to false in Animations.cs
            GameObject projectile = Instantiate(projectilePrefab,projectileSpawn.transform.position,new Quaternion());
            projectile.name = "WaterProjectile";
            if (transform.rotation == new Quaternion(0, 180, 0, 0))
            {
                projectile.GetComponent<WaterProjectile>().direction = -1;
            }
            else if (transform.rotation == new Quaternion(0, 0, 0, 1))
            {
                projectile.GetComponent<WaterProjectile>().direction = 1;
            }
        }
        if (useAbility) {
            if (projectileList.Count != 0) {
                if (projectileList[projectileList.Count - 1].name == "ThrowerProjectile")
                {
                    throwerAbility = true;
                }
                else if (projectileList[projectileList.Count - 1].name == "StopperProjectile")
                {
                    stopperAbility = true;
                }
                else if (projectileList[projectileList.Count - 1].name == "CrusherProjectile") {
                    crusherAbility = true;
                } else if (projectileList[projectileList.Count - 1].name == "LittleSteve") {
                    steveAbility=true;
                }
                projectileList.RemoveAt(projectileList.Count - 1);
            }
            useAbility = false;
        }
        //steve ability
        if (steveAbility) {
            steveAbility = false;
            GameObject projectile = Instantiate(steveProjectile, projectileSpawn.transform.position, new Quaternion());
            projectile.name = "LittleSteve";
            projectile.GetComponent<SteveProjectileController>().playerObject = gameObject;
            projectile.GetComponent<SteveProjectileController>().onPlayer = true;
            projectile.layer = LayerMask.NameToLayer("FriendlyProjectiles");
            projectile.GetComponent<SteveProjectileController>().movementSpeed = littleSteveMovementSpeed;
            projectile.GetComponent<SteveProjectileController>().shootVelocityUp = littleSteveShootVelocityUp;
            projectile.GetComponent<SteveProjectileController>().runningLasts = littleSteveRunningLasts;
        }
        //crusher ability
        if (crusherAbility && !crusherAttack)
        {
            crusherAbility = false;
            crusherAttack = true;
            crusherAttackLastsTime = Time.time + crusherAttackLasts;
            crusherProjectileSpawnTime = Time.time + crusherProjectileSpawnDelay;
        }
        if (crusherAttack && Time.time < crusherAttackLastsTime) {

            if (Time.time > crusherProjectileSpawnTime)
            {
                crusherProjectileCounter++;
                if (crusherProjectileCounter <= 12)
                {
                    GameObject tempProjectile = Instantiate(crusherProjectile);
                    tempProjectile.name = "CrusherProjectile";
                    tempProjectile.GetComponent<CrusherProjectileController>().attackLastTime = crusherAttackLastsTime;
                    tempProjectile.GetComponent<PolygonCollider2D>().isTrigger = true;
                    tempProjectile.transform.SetParent(transform);
                    tempProjectile.layer = LayerMask.NameToLayer("FriendlyProjectiles");
                    tempProjectile.GetComponent<CrusherProjectileController>().onPlayer = true;
                    tempProjectile.GetComponent<CrusherProjectileController>().points = pointsSpawn;
                    tempProjectile.GetComponent<CrusherProjectileController>().speed = crusherProjectileSpeed;
                    crusherProjectileSpawnTime = Time.time + crusherProjectileSpawnDelay;

                }
            }
        }

        if (Time.time > crusherAttackLastsTime && crusherAttack)
        {
            crusherAttack = false;
            crusherProjectileCounter=0;
        }
        //stopper ability
        if (stopperAbility && !stopperAttack)
        {
            stopperAbility = false;
            stopperAttack = true;
            stopperAttackLastsTime = Time.time + stopperAttackLastsDelay;
            stopperAttackFireTime = Time.time;
        }
        if (stopperAttack && Time.time < stopperAttackLastsTime)
        {
            if (Time.time > stopperAttackFireTime)
            {
                float dir = 1;
                for (int i = 0; i < 2; i++)
                {
                    GameObject projectile = Instantiate(stopperProjectile, projectileSpawn2.transform.position, projectileSpawn2.transform.rotation);
                    projectile.name = "StopperProjectile";
                    projectile.layer = LayerMask.NameToLayer("FriendlyProjectiles");
                    projectile.GetComponent<StopperProjectileController>().globalDirection = dir;
                    projectile.GetComponent<StopperProjectileController>().shootVelocityDirection = stopperProjectileSideForce;
                    projectile.GetComponent<StopperProjectileController>().shootVelocityUp = stopperProjectileUpForce;
                    dir = dir * (-1);
                }
                stopperAttackFireTime = Time.time + stopperFireRate;
            }
        }
        if (Time.time > stopperAttackLastsTime && stopperAttack)
        {
            stopperAttack = false;
        }
        //thrower ability
        if (throwerAbility && !throwerAttack)
        {
            throwerAbility = false;
            throwerAttack = true;
            throwerAttackLastsTime = Time.time + throwerAttackLastsDelay;
            throwerAttackSpeedTime = Time.time;
        }
        if (throwerAttack && Time.time < throwerAttackLastsTime)
        {
            if (Time.time > throwerAttackSpeedTime)
            {
                GameObject projectile = Instantiate(throwerProjectile, projectileSpawn.transform.position, new Quaternion());
                projectile.layer = LayerMask.NameToLayer("FriendlyProjectiles");
                projectile.name = "ThrowerProjectile";
                projectile.GetComponent<ThrowerProjectileController>().speed = throwerProjectileSpeed;
                projectile.GetComponent<ThrowerProjectileController>().aliveDelay = throwerProjectileAliveDelay;
                if (transform.rotation == new Quaternion(0, 180, 0, 0))
                {
                    projectile.GetComponent<ThrowerProjectileController>().direction = -1;
                }
                else if (transform.rotation == new Quaternion(0, 0, 0, 1))
                {
                    projectile.GetComponent<ThrowerProjectileController>().direction = 1;
                }
                throwerAttackSpeedTime = Time.time + throwerAttackSpeed;
            }
        }
        if (Time.time > throwerAttackLastsTime && throwerAttack)
        {
            throwerAttack = false;
        }
    }
}
