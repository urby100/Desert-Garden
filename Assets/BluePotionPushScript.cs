using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePotionPushScript : MonoBehaviour
{
    public GameObject player;
    public GameObject startPushPoint;
    public GameObject pushEffect;
    public GameObject cam4floorpos;
    public bool pushBack;
    Rigidbody2D rb;
    public GameObject cam;
    public Vector3 originalPos;

    float shakeAmount = 0.03f;
    float pushForce = 6;
    float pushTime;
    float pushLasts = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        originalPos = new Vector3(cam4floorpos.transform.position.x, cam4floorpos.transform.position.y,cam.transform.position.z);
    }
    public void setPush()
    {
        pushBack = true;
        pushEffect.SetActive(true);
        pushTime = Time.time + pushLasts;
    }
    void ShakeEffect()
    {
        cam.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (pushBack && Time.time<pushTime)
        {
            
            if (cam.GetComponent<BossCameraController>().positionName == 4 )
            {
                //če je med točkama 
                if (player.transform.position.x >= transform.position.x && player.transform.position.x <= startPushPoint.transform.position.x)
                {
                    player.GetComponent<Move>().sceneDontMoveRequest = true;
                    rb.velocity = new Vector2(-1 * pushForce, rb.velocity.y);
                    ShakeEffect();

                }
                //če ni več med točkama
                if (player.transform.position.x <= transform.position.x || player.transform.position.x >= startPushPoint.transform.position.x)
                {
                    player.GetComponent<Move>().sceneDontMoveRequest = false;
                    cam.transform.localPosition = originalPos;

                }
            }
        }
        if (pushBack && Time.time > pushTime)
        {
            if (cam.GetComponent<BossCameraController>().positionName == 4)
            {
                cam.transform.localPosition = originalPos;
            }
            pushBack = false;
            pushEffect.SetActive(false);
        }
    }
}
