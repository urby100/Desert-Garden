using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleStevePreBossScene2 : MonoBehaviour
{
    public Vector3 to;
    float speedMultiplier = 6;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = transform.localScale.x * speedMultiplier;


    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Animator>().SetBool("running", true);
        transform.position = Vector3.MoveTowards(transform.position, to, speed * Time.deltaTime);
        if (transform.position == to) {
            Destroy(gameObject);
        }
    }
}
