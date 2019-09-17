using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupTraining : MonoBehaviour
{
    public GameObject LittleSteve;
    public GameObject Treadmill;
    public GameObject Steve;
    public GameObject Crusher;

    LittleSteveTraining lst;
    Treadmill tm;
    SteveTraining st;
    CrusherTraining ct;

    float goFastDelayMin = 5f;
    float goFastDelayMax = 10f;
    float goFastDelayTime;
    float goFastLasts = 4f;
    float gofastTime;
    bool goFast;
    // Start is called before the first frame update
    void Start()
    {
        lst = LittleSteve.GetComponent<LittleSteveTraining>();
        tm = Treadmill.GetComponent<Treadmill>();
        st = Steve.GetComponent<SteveTraining>();
        ct = Crusher.GetComponent<CrusherTraining>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > gofastTime && goFast)
        {
            lst.fast = false;
            tm.fast = false;
            st.fast = false;
            ct.fast = false;

            goFast = false;
            goFastDelayTime = Time.time + Random.Range(goFastDelayMin,goFastDelayMax);
        }
        else {
            if (Time.time > goFastDelayTime && !goFast)
            {
                gofastTime = Time.time + goFastLasts;
                goFast = true;
                lst.fast = true;
                tm.fast = true;
                st.fast = true;
                ct.fast = true;
            }

        }
    }
}
