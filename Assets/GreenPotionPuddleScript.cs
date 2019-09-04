using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GreenPotionPuddleScript : MonoBehaviour
{
    public GameObject puddleObject;
    float decreaseTime;
    float decreaseRate = 1f;
    public bool increaseSize = false;
    float sizeIncrement = 0.2f;
    float minSize = 0.2f;
    float maxSize = 2f;
    float destroyTime;
    float destroyDelay = 2f;
    bool destroy = false;
    // Start is called before the first frame update
    void Start()
    {
        //increase size...
       // puddleObject.transform.localScale = new Vector3(Mathf.Clamp(puddleObject.transform.localScale.x + sizeIncrement, minSize, maxSize),
       //     puddleObject.transform.localScale.y, 0);
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > decreaseTime)
        {
            puddleObject.transform.localScale = new Vector3(Mathf.Clamp(puddleObject.transform.localScale.x - sizeIncrement, minSize, maxSize),
                puddleObject.transform.localScale.y, 0);
            decreaseTime = Time.time + decreaseRate;
        }
        puddleObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f,0);
    }
}
