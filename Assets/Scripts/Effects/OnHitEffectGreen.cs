using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class OnHitEffectGreen : MonoBehaviour
{
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject particle;
        if (collision.gameObject.name == "Guy" && effect.name == "GreenHitEffect")
        {
            particle = Instantiate(effect, collision.contacts[0].point, gameObject.transform.rotation);
            particle.name = "GreenHitEffect";
            Destroy(particle, 0.4f);
        }
        else if (effect.name == "FlowerHitEffect" )
        {
            if (collision.gameObject.name == "Basket Hoop" || collision.gameObject.name == "Hoop") {
                return;
            }
            if (collision.gameObject.name == "Ground Collider Top")
            {
                particle = Instantiate(effect, collision.contacts[0].point, new Quaternion());
                ParticleSystem ps = particle.GetComponent<ParticleSystem>();
                var vel = ps.velocityOverLifetime;
                vel.y = 0.5f;
                var shape = ps.shape;
                shape.shapeType = ParticleSystemShapeType.Sphere;
                shape.radius = 0.2f;
                shape.rotation = new Vector3(0,90,90);
                var em=ps.emission;
                em.rateOverTime = 80;
                particle.name = "FlowerHitEffect";
                Destroy(particle, 0.4f);
            }
            else
            {
                particle = Instantiate(effect, collision.contacts[0].point, gameObject.transform.rotation);
                particle.name = "FlowerHitEffect";
                Destroy(particle, 0.4f);
            }

        }
    }
}
