using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBirdyController : MonoBehaviour
{
    public GameObject birdyBody;
    Rigidbody2D birdyBodyRb;
    public List<GameObject> walkingCheckPoints;
    int pointsIterator;
    int numberOfPoints;
    public float movementSpeed = 1f;
    int direction = 1;
    public bool followFlower = false;
    public GameObject flower;
    // Start is called before the first frame update
    void Start()
    {
        birdyBodyRb = birdyBody.GetComponent<Rigidbody2D>();
        pointsIterator = 0;
        numberOfPoints = walkingCheckPoints.Count - 1;

    }

    // Update is called once per frame
    void Update()
    {

        if (direction == 1)
        {
            birdyBody.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (direction == -1)
        {
            birdyBody.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    public bool moveOnce = false;
    void FixedUpdate()
    {
        birdyBodyRb.velocity = new Vector2(-direction * movementSpeed, birdyBodyRb.velocity.y);
        if (Mathf.Abs(birdyBody.transform.position.x - walkingCheckPoints[pointsIterator].transform.position.x) < 0.5f)
        {

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
        if (followFlower)
        {
        }
        else
        {
            
        }
    }
}
