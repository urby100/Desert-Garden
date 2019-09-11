using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaterBottle : MonoBehaviour
{
    public bool PickedUpBottle = false;
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
        if (collision.gameObject.name == "Guy") {
            PickedUpBottle = true;
            collision.gameObject.GetComponent<UseAbility>().canUseWater = true;
            collision.gameObject.GetComponent<UseAbility>().canUseAbility = false;
            gameObject.GetComponent<ParticleSystem>().Stop();
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            gameObject.GetComponent<BoxCollider2D>().enabled = false ;

        }
    }
}
