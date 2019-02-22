using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopperController : MonoBehaviour
{
    public GameObject stopperBody;
    public GameObject stopperProjectile;
    public GameObject projectileSpawn;
    public GameObject playerObject;
    public List<Sprite> sprites;

    public float projectileSideForce=2.5f;
    public float projectileUpForce = 2.5f;
    public float fireRate = 0.7f;
    public float popDelay = 0f;

    float fireTime;
    float popTime;
    Vector3 targetUp;
    Vector3 targetDown;
    Vector3 velocity = Vector3.zero;
    float moveSpeedUp = 0.04f;
    bool outside = false;
    //sprite change delay
    float spriteDelay;
    float spriteTime;
    bool spriteChanged = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteDelay = fireRate / 2;
        popTime = Time.time + popDelay;
        targetUp = stopperBody.transform.position + new Vector3(0f, 0.7f, 0f);
        targetDown = stopperBody.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        if (Time.time > fireTime && outside) {
            spriteChanged = true;
            spriteTime = Time.time + spriteDelay;
            stopperBody.GetComponent<SpriteRenderer>().sprite = sprites[1];
            stopperBody.GetComponent<PolygonCollider2D>().offset = new Vector2(0, -0.06f);
            float dir = 1;
            for (int i = 0; i < 2; i++) {
                GameObject projectile = Instantiate(stopperProjectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
                projectile.GetComponent<StopperProjectileController>().globalDirection = dir;
                projectile.GetComponent<StopperProjectileController>().shootVelocityDirection = projectileSideForce;
                projectile.GetComponent<StopperProjectileController>().shootVelocityUp = projectileUpForce;
                dir = dir * (-1);
            }
            fireTime = Time.time + fireRate;

        }
        if (Time.time > spriteTime && spriteChanged)
        {
            spriteChanged = false;
            stopperBody.GetComponent<SpriteRenderer>().sprite = sprites[0];
            stopperBody.GetComponent<PolygonCollider2D>().offset = new Vector2(0, 0);
        }
    }
}
