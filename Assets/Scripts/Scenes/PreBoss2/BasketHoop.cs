using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketHoop : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "StopperTrainingProjectile") {
            Hit();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "StopperTrainingProjectile")
        {
            Normal();
        }
    }
    void Normal()
    {
        animator.SetBool("Normal", true);
        animator.SetBool("Hit", false);
    }
    void Hit()
    {
        animator.SetBool("Normal", false);
        animator.SetBool("Hit", true);
    }
}
