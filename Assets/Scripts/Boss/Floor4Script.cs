using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor4Script : MonoBehaviour
{
    public GameObject points;
    public GameObject LittleSteves;
    public GameObject floor;
    public GameObject ceiling;
    public bool littleStevesStartRunning = false;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform littleSteve in LittleSteves.transform )
        {
            littleSteve.gameObject.GetComponent<ParticleSystem>().Stop();
            littleSteve.gameObject.GetComponent<TrailRenderer>().enabled = false;
            littleSteve.gameObject.GetComponent<LittleSteveBossController>().points = points;
            littleSteve.gameObject.GetComponent<LittleSteveBossController>().floor = floor;
            littleSteve.gameObject.GetComponent<LittleSteveBossController>().ceiling = ceiling;
            littleSteve.gameObject.GetComponent<LittleSteveBossController>().floor4script = this;
        }
        littleStevesStartRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
