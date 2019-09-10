using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdyController : MonoBehaviour
{
    public GameObject birdyBody;
    Rigidbody2D birdyBodyRb;
    public List<GameObject> walkingCheckPoints;
    int pointsIterator;
    int numberOfPoints;
    public float movementSpeed = 3f;
    public float jumpForce = 5f;
    public float attackSpeed = 3f;
    float fallMultiplier = 0.6f;
    int direction = 1;
    public bool attack = false;
    public bool neutral = false;
    public float attackTime;
    // Start is called before the first frame update
    void Start()
    {
        birdyBodyRb = birdyBody.GetComponent<Rigidbody2D>();
        pointsIterator = 0;
        numberOfPoints = walkingCheckPoints.Count - 1;
        attackTime = Time.time+attackSpeed;

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
    void FixedUpdate()
    {
        //better jump
        if (birdyBodyRb.velocity.y < 0)
        {
            birdyBodyRb.gravityScale = fallMultiplier;
        }
        else
        {
            birdyBodyRb.gravityScale = 1f;
        }
        birdyBodyRb.velocity = new Vector2(-direction * movementSpeed, birdyBodyRb.velocity.y);
        
        if (Mathf.Abs(birdyBody.transform.position.x- walkingCheckPoints[pointsIterator].transform.position.x) < 0.5f )
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
        if (attack && birdyBodyRb.velocity.y == 0)
        {
            attackTime = Time.time + attackSpeed;
            attack = false;
        }
        if (attackTime < Time.time && !attack)
        {
            if(!neutral){
                birdyBodyRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                attack = true;
            }
        }
    }
}
