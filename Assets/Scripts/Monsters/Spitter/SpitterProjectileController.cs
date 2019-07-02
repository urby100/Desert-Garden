using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterProjectileController : MonoBehaviour
{
    public GameObject effect;
    public GameObject spitterPuddle;
    public float globalDirection = -1;
    float shootVelocity = 4f;
    float revSpeed = 720f;
    float fallMultiplier = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(globalDirection * Vector2.right * shootVelocity, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = fallMultiplier;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, transform.localRotation.eulerAngles.z + revSpeed * Time.deltaTime));

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            GameObject puddle = Instantiate(spitterPuddle,collision.contacts[0].point, spitterPuddle.transform.rotation);
            puddle.name = "Puddle";
            GameObject particle;
            particle = Instantiate(effect, collision.contacts[0].point, effect.transform.rotation);
            particle.name = "PuddleHitEffect";
            Destroy(particle, 0.6f);
        }
        else if (collision.gameObject.name == "Puddle")
        {
            GameObject particle;
            particle = Instantiate(effect, collision.contacts[0].point, effect.transform.rotation);
            particle.name = "PuddleHitEffect";
            Destroy(particle, 0.6f);
            collision.gameObject.transform.parent.gameObject.GetComponent<SpitterPuddleController>().increaseSize = true;
        }
        Destroy(gameObject);
    }
}
