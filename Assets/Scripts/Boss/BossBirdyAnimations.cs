using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBirdyAnimations : MonoBehaviour
{
    public GameObject playerObject;
    Rigidbody2D birdyBodyRb;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        birdyBodyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Monster();
    }
    void Monster()
    {
        animator.SetBool("Monster", true);
        animator.SetBool("Neutral", false);
        animator.SetBool("Attack", false);
    }
}
