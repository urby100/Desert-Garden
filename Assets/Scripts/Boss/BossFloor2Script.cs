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
    float multipleCrushersTimeDelay = 0.6f;
    int counter = 0;
    float attackTime;
    float attackDelay;
    float attackDelayDelay = 4f;//vsake 4sekunde napade
    // Start is called before the first frame update
    void Start()
    {
        attackDelay = bossCrushers[0].GetComponent<BossCrusherController>().attackLast + attackDelayDelay;
        rb = player.GetComponent<Rigidbody2D>();
        attackTime = Time.time + 4.1f;//za prfect run
    }
    int maxProjectiles = 12;
    int multipierDelay = 1;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > attackTime)
        {
            counter = 0;
            foreach (GameObject go in bossCrushers)
            {
                if (!go.GetComponent<BossCrusherController>().attackSet)
                {
                    go.GetComponent<BossCrusherController>().maxProjectiles = maxProjectiles;
                    go.GetComponent<BossCrusherController>().setAttack(multipierDelay*counter * multipleCrushersTimeDelay);
                    attackTime = Time.time + attackDelay;
                }
                counter++;
            }
        }


        if (!pushOnce && player.transform.position.x > pushBackPoint.transform.position.x && !pushBack && cam.GetComponent<BossCameraController>().positionName == 2)
        {
            pushBack = true;
            maxProjectiles = 0;
            multipierDelay = 0;
            foreach (GameObject go in bossCrushers)
            {
                go.GetComponent<BossCrusherController>().destroyProjectiles = true;
                go.GetComponent<BossCrusherController>().attackSet = false;
                go.GetComponent<BossCrusherController>().attack = false;
                
            }
            attackTime = Time.time;
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
                maxProjectiles = 12;
                multipierDelay = 1;
                attackTime = Time.time;
                foreach (GameObject go in bossCrushers)
                {
                    go.GetComponent<BossCrusherController>().destroyProjectiles = false;
                    go.GetComponent<BossCrusherController>().counter = 0;
                    go.GetComponent<BossCrusherController>().attackSet = false;
                    go.GetComponent<BossCrusherController>().attack = false;
                    go.GetComponent<BossCrusherController>().attackLastTime = Time.time;
                }
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
