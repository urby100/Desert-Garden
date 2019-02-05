using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopperController : MonoBehaviour
{
    public GameObject playerObject;
    public float globalDelay = 0f;//za drugačni delay ko je več enakih 
    public float popDelayUp = 0.7f;
    public float popDelayDown = 0.7f;
    public GameObject popperBody;
    float popTime;
    float moveSpeed = 0.04f;
    bool moveIt = true;
    bool direction = true;// gor ali dol
    Vector3 targetUp ;
    Vector3 targetDown;
    Vector3 velocity = Vector3.zero;
    //obrnjen proti playerju

    // Start is called before the first frame update
    void Start()
    {
        popTime = Time.time + popDelayUp+globalDelay;
        targetUp = popperBody.transform.position+ new Vector3(0f,1f,0f);
        targetDown = popperBody.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //obrni proti playerju
        if (playerObject.transform.position.x > popperBody.transform.position.x)
        {
            popperBody.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            popperBody.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        if (Time.time > popTime) {
            if (direction)//more iti gor
            {
                popTime = Time.time + popDelayDown + globalDelay;
            }
            else//more iti dol
            {
                popTime = Time.time + popDelayUp + globalDelay;
            }
            direction = !direction;
            moveIt = true;
        }
        if (moveIt) {
            move();
        }
    }
    void move() {
        if (direction)
        {
            popperBody.transform.position = Vector3.SmoothDamp(popperBody.transform.position,
                                                                targetUp,
                                                                ref velocity,
                                                                moveSpeed);
        }
        else
        {
            popperBody.transform.position = Vector3.SmoothDamp(popperBody.transform.position,
                                                                targetDown,
                                                                ref velocity,
                                                                moveSpeed);
        }
        
    }
}
