using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterPuddleController : MonoBehaviour
{
    public GameObject puddleObject;
    float decreaseTime;
    float decreaseRate = 1f;
    public bool increaseSize = false;
    float sizeIncrement = 0.2f;
    float minSize=0.2f;
    float maxSize = 2f;
    float destroyTime;
    float destroyDelay = 2f;
    bool destroy = false;
    // Start is called before the first frame update
    void Start()
    {
        decreaseTime = Time.time + decreaseRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (increaseSize)
        {
            puddleObject.transform.localScale =new Vector3(Mathf.Clamp(puddleObject.transform.localScale.x+ sizeIncrement,minSize,maxSize), 
                puddleObject.transform.localScale.y, 0);
            destroy = false;
            increaseSize = false;
        }
        else
        {
            if (Time.time > decreaseTime)
            {
                puddleObject.transform.localScale = new Vector3(Mathf.Clamp(puddleObject.transform.localScale.x - sizeIncrement, minSize, maxSize),
                    puddleObject.transform.localScale.y, 0);
                decreaseTime = Time.time + decreaseRate;
            }
            
        }
        if (puddleObject.transform.localScale.x == minSize && !destroy) {
            destroyTime = Time.time + destroyDelay;
            destroy = true;
        }
        if(puddleObject.transform.localScale.x == minSize && destroy && Time.time > destroyTime) {
            Destroy(gameObject);
        }
    }
    
}
