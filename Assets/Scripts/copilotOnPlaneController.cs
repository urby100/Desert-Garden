using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copilotOnPlaneController : MonoBehaviour
{
    Animator animator;

    public GameObject playerObject;
    public GameObject bombObject;
    public GameObject screwdriverSpawnPoint;
    public GameObject screwdriver;

    float waveTime;
    float waveLasts=2f;
    bool throwScrewdriver = false;
    bool throwOnce = false;
    bool scene = false;
    bool turnAround = false;
    float turnTime;
    float turnDelay;
    bool playerNear = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        turnDelay = bombObject.GetComponent<bombUnderPlaneController>().explodeDelay-0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (scene) {
            if (Time.time > waveTime) {
                //vrži izvijač
                throwScrewdriver = true;
                animator.SetBool("wave",false);
                turnAround = true;
                turnTime = Time.time + turnDelay;
                scene = false;
                playerObject.GetComponent<Move>().sceneDontMoveRequest = false;//player se lahko začne premikati
            }
        }
        if (playerNear && !scene && !throwScrewdriver) {//ko je na določeni poziciji
            playerObject.GetComponent<Move>().sceneDontMoveRequest = true;//player se ne sme premikati
            scene = true;
            animator.SetBool("wave",true);
            waveTime = Time.time + waveLasts;
        }
        if (Time.time > turnTime && turnAround) {

            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            turnAround = false;
        }
    }
    void FixedUpdate()
    {
        if ((playerObject.transform.position.x - transform.position.x) >-6.5f  && !playerNear)
        {

            playerNear = true;
        }
        if (throwScrewdriver && !throwOnce) {
            GameObject screwdriverObject= Instantiate(screwdriver, screwdriverSpawnPoint.transform.position, screwdriverSpawnPoint.transform.rotation);
            screwdriverObject.GetComponent<Rigidbody2D>().AddForce((bombObject.transform.position-screwdriver.transform.position)*2f);//proti bombi
            screwdriverObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 150f);//malo gor
            screwdriverObject.GetComponent<Rigidbody2D>().AddTorque(5f*Time.deltaTime, ForceMode2D.Impulse);//zavrti
            screwdriverObject.name = "Screwdriver";
            throwOnce = true;
        }
    }
}
