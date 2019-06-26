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
            particle = Instantiate(effect, collision.contacts[0].point, gameObject.transform.rotation);
            particle.name = "FlowerHitEffect";
            Destroy(particle, 0.4f);

        }
    }
}
