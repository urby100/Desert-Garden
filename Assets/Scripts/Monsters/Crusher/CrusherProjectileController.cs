using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherProjectileController : MonoBehaviour
{
    public GameObject crusher;
    public GameObject points;
    public List<Transform> pointsList;

    int counter = 0;
    int direction = 1;//naprej 1, nazaj -1
    float speed = 2f;
    float revSpeed = -270f;
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
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, transform.localRotation.eulerAngles.z + revSpeed * Time.deltaTime));
        if (pointsList[counter].position == transform.position) {
            if (counter + direction > pointsList.Count-1 && direction == 1)
            {
                counter = 0;
            }
            else {
                counter = counter + direction;
            }

        }
        transform.position = Vector2.MoveTowards(transform.position, pointsList[counter].position, speed * Time.deltaTime);
        if (crusher.GetComponent<CrusherController>().destroyProjectiles) {
            Destroy(gameObject);
        }
    }
}
