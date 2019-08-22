using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrusherAnimations : MonoBehaviour
{
    GameObject crusherGameobject;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {

        crusherGameobject = transform.parent.gameObject;
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (crusherGameobject.GetComponent<BossCrusherController>().attack)
        {
            attacking();
        }
        if (!crusherGameobject.GetComponent<BossCrusherController>().attack)
        {
            normal();
        }
    }
    void normal()
    {
        animator.SetBool("normal", true);
        animator.SetBool("attacking", false);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", false);
    }
    void attacking()
    {
        animator.SetBool("normal", false);
        animator.SetBool("attacking", true);
        animator.SetBool("neutral", false);
        animator.SetBool("satisfied", false);

    }
}
