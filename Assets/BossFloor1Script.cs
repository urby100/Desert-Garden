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
                cactusList[i].GetComponent<BossPopperController>().setNextPopper(cactusList[0]);
            }
            else
            {
                cactusList[i].GetComponent<BossPopperController>().setNextPopper(cactusList[i+1]);
            }
        }
        for (int i = cactusList.Count - 1; i >= 0; i--) {
            if ((i - 1) <= -1)
            {
                cactusList[i].GetComponent<BossPopperController>().setPrevPopper(cactusList[cactusList.Count-1]);
            }
            else
            {
                cactusList[i].GetComponent<BossPopperController>().setPrevPopper(cactusList[i - 1]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
