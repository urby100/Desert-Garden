using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    public TextMeshPro speedText;
    public GameObject icons;
    public GameObject NormalPoint;
    public GameObject FastPoint;
    public GameObject LittleSteve;
    public Sprite emptyGuy;
    public Sprite fullGuy;

    public bool fast = false;
    bool setFast = false;
    
    Animator animator;
    
    float dist;
    float updateDelay = 0.3f;
    float updateTime;
    List<GameObject> iconsList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform icon in icons.transform)
        {
            iconsList.Add(icon.gameObject);
        }
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(LittleSteve.transform.position, FastPoint.transform.position);

        if (Time.time > updateTime)
        {

            int speed = (int)Mathf.Ceil(((100 - (dist * 100))));
            speedText.text = speed.ToString();
            updateTime = Time.time + updateDelay;
            int i = (int)Mathf.Floor(speed/25);
            int counter = 0;
            foreach (GameObject g in iconsList) {
                if (counter <= i)
                {
                    g.GetComponent<SpriteRenderer>().sprite = fullGuy;
                }
                else
                {
                    g.GetComponent<SpriteRenderer>().sprite = emptyGuy;
                }
                counter++;
            }

        }
        if (fast && !setFast)
        {
            setFast = true;
            SetAnimationFast();
        }
        if (!fast && setFast)
        {
            setFast = false;
            SetAnimationNormal();

        }
    }
    void SetAnimationFast()
    {
        animator.speed = 1.75f;
    }
    void SetAnimationNormal()
    {
        animator.speed = 1;
    }
}
