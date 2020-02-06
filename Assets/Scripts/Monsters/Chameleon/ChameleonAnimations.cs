using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonAnimations : MonoBehaviour
{
    public GameObject ChameleonGameObject;
    Animator animator;
    public bool NeutralBool = false;
    public bool showAnim = false;
    public bool attackAnim = false;
    public bool hideAnim = false;
    float whenToHideTIme;
    float hidedelay = 7f;
    public bool whentohidetimesaved = false;
    float attackanimdelay=0.583f;
    float showhidedelay = 0.75f;
    float attackanimtime;
    float showanimtime;
    float hideanimtime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        this.GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {


        if (NeutralBool)
        {
            
            Neutral();
            ChameleonGameObject.GetComponent<ChameleonController>().neutral = true;
        }
        else
        {
            if (ChameleonGameObject.GetComponent<ChameleonController>().attack)
            {
                if (!whentohidetimesaved)
                {

                    hideAnim = false;
                    this.GetComponent<Renderer>().enabled = true;
                    whenToHideTIme = ChameleonGameObject.GetComponent<ChameleonController>().attackTime - hidedelay;
                    whentohidetimesaved = true;
                    showanimtime = Time.time + showhidedelay;
                }
                if (!showAnim) {
                    Show();
                    if (Time.time > showanimtime) {
                        showAnim = true;
                        attackanimtime = Time.time + attackanimdelay;
                    }
                }
                if (showAnim && !attackAnim)
                {
                    Attack();
                    if (Time.time > attackanimtime)
                    {
                        attackAnim = true;
                    }
                }
                if (attackAnim)
                {
                    Idle();
                }
                if (Time.time > whenToHideTIme && ChameleonGameObject.GetComponent<ChameleonController>().attack)
                {
                    whentohidetimesaved = false;
                    showAnim = false;
                    attackAnim = false;
                    hideanimtime = Time.time + showhidedelay;
                    ChameleonGameObject.GetComponent<ChameleonController>().attack = false;

                }
            }
            if (!showAnim && !attackAnim && !whentohidetimesaved && !ChameleonGameObject.GetComponent<ChameleonController>().attack)
            {
                Hide();
                hideAnim = true;
                if (Time.time > hideanimtime)
                {
                    this.GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "CrusherProjectile")
        {
            NeutralBool = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "ThrowerProjectile")
        {
            NeutralBool = true;
        }
        if (collision.gameObject.name == "LittleSteve")
        {
            NeutralBool = true;
        }
        if (collision.gameObject.name == "StopperProjectile")
        {
            NeutralBool = true;
        }
    }
    void Attack()
    {
        animator.SetBool("attack", true);
        animator.SetBool("hide", false);
        animator.SetBool("show", false);
        animator.SetBool("idle", false);
        animator.SetBool("neutral", false);
    }
    void Hide()
    {
        animator.SetBool("attack", false);
        animator.SetBool("hide", true);
        animator.SetBool("show", false);
        animator.SetBool("idle", false);
        animator.SetBool("neutral", false);
    }
    void Show()
    {
        animator.SetBool("attack", false);
        animator.SetBool("hide", false);
        animator.SetBool("show", true);
        animator.SetBool("idle", false);
        animator.SetBool("neutral", false);
    }
    void Idle()
    {
        animator.SetBool("attack", false);
        animator.SetBool("hide", false);
        animator.SetBool("show", false);
        animator.SetBool("idle", true);
        animator.SetBool("neutral", false);
    }
    void Neutral()
    {
        animator.SetBool("attack", false);
        animator.SetBool("hide", false);
        animator.SetBool("show", false);
        animator.SetBool("idle", false);
        animator.SetBool("neutral", true);
    }
}
