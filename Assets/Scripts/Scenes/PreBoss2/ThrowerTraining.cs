using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerTraining : MonoBehaviour
{
    AudioSource audioSource;
    public bool mute;
    public AudioClip attackClip;
    bool attackOnce;


    public GameObject projectileSpawnPoint;
    public GameObject projectilePrefab;
    public GameObject popper;
    Animator animator;

    bool attack = false;
    public bool wait = false;

    float attackSpeedMin = 0.75f;
    float attackSpeedMax = 1.75f;
    float attackTime;


    float throwDelay = 0.2f;
    float throwTime;
    int ignoreSelf;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > throwTime)
        {
            attack = false;
        }
        if (attack && Time.time < throwTime)
        {
            Throw();
        }
        else
        {
            if (wait)
            {
                Waiting();
            }
            else
            {
                Normal();
            }
        }
        if (Time.time > attackTime && !wait)
        {
            if (!mute) {
                audioSource.PlayOneShot(attackClip);
            }
            attack = true;
            wait = true;
            throwTime = Time.time + throwDelay;
            Throw();
            GameObject p = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
            p.name = "ThrowerProjectile";
            ignoreSelf = p.GetInstanceID();
            if (popper.transform.position.x > transform.position.x)
            {
                p.GetComponent<ThrowerTrainingProjectile>().setDirection(1);
            }
            else
            {
                p.GetComponent<ThrowerTrainingProjectile>().setDirection(-1);
            }
            p.GetComponent<ThrowerTrainingProjectile>().setSpeed(5);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetInstanceID() == ignoreSelf)
        {
            return;
        }
        wait = false;
        attackTime = Time.time + Random.Range(attackSpeedMin,attackSpeedMax);
        Destroy(collision.gameObject);
    }
    void Normal()
    {
        animator.SetBool("Normal", true);
        animator.SetBool("Waiting", false);
        animator.SetBool("Throw", false);
        animator.SetBool("Catch", false);
    }
    void Waiting()
    {
        animator.SetBool("Normal", false);
        animator.SetBool("Waiting", true);
        animator.SetBool("Throw", false);
        animator.SetBool("Catch", false);
    }
    void Throw()
    {
        animator.SetBool("Normal", false);
        animator.SetBool("Waiting", false);
        animator.SetBool("Throw", true);
        animator.SetBool("Catch", false);
    }
    void Catch()
    {
        animator.SetBool("Normal", false);
        animator.SetBool("Waiting", false);
        animator.SetBool("Throw", false);
        animator.SetBool("Catch", true);
    }
}
