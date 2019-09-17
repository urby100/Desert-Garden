using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrushersCatcher : MonoBehaviour
{
    
    public TextMeshPro speedText;
    public GameObject icons;
    public Sprite emptyFlower;
    public Sprite fullFlower;

    float updateDelay = 0.3f;
    float updateTime;
    int counter = 0;
    List<GameObject> iconsList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform icon in icons.transform)
        {
            iconsList.Add(icon.gameObject);
        }
        InvokeRepeating("CheckSpeed", 0f, 1.6f);
    }

    // Update is called once per frame
    void Update()
    {
        {

            speedText.text = counter.ToString();
            updateTime = Time.time + updateDelay;
            int i = (int)Mathf.Floor(counter / 4);
            int c = 0;
            foreach (GameObject g in iconsList)
            {
                if (c <= i)
                {
                    g.GetComponent<SpriteRenderer>().sprite = fullFlower;
                }
                else
                {
                    g.GetComponent<SpriteRenderer>().sprite = emptyFlower;
                }
                c++;
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        counter++;
    }
    void CheckSpeed()
    {
        counter = 0;
    }
}
