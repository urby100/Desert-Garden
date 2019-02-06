using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherProjectileController : MonoBehaviour
{
    public GameObject crusher;
    public GameObject points;
    public List<Transform> pointsList;

    int counter = 0;
    int direction = 1;//naprej 1, nazaj 0
    float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform point in points.transform) {
            pointsList.Add(point);
        }
        transform.position = pointsList[counter].position;
    }
    
    void FixedUpdate()
    {
        if (pointsList[counter].position == transform.position) {
            counter=counter+direction;
            if (counter > pointsList.Count && direction == 1)
            {
                direction = -1;
                counter = counter + direction;
            }
            else if (counter < 0 && direction == -1) {

                direction = 1;
                counter = counter + direction;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, pointsList[counter].position, speed * Time.deltaTime);
        if (crusher.GetComponent<CrusherController>().destroyProjectiles) {
            Destroy(gameObject);
        }
    }
}
