using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFloor3Script : MonoBehaviour
{
    public List<GameObject> stoppers;

    public float attackSpeed;
    public float attackTime;
    public float projectileSpeed;
    bool GroupToFire = true;
    List<GameObject> groupA = new List<GameObject>();
    List<GameObject> groupB = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach (GameObject s in stoppers)
        {
            s.GetComponent<BossStopperController>().projectileSpeed = projectileSpeed;
            if (s.GetComponent<BossStopperController>().hasProjectile)
            {
                groupA.Add(s);

            }
            else
            {
                groupB.Add(s);
            }
        }
        GroupToFire = !GroupToFire;


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > attackTime)
        {

            if (GroupToFire)
            {
                foreach (GameObject s in groupA)
                {
                    s.GetComponent<BossStopperController>().projectileSpeed = projectileSpeed;
                    s.GetComponent<BossStopperController>().setFireTime();
                }
            }
            else
            {
                foreach (GameObject s in groupB)
                {
                    s.GetComponent<BossStopperController>().projectileSpeed = projectileSpeed;
                    s.GetComponent<BossStopperController>().setFireTime();
                }
            }
            attackTime = Time.time + attackSpeed;
            GroupToFire = !GroupToFire;
        }
    }
}
