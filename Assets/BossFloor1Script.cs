using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFloor1Script : MonoBehaviour
{
    public List<GameObject> cactusList;
    float MoveDelay = 0.5f;
    float MoveTime;
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < cactusList.Count; i++)
        {
            if ((i + 1) >= 6)
            {
                cactusList[i].GetComponent<BossPopperController>().popperRef = cactusList[0];
                cactusList[i].GetComponent<BossPopperController>().popperRefBody = cactusList[0].transform.Find("PopperBody").gameObject;
            }
            else
            {
                cactusList[i].GetComponent<BossPopperController>().popperRef = cactusList[i + 1];
                cactusList[i].GetComponent<BossPopperController>().popperRefBody = cactusList[i+1].transform.Find("PopperBody").gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
