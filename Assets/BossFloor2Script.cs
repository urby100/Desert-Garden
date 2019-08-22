using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFloor2Script : MonoBehaviour
{
    public GameObject pushBackPoint;
    public GameObject StopPushBackPoint;
    public GameObject player;
    public GameObject PushEffects;
    bool pushBack = false;
    bool pushOnce = false;
    public GameObject cam;
    public Vector3 originalPos;
    float pushForce = 6;
    Rigidbody2D rb;
    public List<GameObject> bossCrushers;

    float shakeAmount = 0.03f;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    float multipleCrushersTimeDelay = 0.5f;
    int counter = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        counter = 0;
        foreach (GameObject go in bossCrushers)
        {
            if (!go.GetComponent<BossCrusherController>().attackSet)
            {
                go.GetComponent<BossCrusherController>().setAttack(counter * multipleCrushersTimeDelay);
            }
            counter++;
        }


        if (!pushOnce && player.transform.position.x > pushBackPoint.transform.position.x && !pushBack && cam.GetComponent<BossCameraController>().positionName == "2")
        {
            pushBack = true;
            player.GetComponent<Move>().sceneDontMoveRequest = true;
            originalPos = cam.transform.position;
            PushEffects.SetActive(true);
        }
        if (pushBack)
        {
            rb.velocity = new Vector2(-1 * pushForce, rb.velocity.y);
            ShakeEffect();
            if (player.transform.position.x < StopPushBackPoint.transform.position.x)
            {
                cam.transform.localPosition = originalPos;
                pushBack = false;
                pushOnce = true;
                player.GetComponent<Move>().sceneDontMoveRequest = false;
                foreach (Transform go in PushEffects.transform)
                {
                    go.gameObject.GetComponent<ParticleSystem>().Stop();
                }
            }
        }
    }
    void ShakeEffect()
    {
        cam.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
    }
}
