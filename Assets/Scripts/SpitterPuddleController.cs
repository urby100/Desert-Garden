using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterPuddleController : MonoBehaviour
{
    public GameObject puddleObject;
    float decreaseRate = 0.1f;
    public bool increaseSize = false;
    float sizeIncrement = 0.2f;
    float minSize=0.2f;
    float maxSize = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (increaseSize)
        {
            puddleObject.transform.localScale =new Vector3(Mathf.Clamp(puddleObject.transform.localScale.x+ sizeIncrement,minSize,maxSize), 
                puddleObject.transform.localScale.y, 0);
            increaseSize = false;
        }
        else
        {
            puddleObject.transform.localScale = new Vector3(Mathf.Clamp(puddleObject.transform.localScale.x -(decreaseRate * Time.deltaTime), minSize, maxSize), 
                puddleObject.transform.localScale.y, 0);
            
        }
    }
    
}
