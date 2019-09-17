using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScientists : MonoBehaviour
{
    public bool mute;
    public AudioClip walking;
    float walkingRepeatTime;

    AudioSource audioSource1;
    AudioSource audioSource2;

    public GameObject scientist1;
    public GameObject scientist2;
    public GameObject cactus;
    float scientistSpeed = 4f;
    float keepDistance = 1.6f;
    float waveTime;
    float waveLasts = 2f;
    bool wave = false;
    bool wavedOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource1 = scientist1.GetComponent<AudioSource>();
        audioSource2 = scientist2.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (cactus == null)
        {
            wave = false;
            wavedOnce = false;
            Scientist1standing();
            Scientist2standing();
            return;
        }

        if (gameObject.transform.position.x > cactus.transform.position.x)
        {
            scientist1.transform.rotation = new Quaternion(0, 180, 0, 0);
            scientist2.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            scientist1.transform.rotation = new Quaternion(0, 0, 0, 0);
            scientist2.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (gameObject.transform.position.x > cactus.transform.position.x)
        {
            keepDistance = 1.6f;
        }
        else
        {
            keepDistance = -1.6f;
        }
        gameObject.transform.position =
                    Vector3.MoveTowards(gameObject.transform.position,
                    new Vector3(cactus.transform.position.x + keepDistance, gameObject.transform.position.y, gameObject.transform.position.z)
                    , scientistSpeed * Time.deltaTime);
        if (Math.Abs(cactus.transform.position.x + keepDistance - gameObject.transform.position.x) < 0.2f)
        {
            if (!wavedOnce) {
                wave = true;
                waveTime = Time.time + waveLasts;
                wavedOnce = true;
            }
            if (wave && Time.time < waveTime)
            {
                Scientist1waving();
                Scientist2waving();
            }
            else
            {
                Scientist1standing();
                Scientist2standing();
            }
        }
        else
        {
            if (!mute && Time.time > walkingRepeatTime) {
                audioSource1.PlayOneShot(walking);
                audioSource2.PlayOneShot(walking);
                walkingRepeatTime = Time.time + walking.length;
            }

            Scientist1walking();
            Scientist2walking();
        }


    }
    void Scientist1walking()
    {
        scientist1.GetComponent<Animator>().SetBool("walking", true);
        scientist1.GetComponent<Animator>().SetBool("standing", false);
        scientist1.GetComponent<Animator>().SetBool("waving", false);
    }
    void Scientist1standing()
    {
        scientist1.GetComponent<Animator>().SetBool("walking", false);
        scientist1.GetComponent<Animator>().SetBool("standing", true);
        scientist1.GetComponent<Animator>().SetBool("waving", false);
    }
    void Scientist1waving()
    {
        scientist1.GetComponent<Animator>().SetBool("walking", false);
        scientist1.GetComponent<Animator>().SetBool("standing", false);
        scientist1.GetComponent<Animator>().SetBool("waving", true);
    }
    void Scientist2walking()
    {
        scientist2.GetComponent<Animator>().SetBool("walking", true);
        scientist2.GetComponent<Animator>().SetBool("standing", false);
        scientist2.GetComponent<Animator>().SetBool("waving", false);
    }
    void Scientist2standing()
    {
        scientist2.GetComponent<Animator>().SetBool("walking", false);
        scientist2.GetComponent<Animator>().SetBool("standing", true);
        scientist2.GetComponent<Animator>().SetBool("waving", false);
    }
    void Scientist2waving()
    {
        scientist2.GetComponent<Animator>().SetBool("walking", false);
        scientist2.GetComponent<Animator>().SetBool("standing", false);
        scientist2.GetComponent<Animator>().SetBool("waving", true);
    }
}
