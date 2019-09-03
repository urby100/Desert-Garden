using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSteveBossController : MonoBehaviour
{
    public Floor4Script floor4script;
    public GameObject points;
    Animator animator;
    public GameObject floor;
    public GameObject ceiling;
    public List<Transform> pointsList;
    float speed = 3f;
    float defaultSpeed;
    int counter = 0;
    float rotateSpeed = 12f;
    float SpeedChangeTime;
    bool changedSpeed = false;
    float changeDirTime;
    bool changeDir = false;
    public void SetSpeed(float s, float speedLasts)
    {
        GetComponent<TrailRenderer>().enabled = true;
        speed = s;
        SpeedChangeTime = Time.time + speedLasts;
        changedSpeed = true;
    }
    public void ChangeDirection(float changeDirLasts)
    {
        GetComponent<ParticleSystem>().Play();
        changeDirTime = Time.time + changeDirLasts;
        changeDir = true;
        if (counter - 1 <= -1)
        {
            counter = pointsList.Count - 1;
        }
        else
        {
            counter = counter - 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = speed;
        animator = GetComponent<Animator>();
        foreach (Transform point in points.transform)
        {
            pointsList.Add(point);
        }
        float dist = Vector3.Distance(transform.position, pointsList[0].position);
        string pointName = pointsList[counter].gameObject.name;
        Transform closestPoint = pointsList[counter];
        int closest = 0;
        for (int i = 1; i < pointsList.Count; i++)
        {
            if (Vector3.Distance(transform.position, pointsList[i].position) < dist)
            {
                dist = Vector3.Distance(transform.position, pointsList[i].position);
                pointName = pointsList[i].gameObject.name;
                closestPoint = pointsList[i];
                closest = i;
            }
        }
        //nastaviti moramo cilj
        if (transform.position.x == closestPoint.position.x)
        {
            //če je x isti pomeni, da je objekt na poti gor ali dol
            //objekt je lahko ravno zapustil prejšnji cilj in je bližje njemu zato mu moramo nastaviti cilj
            //na naslednjo tarčo
            //ali pa je blizu cilju in ni treba nič spremeniti
            if (closest + 1 >= pointsList.Count)
            {
                if (pointsList[0].position.x == transform.position.x)
                {
                    counter = 0;
                }
                else
                {
                    counter = closest;
                }
            }
            else
            {
                if (pointsList[closest + 1].position.x == transform.position.x)
                {
                    counter = closest + 1;
                }
                else
                {
                    counter = closest;
                }
            }

        }
        else
        {
            if (closest + 1 >= pointsList.Count)
            {
                if (pointsList[0].position.y == transform.position.y)
                {
                    counter = 0;
                }
                else
                {
                    counter = closest;
                }
            }
            else
            {
                if (pointsList[closest + 1].position.y == transform.position.y)
                {
                    counter = closest + 1;
                }
                else
                {
                    counter = closest;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!floor4script.littleStevesStartRunning)
        {
            return;
        }
        if (changedSpeed)
        {
            if (Time.time > SpeedChangeTime)
            {
                changedSpeed = false;
                GetComponent<TrailRenderer>().enabled = false;
                speed = defaultSpeed;
            }
        }
        if (changeDir)
        {
            if (pointsList[counter].position == transform.position)
            {
                if (counter - 1 <= -1)
                {
                    counter = pointsList.Count - 1;
                }
                else
                {
                    counter = counter - 1;
                }

            }
            if (Time.time > changeDirTime)
            {
                changeDir = false;

                GetComponent<ParticleSystem>().Stop();
                if (counter + 1 > pointsList.Count - 1)
                {
                    counter = 0;
                }
                else
                {
                    counter = counter + 1;
                }
            }
        }
        else
        {
            if (pointsList[counter].position == transform.position)
            {
                if (counter + 1 > pointsList.Count - 1)
                {
                    counter = 0;
                }
                else
                {
                    counter = counter + 1;
                }

            }
        }
        transform.position = Vector2.MoveTowards(transform.position, pointsList[counter].position, speed * Time.deltaTime);

        //nastavi rotacijo
        if (Vector3.Distance(transform.position, ceiling.transform.position) <
            Vector3.Distance(transform.position, floor.transform.position))
        {//bližje stropu

            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(transform.eulerAngles.z + rotateSpeed, 0, 180));

        }
        else
        {

            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(transform.eulerAngles.z - rotateSpeed, 0, 180));
        }

        //animacije
        if (transform.position.y == pointsList[counter].position.y)
        {
            runningAnimation();
            animator.speed = 0.7f;
        }
        else
        {
            inairAnimation();
        }

    }

    void inairAnimation()
    {
        animator.SetBool("running", false);
        animator.SetBool("inair", true);
        animator.SetBool("neutral-running", false);
        animator.SetBool("neutral-inair", false);
    }
    void runningAnimation()
    {
        animator.SetBool("running", true);
        animator.SetBool("inair", false);
        animator.SetBool("neutral-running", false);
        animator.SetBool("neutral-inair", false);
    }
    void runningNeutralAnimation()
    {
        animator.SetBool("running", false);
        animator.SetBool("inair", false);
        animator.SetBool("neutral-running", true);
        animator.SetBool("neutral-inair", false);
    }
    void inairNeutralAnimation()
    {
        animator.SetBool("running", false);
        animator.SetBool("inair", false);
        animator.SetBool("neutral-running", false);
        animator.SetBool("neutral-inair", true);
    }
}
