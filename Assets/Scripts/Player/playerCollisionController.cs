using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCollisionController : MonoBehaviour
{
    public GameObject landEffect;
    float prev_vel;
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sceneName == "Boss")
        {
            return;
        }
        //jump effect
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 0 && prev_vel == 0 && gameObject.GetComponent<Move>().jump)
        {
            var em = landEffect.GetComponent<ParticleSystem>().emission;
            em.rateOverTime = 200;
            var gm = landEffect.GetComponent<ParticleSystem>().main.gravityModifier;
            gm.constant = 1f;
            var sh= landEffect.GetComponent<ParticleSystem>().shape;
            sh.shapeType = ParticleSystemShapeType.Cone;
            GameObject particle = Instantiate(landEffect, gameObject.transform.position + new Vector3(0, -0.5f, 0), gameObject.transform.rotation);
            particle.name = "JumpEffectGuy";
            Destroy(particle, 0.4f);
        }
        prev_vel = gameObject.GetComponent<Rigidbody2D>().velocity.y;
        //walking effect
        if (gameObject.GetComponent<Rigidbody2D>().velocity.x != 0 && gameObject.GetComponent<Rigidbody2D>().velocity.y==0)
        {/*
            var em = landEffect.GetComponent<ParticleSystem>().emission;
            em.rateOverTime = 25;
            var gm = landEffect.GetComponent<ParticleSystem>().main.gravityModifier;
            gm.constant = 0.5f;
            var sh = landEffect.GetComponent<ParticleSystem>().shape;
            sh.shapeType = ParticleSystemShapeType.Cone;
            GameObject particle = Instantiate(landEffect, gameObject.transform.position+new Vector3(0,-0.5f,0), gameObject.transform.rotation);
            particle.name = "MoveEffectGuy";
            Destroy(particle, 0.4f);*/
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //land effect
        if (collision.gameObject.name == "Ground") {
            gameObject.GetComponent<Move>().jump = false;

            if (sceneName != "Boss")
            {
                var em = landEffect.GetComponent<ParticleSystem>().emission;
                if (collision.relativeVelocity.magnitude < 10) { return; }
                em.rateOverTime = 100 + Mathf.Clamp(collision.relativeVelocity.magnitude - 10, 1, 6) * 20;
                var gm = landEffect.GetComponent<ParticleSystem>().main.gravityModifier;
                gm.constant = Random.Range(1.5f, 2);
                var sh = landEffect.GetComponent<ParticleSystem>().shape;
                sh.shapeType = ParticleSystemShapeType.Sphere;
                GameObject particle = Instantiate(landEffect, collision.contacts[0].point, gameObject.transform.rotation);
                particle.name = "LandEffectGuy";
                Destroy(particle, 0.4f);
            }
        }
        if (collision.gameObject.name == "PopperBody") {
            GetComponent<Move>().hurtRequest = true;
        }
        if (collision.gameObject.name == "StopperBody")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "CrusherBody")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "SteveBody")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "StopperProjectile")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "CrusherProjectile")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "LittleSteve")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "ThrowerProjectile")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "BossStopperProjectile")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (collision.gameObject.name == "BossThrowerProjectile")
        {
            GetComponent<Move>().hurtRequest = true;

        }
        if (GetComponent<Move>().invincible) {
            return;
        }
        //planeWing
        if (collision.gameObject.name == "planeWing")
        {
            GetComponent<Move>().tiredRequest = true;
        }
        //Real monsters
        if (collision.gameObject.name == "SpitterProjectile")
        {
            GetComponent<Move>().tiredRequest = true;
        }
        if (collision.gameObject.name == "Puddle")
        {
            GetComponent<Move>().tiredRequest = true;
        }
        if (collision.gameObject.name == "SpitterBody")
        {
            GetComponent<Move>().tiredRequest = true;
        }
        if (collision.gameObject.name == "CrawlerBody")
        {
            if (!collision.gameObject.GetComponent<CrawlerAnimations>().NeutralBool)
            {
                GetComponent<Move>().tiredRequest = true;
            }
        }
        if (collision.gameObject.name == "BirdyBody")
        {
            if (!collision.gameObject.GetComponent<BirdyAnimations>().NeutralBool)
            {
                GetComponent<Move>().tiredRequest = true;
            }
        }
        if (collision.gameObject.name == "ChameleonBody")
        {
            GetComponent<Move>().tiredRequest = true;
        }
    }
}
