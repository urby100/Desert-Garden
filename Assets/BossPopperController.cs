using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPopperController : MonoBehaviour
{
    GameObject prevPopper;
    GameObject nextPopper;
    GameObject prevPopperBody;
    GameObject nextPopperBody;

    public GameObject popEffect;
    bool effect = false;
    float moveSpeed = 1f;
    public GameObject playerObject;
    public GameObject popperBody;
    float popTime;
    public bool moveIt = true;
    public bool direction = true;// gor ali dol
    public Transform targetUp;
    public Transform targetDown;
    Vector3 velocity = Vector3.zero;
    Transform middle;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void setMoveSpeed(float ms) {
        moveSpeed = ms;
    }
    public void setMoveDelay(float md) {
        moveDelay = md;
    }
    public bool checkIfDown()
    {
        bool b = false ;
        if (targetDown.position.y == popperBody.transform.position.y)
        {
            b = true;
        }
        return b;
    }
    public bool checkIfUp()
    {
        bool b = false;
        if (targetUp.position.y == popperBody.transform.position.y)
        {
            b = true;
        }
        return b;
    }
    public void setPrevPopper(GameObject pp)
    {
        prevPopper = pp;
        prevPopperBody = pp.transform.Find("PopperBody").gameObject;
    }
    public void setNextPopper(GameObject np)
    {
        nextPopper = np;
        nextPopperBody = np.transform.Find("PopperBody").gameObject;

    }
    bool moveDownOnce = false;
    bool moveUpOnce = false;
    bool setTimeOnce = false;
    float moveDelay = 0.15f;
    float moveTime;
    // Update is called once per frame
    void Update()
    {
        if (prevPopper != null && nextPopper != null)
        {
            if (!prevPopper.GetComponent<BossPopperController>().direction && prevPopper.GetComponent<BossPopperController>().checkIfDown() && checkIfUp())
            {
                if (!moveDownOnce)
                {
                    if (!setTimeOnce) {
                        moveTime = Time.time + moveDelay;
                        setTimeOnce = true;
                    }
                    if (Time.time > moveTime && setTimeOnce)
                    {
                        direction = false;
                        moveDownOnce = true;
                        moveUpOnce = false;
                        setTimeOnce = false;
                    }
                }
            }
            if (checkIfDown() && !nextPopper.GetComponent<BossPopperController>().direction && nextPopper.GetComponent<BossPopperController>().checkIfDown()) {
                if (!moveUpOnce)
                {
                    if (!setTimeOnce)
                    {
                        moveTime = Time.time + moveDelay;
                        setTimeOnce = true;
                    }
                    if (Time.time > moveTime && setTimeOnce)
                    {
                        direction = true;
                        moveUpOnce = true;
                        moveDownOnce = false;
                        setTimeOnce = false;
                    }
                }
            }
        }

        if (targetDown.position.y > targetUp.position.y)
        {
            if (playerObject.transform.position.x > popperBody.transform.position.x)
            {
                popperBody.transform.eulerAngles = new Vector3(
                                                                popperBody.transform.eulerAngles.x,
                                                                -180,
                                                                popperBody.transform.eulerAngles.z);
            }
            else
            {
                popperBody.transform.eulerAngles = new Vector3(
                                                              popperBody.transform.eulerAngles.x,
                                                              0,
                                                              popperBody.transform.eulerAngles.z);
            }
        }
        else
        {
            if (playerObject.transform.position.x > popperBody.transform.position.x)
            {
                popperBody.transform.eulerAngles = new Vector3(
                                                                popperBody.transform.eulerAngles.x,
                                                                0,
                                                                popperBody.transform.eulerAngles.z);
            }
            else
            {
                popperBody.transform.eulerAngles = new Vector3(
                                                              popperBody.transform.eulerAngles.x,
                                                              -180,
                                                              popperBody.transform.eulerAngles.z);
            }
        }
        if (moveIt)
        {
            move();
        }
    }
    public void move()
    {
        if (direction)
        {
            if (!effect)
            {
                Effect();
            }
            popperBody.transform.position = Vector3.SmoothDamp(popperBody.transform.position,
                                                                targetUp.position,
                                                                ref velocity,
                                                                moveSpeed * Time.deltaTime);
        }
        else
        {
            effect = false;
            popperBody.transform.position = Vector3.SmoothDamp(popperBody.transform.position,
                                                                targetDown.position,
                                                                ref velocity,
                                                                moveSpeed * Time.deltaTime);
        }

    }
    void Effect()
    {
        var em = popEffect.GetComponent<ParticleSystem>().emission;
        em.rateOverTime = 120;
        var gm = popEffect.GetComponent<ParticleSystem>().main.gravityModifier;
        gm.constant = Random.Range(1.5f, 2);
        var sh = popEffect.GetComponent<ParticleSystem>().shape;
        sh.shapeType = ParticleSystemShapeType.Sphere;
        float dir = -1;
        if (targetDown.position.y > targetUp.position.y)
        {
            dir = 1;
        }
        GameObject particle = Instantiate(popEffect, gameObject.transform.position + new Vector3(0, dir * 0.5f, 0), gameObject.transform.rotation);
        particle.name = "PopEffectPopper";
        particle.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        Destroy(particle, 14f);
        effect = true;
    }
}
