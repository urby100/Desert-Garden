using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSteveTraining : MonoBehaviour
{

    public bool fast = false;
    bool setFast = false;
    float speed = 0.1f;
    public GameObject SlowPoint;
    public GameObject NormalPoint;
    public GameObject FastPoint;
    Animator animator;

    float speedChangeDelay = 1.2f;
    float speedChangeTime;
    bool speedChange=false;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fast)
        {

            transform.position = Vector2.MoveTowards(transform.position, FastPoint.transform.position, speed * Time.deltaTime);
        }
        else
        {
            if (!speedChange ) {
                i= Random.Range(0, 2);
                speedChange = true;
                speedChangeTime = Time.time + speedChangeDelay;
            }
            if (Time.time > speedChangeTime)
            {
                speedChange = false;
            }
            if (i == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, SlowPoint.transform.position, speed /2* Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, NormalPoint.transform.position, speed/2 * Time.deltaTime);
            }
        }
        if (fast && !setFast)
        {
            setFast = true;
            SetAnimationFast();
        }
        if (!fast && setFast)
        {
            setFast = false;
            SetAnimationNormal();

        }
    }
    void SetAnimationFast() {
        animator.speed = 1.75f;
    }
    void SetAnimationNormal()
    {
        animator.speed = 1;
    }
}
