using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaterBottle : MonoBehaviour
{
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

            collision.gameObject.GetComponent<UseAbility>().enabled = true;
            collision.gameObject.GetComponent<UseAbility>().canUseWater = true;
            collision.gameObject.GetComponent<UseAbility>().canUseAbility = false;
            Destroy(gameObject);
        }
    }
}
