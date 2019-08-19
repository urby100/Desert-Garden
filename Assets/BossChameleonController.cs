using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChameleonController : MonoBehaviour
{

    public GameObject ChameleonBody;
    public GameObject SpawnObject;
     List<Transform> spawnPoints=new List<Transform>();
    float attackSpeed = 3f;
    public bool neutral = false;
    public bool attack = false;
    public float attackTime;
    int counter = 0;
    // Start is called before the first frame update
    void Awake()
    {

        foreach (Transform s in SpawnObject.transform) {
            spawnPoints.Add(s);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        if (Time.time > attackTime && !attack)
        {
            //nastavi pozicijo
            ChameleonBody.transform.position = spawnPoints[counter].position;
            ChameleonBody.transform.rotation = spawnPoints[counter].rotation;
            counter++;
            if (counter >= spawnPoints.Count) {
                counter = 0;
            }

            attack = true;
            attackTime = Time.time + attackSpeed;
        }


    }
}
