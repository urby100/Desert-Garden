using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusPopUpScript : MonoBehaviour
{
    GameObject child;
    Vector3 targetUp;
    Vector3 targetDown;
    Vector3 velocity = Vector3.zero;
    public float moveSpeed = 0.08f;
    public bool direction = true;
    RectTransform rt;
    public GameObject scientists;
    // Start is called before the first frame update
    void Start()
    {
        child =this.gameObject.transform.GetChild(0).gameObject;
        
        targetUp = child.transform.position + new Vector3(0f, child.GetComponent<SpriteRenderer>().bounds.extents.y * 2, 0f);
        targetDown = child.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x > scientists.transform.position.x)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (direction)
        {
            child.transform.position = Vector3.SmoothDamp(child.transform.position,
                                                                targetUp,
                                                                ref velocity,
                                                                moveSpeed);
        }
        else
        {
            child.transform.position = Vector3.SmoothDamp(child.transform.position,
                                                                targetDown,
                                                                ref velocity,
                                                                moveSpeed);

        }
        if (!direction && child.transform.position == targetDown) {
            Destroy(gameObject);
        }
    }
}
