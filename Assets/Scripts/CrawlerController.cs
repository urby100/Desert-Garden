using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerController : MonoBehaviour
{
    public GameObject crawlerBody;
    Rigidbody2D crawlerBodyRb;
    public List<GameObject> walkingCheckPoints;
    int pointsIterator;
    int numberOfPoints;
    float movementSpeed=5f;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        crawlerBodyRb = crawlerBody.GetComponent<Rigidbody2D>();
        pointsIterator = 0;
        numberOfPoints = walkingCheckPoints.Count-1;
    }
    private void Update()
    {

        if (direction == 1)
        {
            crawlerBody.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (direction == -1)
        {
            crawlerBody.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 move= Vector2.zero;
        if (Vector2.Distance(crawlerBody.transform.position, walkingCheckPoints[pointsIterator].transform.position) > 0.5f)
        {
            move = walkingCheckPoints[pointsIterator].transform.position - crawlerBody.transform.position;
            crawlerBodyRb.velocity=move.normalized * movementSpeed;
        }
        else {

            pointsIterator = pointsIterator + direction;
            if (pointsIterator >= numberOfPoints)
            {
                direction = -1;
                pointsIterator = numberOfPoints;
            }
            else if (pointsIterator <= 0)
            {
                direction = 1;
                pointsIterator = 0;
            }
        }
    }
}
