using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public GameObject scientistBoss;
    public List<GameObject> bombPrefab;
    public GameObject bluePointStopPushBackPoint;
    public GameObject cam;

    public GameObject RedPotionProjectileStartPoint;
    public GameObject RedPotionProjectileEndPoint;
    public GameObject RedPotionProjectile;


    public GameObject player;
    public GameObject stevies;//for yellow potion

    float attackTime;
    float attackSpeed = 1f;
    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        attackTime = Time.time + attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > attackTime- scientistBoss.GetComponent<ScientistBossScript>().getAnimLasts()) {

            scientistBoss.GetComponent<ScientistBossScript>().throwAnimation = true;
        }
        if (Time.time > attackTime)
        {
            GameObject projectile = Instantiate(bombPrefab[counter], transform.position, Quaternion.identity);
            if (bombPrefab[counter].name == "Yellow Projectile")
            {
                projectile.GetComponent<YellowProjectileScript>().stevies = stevies;
            }
            if (bombPrefab[counter].name == "Violet Projectile")
            {
                projectile.GetComponent<VioletProjectileScript>().stevies = stevies;
            }
            if (bombPrefab[counter].name == "Blue Projectile")
            {
                projectile.GetComponent<BlueProjectileScript>().player = player;
                projectile.GetComponent<BlueProjectileScript>().bluePointStopPushBackPoint = bluePointStopPushBackPoint;
                projectile.GetComponent<BlueProjectileScript>().cam = cam;
            }
            if (bombPrefab[counter].name == "Red Projectile")
            {
                projectile.GetComponent<RedProjectileScript>().RedPotionProjectileStartPoint = RedPotionProjectileStartPoint;
                projectile.GetComponent<RedProjectileScript>().RedPotionProjectileEndPoint = RedPotionProjectileEndPoint;
                projectile.GetComponent<RedProjectileScript>().RedPotionProjectile = RedPotionProjectile;
            }
            attackTime = Time.time + attackSpeed;
            counter++;
            if (counter >= bombPrefab.Count)
            {
                counter = 0;
            }
        }
    }
}
