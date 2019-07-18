using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveTraining : MonoBehaviour
{
    Animator animator;
    public bool fast = false;
    bool setFast = false;
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
            Fast();
        }
        else
        {
            Normal();
        }
    }
    void Normal()
    {
        animator.SetBool("Normal", true);
        animator.SetBool("Fast", false);
    }
    void Fast()
    {
        animator.SetBool("Normal", false);
        animator.SetBool("Fast", true);
    }
}
