using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonController : MonoBehaviour
{
    public GameObject ChameleonBody;
    public List<GameObject> CheckPoints;
    public GameObject playerObject;
    float attackSpeed = 12f;
    public bool neutral = false;
    public bool attack = false;
    public float attackTime;
    // Start is called before the first frame update
    void Start()
    {
        attackTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        //obrni proti playerju
        if (playerObject.transform.position.x > ChameleonBody.transform.position.x)
        {
            ChameleonBody.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            ChameleonBody.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        if (neutral)
        {

            return;
        }
        if (Time.time > attackTime && !attack)
        {
            //nastavi pozicijo - med dvema točkama 
            float x= Random.Range(CheckPoints[0].transform.position.x, CheckPoints[1].transform.position.x);
            ChameleonBody.transform.position = new Vector2(x,ChameleonBody.transform.position.y);

            attack = true;
            attackTime = Time.time + attackSpeed;
        }


    }
}
