using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenProjectileScript : MonoBehaviour
{
    public GameObject effect;
    public GameObject puddlePrefab;
    float upForce = 100f;
    float sideForce = 250f;
    float rotation = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * upForce);
        GetComponent<Rigidbody2D>().AddForce(-transform.right * sideForce);
        GetComponent<Rigidbody2D>().AddTorque(-rotation);
        /*GameObject p= Instantiate(greenProjectile, transform.position, new Quaternion());
        p.name = "Green Potion Projectile";
        Destroy(gameObject, 0.4f);*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            GameObject puddle = Instantiate(puddlePrefab, collision.contacts[0].point, puddlePrefab.transform.rotation);
            puddle.name = "Puddle";
            GameObject particle;
            particle = Instantiate(effect, collision.contacts[0].point, effect.transform.rotation);
            particle.name = "GreenPotionPopEffect";
            Destroy(particle, 0.6f);
            Destroy(gameObject);
        }
    }
}
