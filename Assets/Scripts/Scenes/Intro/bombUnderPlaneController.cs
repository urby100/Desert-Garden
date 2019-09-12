using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombUnderPlaneController : MonoBehaviour
{
    public bool mute;
    public AudioClip boomSound;
    AudioSource audioSource;

    public GameObject airplane;
    public List<Sprite> bombSprites;
    public GameObject signStage1;
    public GameObject copilotOnPlane;
    public GameObject playerObject;
    public GameObject explosionEffects;

    GameObject screwdriver;
    SpriteRenderer bomb;
    bool screwdriverHitBomb = false;
    float timeExplode;
    bool explodeOnce = false;
    public float explodeDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        TurnOffExplosionEffects();
        bomb = gameObject.GetComponent<SpriteRenderer>();
    }
    void TurnOnExplosionEffects()
    {
        if (explodeOnce)
        {
            return;
        }
        else {
            explodeOnce = true;
        }
        audioSource.PlayOneShot(boomSound);
        foreach (Transform t in explosionEffects.transform)
        {
            t.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
    void TurnOffExplosionEffects()
    {
        foreach (Transform t in explosionEffects.transform)
        {
            t.gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (screwdriverHitBomb && Time.time < timeExplode)
        {
            gameObject.GetComponent<SpriteRenderer>().color =
                new Color(bomb.color.r,
                Mathf.Clamp(bomb.color.g - (Time.deltaTime * 0.4f * (timeExplode - Time.time)), 0, 255),
                bomb.color.b);

            //spremeni obraz bombe
            if ((timeExplode - Time.time) < (explodeDelay / 4 * 3) &&
                (timeExplode - Time.time) > (explodeDelay / 4 * 2))//4 sličice, če delay 2s... more biti med 1.5s in 1s
            {
                bomb.sprite = bombSprites[1];
            }
            else if ((timeExplode - Time.time) < (explodeDelay / 4 * 2) &&
                (timeExplode - Time.time) > (explodeDelay / 4))
            {
                bomb.sprite = bombSprites[2];
            }
            else if ((timeExplode - Time.time) < (explodeDelay / 4) &&
                (timeExplode - Time.time) > 0f)
            {
                bomb.sprite = bombSprites[3];
            }
        }
        if (Time.time > timeExplode && screwdriverHitBomb) {
            screwdriver.GetComponent<ScrewdriverController>().goToSign = true;
            copilotOnPlane.GetComponent<copilotOnPlaneController>().DialogText.gameObject.SetActive(false);
            airplane.GetComponent<AirplaneController>().shoot = true;
            TurnOnExplosionEffects();
            gameObject.GetComponent<SpriteRenderer>().sprite=null;
            gameObject.GetComponent<PolygonCollider2D>().enabled=false;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Screwdriver") {
            if (!screwdriverHitBomb)
            {
                screwdriver = collision.gameObject;
                screwdriver.GetComponent<ScrewdriverController>().signStage1 = signStage1;
                copilotOnPlane.GetComponent<copilotOnPlaneController>().type = true;
                screwdriverHitBomb = true;
                timeExplode = Time.time + explodeDelay;
            }
        }
        
    }
}
