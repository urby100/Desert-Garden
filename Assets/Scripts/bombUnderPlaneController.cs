using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombUnderPlaneController : MonoBehaviour
{
    public GameObject airplane;
    public List<Sprite> bombSprites;

    SpriteRenderer bomb;
    bool screwdriverHitBomb = false;
    float timeExplode;
    public float explodeDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        bomb = gameObject.GetComponent<SpriteRenderer>();
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
            airplane.GetComponent<AirplaneController>().shoot = true;
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Screwdriver") {
            if (!screwdriverHitBomb)
            {
                screwdriverHitBomb = true;
                timeExplode = Time.time + explodeDelay;
            }
        }
        
    }
}
