using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFloor1Script : MonoBehaviour
{
    public List<GameObject> cactusList;
    public float moveSpeed;
    public float moveDelay;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in cactusList) {
            g.GetComponent<BossPopperController>().setMoveSpeed(moveSpeed);
            g.GetComponent<BossPopperController>().setMoveDelay(moveDelay);
        }
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
