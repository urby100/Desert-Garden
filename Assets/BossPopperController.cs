using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPopperController : MonoBehaviour
{
    public GameObject popEffect;
    bool effect = false;
    public float moveSpeed = 2f;
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

    // Update is called once per frame
    void Update()
    {
        if (moveIt) {
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
                                                                moveSpeed*Time.deltaTime);
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
    void Effect() {
        var em = popEffect.GetComponent<ParticleSystem>().emission;
        em.rateOverTime = 120;
        var gm = popEffect.GetComponent<ParticleSystem>().main.gravityModifier;
        gm.constant = Random.Range(1.5f, 2);
        var sh = popEffect.GetComponent<ParticleSystem>().shape;
        sh.shapeType = ParticleSystemShapeType.Sphere;
        float dir=-1;
        if (targetDown.position.y > targetUp.position.y) {
            dir = 1;
        }
        GameObject particle = Instantiate(popEffect, gameObject.transform.position + new Vector3(0, dir*0.5f, 0), gameObject.transform.rotation);
        particle.name = "PopEffectPopper";
        particle.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        Destroy(particle, 14f);
        effect = true;
    }
}
