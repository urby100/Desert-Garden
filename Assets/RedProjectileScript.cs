using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedProjectileScript : MonoBehaviour
{
    float upForce = 100f;
    float sideForce = 250f;
    float rotation = 10f;
    public GameObject RedPotionProjectileStartPoint;
    public GameObject RedPotionProjectileEndPoint;
    public GameObject RedPotionProjectile;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * upForce);
        GetComponent<Rigidbody2D>().AddForce(-transform.right * sideForce);
        GetComponent<Rigidbody2D>().AddTorque(-rotation);
    }
    private void Update()
    {

        if (transform.position.x <= RedPotionProjectileStartPoint.transform.position.x)
        {
            float inc = 0.2f;
            for (int i = 0; i < 6; i++) {
                Vector3 v = new Vector3(transform.position.x, transform.position.y+(inc*i)-0.4f, transform.position.z);
                GameObject projectile = Instantiate(RedPotionProjectile, v, Quaternion.identity);
                projectile.name = "RedPotionProjectile";
                projectile.GetComponent<RedPotionProjectileScript>().maxDistance = RedPotionProjectileEndPoint;
                projectile.GetComponent<RedPotionProjectileScript>().speed = 7;
            }
            Destroy(gameObject);
        }
    }
}
