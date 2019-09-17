using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrusherProjectileController : MonoBehaviour
{
    public GameObject crusher;
    public GameObject points;
    public bool onPlayer = false;
    public List<Transform> pointsList;
    public float attackLastTime;
    float PushAwaySpeed = 8f;
    public float speed = 2f;

    int counter = 0;
    float revSpeed = -270f;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform point in points.transform)
        {
            pointsList.Add(point);
        }
        transform.position = pointsList[counter].position;
    }
    void FixedUpdate()
    {
        if (Time.time > attackLastTime)
        {
            Destroy(gameObject);
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, transform.localRotation.eulerAngles.z + revSpeed * Time.deltaTime));
        if (crusher.GetComponent<BossCrusherController>().destroyProjectiles)
        {
            attackLastTime = Time.time + 0.2f;
            transform.Translate(Vector3.left * PushAwaySpeed * speed * Time.deltaTime,Space.World);
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
            transform.position = Vector2.MoveTowards(transform.position, pointsList[counter].position, speed * Time.deltaTime);
        }

    }
}
