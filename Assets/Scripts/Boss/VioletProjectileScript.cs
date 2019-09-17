using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VioletProjectileScript : MonoBehaviour
{
    float upForce = 100f;
    float sideForce = 250f;
    float rotation = 10f;

    public GameObject stevies;
    public GameObject effect;
    float time = 2f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * upForce);
        GetComponent<Rigidbody2D>().AddForce(-transform.right * sideForce);
        GetComponent<Rigidbody2D>().AddTorque(-rotation);
    }
    void ChangeSteviesDirection()
    {
        foreach (Transform littleSteve in stevies.transform)
        {
            littleSteve.gameObject.GetComponent<LittleSteveBossController>().ChangeDirection(time);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeSteviesDirection();
        GameObject particle;
        particle = Instantiate(effect, collision.contacts[0].point, effect.transform.rotation);
        particle.name = "VioletPotionPopEffect";
        Destroy(particle, 0.6f);
        Destroy(gameObject);
    }
}
