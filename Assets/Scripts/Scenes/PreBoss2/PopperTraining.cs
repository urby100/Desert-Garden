using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopperTraining : MonoBehaviour
{
    AudioSource audioSource;
    public bool mute;
    public AudioClip showClip;
    public AudioClip hideClip;
    bool showOnce;
    bool hideOnce;

    Animator animator;
    SpriteRenderer sr;
    Vector3 targetUp;
    Vector3 targetDown;
    public bool direction=true;
    Vector3 velocity = Vector3.zero;
    float moveSpeed = 0.04f;
    BoxCollider2D boxCollider;

    float popBackUpDelay = 0.7f;
    float popBackTime;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        targetUp = transform.position;
        targetDown = transform.position - new Vector3(0f,
        GetComponent<Renderer>().bounds.size.y
        , 0f);
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > popBackTime && !direction) {
            direction = true;
            sr.flipX = !sr.flipX;
        }
        move();
    }
    public void move()
    {
        if (direction)
        {
            if (!showOnce && !mute) {
                audioSource.PlayOneShot(showClip);
                showOnce = true;
            }
            hideOnce = false;
            Normal();
            transform.position = Vector3.SmoothDamp(transform.position,
                                                                targetUp,
                                                                ref velocity,
                                                                moveSpeed);
        }
        else//false - gre dol
        {
            if (!hideOnce && !mute)
            {
                audioSource.PlayOneShot(hideClip);
                hideOnce = true;
            }
            showOnce = false;
            Hide();
            transform.position = Vector3.SmoothDamp(transform.position,
                                                                targetDown,
                                                                ref velocity,
                                                                moveSpeed);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        direction = false;
        popBackTime = Time.time + popBackUpDelay;
        float randomX = (float)System.Math.Round(Random.Range(0.8f, 1.5f), 2);
        boxCollider.size = new Vector2(randomX,boxCollider.size.y);
    }
    void Normal()
    {
        animator.SetBool("Normal", true);
        animator.SetBool("Hide", false);
    }
    void Hide()
    {
        animator.SetBool("Normal", false);
        animator.SetBool("Hide", true);
    }
}
