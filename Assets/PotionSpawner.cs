using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    
    public List<GameObject> bombPrefab;

    public GameObject stevies;//for yellow potion

    float attackTime;
    float attackSpeed=1f;
    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        attackTime = Time.time + attackSpeed;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > attackTime) {
            GameObject projectile= Instantiate(bombPrefab[counter], transform.position, Quaternion.identity);
            if (bombPrefab[counter].name == "Yellow Projectile") {
                projectile.GetComponent<YellowProjectileScript>().stevies = stevies;
            }
            if (bombPrefab[counter].name == "Violet Projectile")
            {
                projectile.GetComponent<VioletProjectileScript>().stevies = stevies;
            }
            attackTime = Time.time + attackSpeed;
            counter++;
            if (counter >= bombPrefab.Count) {
                counter=0;
            }
        }
    }
}
