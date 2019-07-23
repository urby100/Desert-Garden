using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreBossScene1Controller : MonoBehaviour
{
    public GameObject explosionGameObject;
    List<ParticleSystem>[] particles=new List<ParticleSystem>[3];
    public Transform camTransform;
    bool effectOnce = false;
    bool shake = false;
    
    float shakeDuration = 1f;
    float shakeAmount = 0.1f;
    float decreaseFactor = 1.0f;

    Vector3 originalPos;
    public GameObject Base;
    Animator BaseAnim;
    int BaseState = 1;
    public GameObject scientist1;
    public List<GameObject> scientist1points;
    float scientistSpeed = 2f;
    public GameObject player;
    public List<GameObject> playerpoints;
    public GameObject copilot;
    public List<GameObject> copilotpoints;
    float sceneNumber = 1;
    public float movementSpeed = 4f;
    public TextMeshPro DialogText;
    public TextMeshPro skipBox;
    string alpha = "<alpha=#00>";
    List<string> scientistTalk = new List<string>() {
        "Oh, hello.",
        "My colleague got all crazy and I think he needs some space, so I'm going home.",
        //Explosion - turns - waits a few - turns back
        "Anyway...",
        "See you tomorrow."
    };
    bool turnOnce = false;
    bool explosion = false;
    bool exploded = false;
    float explosionTime;
    float explosionDelay = 2f;
    int explosionCounter = 0;

    List<string> pilotsTalk = new List<string>() {
        "We should go help him.",
        //Bigger Explosion
        "Uhh, boss.",
        "My bus is coming in a few minutes.",
        "And I was thinking if I could leave early today?",
        //Even Bigger Explosion
        "OKAY, BYE!"
    };
    float newLineDelay = 1f;
    float newLineTime;
    bool newLine = false;
    float typingSpeed = 0.05f;
    float typingTime;
    int iterator = 0;
    int iterator2 = 0;
    float waitForExplosion;
    Vector2 move = Vector2.zero;
    int c = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        particles[0] = new List<ParticleSystem>();
        particles[1] = new List<ParticleSystem>();
        particles[2] = new List<ParticleSystem>();
        foreach (Transform t in explosionGameObject.transform)
        {
            if (c >= particles.Length) {
                break;
            }
            GetExplosionParticles(t);
            c++;
        }

        camTransform = Camera.main.transform;
        originalPos = camTransform.position;
        BaseAnim = Base.GetComponent<Animator>();
        skipBox.text = "Press " + GetComponent<Keybindings>().attack1.ToString() + " to skip.";
        scientist1.transform.position = scientist1points[0].transform.position;
        player.transform.position = playerpoints[0].transform.position;
        copilot.transform.position = copilotpoints[0].transform.position;

        turnObject(scientist1);
        Scientist1standing();

        DialogText.text = "";
        DialogText.gameObject.SetActive(false);
        skipBox.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        switch (sceneNumber)
        {
            case 1:
                SetBaseAnim();
                Scientist1waving();
                move = Vector2.zero;
                bool arrived1 = false;
                bool arrived2 = false;
                if (player.transform.position.x < playerpoints[1].transform.position.x)
                {
                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * movementSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
                    // walking animation
                    playerwalking();
                    //
                }
                else
                {
                    player.GetComponent<Rigidbody2D>().velocity = move;
                    //still animation
                    playerstanding();
                    arrived1 = true;
                    //
                }
                if (copilot.transform.position.x < copilotpoints[1].transform.position.x)
                {
                    copilot.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * movementSpeed, copilot.GetComponent<Rigidbody2D>().velocity.y);

                    // walking animation
                    copilotwalking();
                    //
                }
                else
                {
                    copilot.GetComponent<Rigidbody2D>().velocity = move;
                    //still animation
                    copilotstanding();
                    arrived2 = true;
                    //
                }
                if (arrived1 && arrived2)
                {
                    sceneNumber++;
                    typingTime = Time.time + typingSpeed;
                    DialogText.text = alpha + scientistTalk[0];
                }
                break;
            case 2:
                SetBaseAnim();
                if (Input.GetKeyDown(GetComponent<Keybindings>().attack1) && !explosion)
                {
                    iterator = scientistTalk[iterator2].Length;
                }
                if (explosion)
                {

                    if (Time.time > waitForExplosion)
                    {
                        BaseState = 2;
                        if (!turnOnce)
                        {
                            turnObject(scientist1);
                            DialogText.gameObject.SetActive(false);
                            skipBox.gameObject.SetActive(false);
                            explosionTime = Time.time + explosionDelay;
                            turnOnce = true;
                        }
                        //camera shake
                        //explosions
                        Explosion();
                        if (Time.time > explosionTime)
                        {
                            explosion = false;
                            exploded = true;
                            effectOnce = false;
                            shakeDuration = 1f;
                            shakeAmount =0.12f;
                            turnObject(scientist1);
                        }
                    }
                }
                else
                {
                   
                    DialogText.gameObject.SetActive(true);
                    skipBox.gameObject.SetActive(true);
                }
                if (Time.time > typingTime && !explosion)
                {
                    if (newLine && Time.time > newLineTime)
                    {
                        DialogText.text = alpha + scientistTalk[iterator2];
                        newLine = false;
                    }
                    if (!newLine)
                    {
                        DialogText.text = DialogText.text.Replace(alpha, "");
                        DialogText.text = DialogText.text.Insert(iterator, alpha);
                        iterator++;
                        typingTime = Time.time + typingSpeed;
                    }

                    if (iterator > scientistTalk[iterator2].Length)
                    {
                        iterator = 0;
                        iterator2++;
                        if (iterator2 == scientistTalk.Count)
                        {
                            sceneNumber++;
                            iterator = 0;
                            iterator2 = 0;
                        }
                        else
                        {//novi stavek
                            if (!newLine)
                            {
                                newLineTime = Time.time + newLineDelay;
                                newLine = true;
                            }

                        }
                    }
                    if (iterator2 >= 2 && exploded)
                    {
                        scientist1.transform.position =
                            Vector3.MoveTowards(scientist1.transform.position,
                                        scientist1points[1].transform.position, scientistSpeed * Time.deltaTime);
                        // walking animation
                        Scientist1walking();
                    }
                    else {
                        Scientist1standing();
                    }
                    if (iterator2 == 2 && !explosion && !exploded)
                    {
                        waitForExplosion = Time.time + newLineDelay;
                        explosion = true;
                    }
                }
                break;
            case 3:

                SetBaseAnim();
                scientist1.transform.position =
                    Vector3.MoveTowards(scientist1.transform.position,
                                scientist1points[1].transform.position, scientistSpeed * Time.deltaTime);
                // walking animation
                Scientist1walking();
                if (scientist1.transform.position == scientist1points[1].transform.position) {
                    sceneNumber++;
                    DialogText.text = alpha + pilotsTalk[iterator2];
                    exploded = false;
                    explosion = false;
                    turnOnce = false;

                }
                break;
            case 4:

                SetBaseAnim();
                if (Input.GetKeyDown(GetComponent<Keybindings>().attack1))
                {
                    iterator = pilotsTalk[iterator2].Length;
                }
                if (explosion)
                {
                    if (Time.time > waitForExplosion)
                    {
                        BaseState = 2+explosionCounter;
                        if (!turnOnce)
                        {
                            DialogText.gameObject.SetActive(false);
                            skipBox.gameObject.SetActive(false);
                            explosionTime = Time.time + explosionDelay;
                            turnOnce = true;
                        }
                        //camera shake
                        //explosions
                        Explosion();
                        if (Time.time > explosionTime)
                        {
                            explosion = false;
                            exploded = true;
                            turnOnce = false;
                            effectOnce = false;
                            shakeDuration = 1f;
                            shakeAmount =0.14f;
                        }
                    }
                }
                else
                {

                    DialogText.gameObject.SetActive(true);
                    skipBox.gameObject.SetActive(true);
                }
                if (Time.time > typingTime && !explosion)
                {
                    if (newLine && Time.time > newLineTime)
                    {
                        DialogText.text = alpha + pilotsTalk[iterator2];
                        newLine = false;
                    }
                    if (!newLine)
                    {
                        DialogText.text = DialogText.text.Replace(alpha, "");
                        DialogText.text = DialogText.text.Insert(iterator, alpha);
                        iterator++;
                        typingTime = Time.time + typingSpeed;
                    }
                    if (iterator > pilotsTalk[iterator2].Length)
                    {
                        iterator = 0;
                        iterator2++;
                        if (iterator2 == 2) {
                            exploded = false;
                        }
                        if (iterator2 == pilotsTalk.Count)
                        {
                            sceneNumber ++;
                        }
                        else
                        {//novi stavek
                            if (!newLine)
                            {
                                newLineTime = Time.time + newLineDelay;
                                newLine = true;
                            }

                        }
                        if (iterator2 == 1 && !explosion && !exploded)
                        {
                            explosionCounter++;
                            waitForExplosion = Time.time + newLineDelay;
                            explosion = true;
                        }

                        if (iterator2 == 4 && !explosion && !exploded)
                        {
                            explosionCounter++;
                            waitForExplosion = Time.time + newLineDelay;
                            explosion = true;
                            turnOnce = false;
                        }
                        if (iterator2 >= 4 && exploded)
                        {
                            if (!turnOnce) {

                                turnObject(copilot);
                                turnOnce = true;
                            }

                            copilot.GetComponent<Rigidbody2D>().velocity = new Vector2(-movementSpeed, copilot.GetComponent<Rigidbody2D>().velocity.y);
                            // walking animation
                            copilotwalking();
                        }
                    }
                }
                break;
            case 5:
                if (copilot.transform.position.x > copilotpoints[0].transform.position.x)
                {
                    copilot.GetComponent<Rigidbody2D>().velocity = new Vector2(-movementSpeed, copilot.GetComponent<Rigidbody2D>().velocity.y);

                    // walking animation
                    copilotwalking();
                    //
                }
                else
                {
                    sceneNumber++;
                    DialogText.gameObject.SetActive(false);
                    skipBox.gameObject.SetActive(false);
                }
                break;
            case 6:
                if (player.transform.position.x < playerpoints[2].transform.position.x)
                {
                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
                    // walking animation
                    playerwalking();
                    //
                }
                else
                {
                    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    playerstanding();
                    //Load new scene here.

                    SceneManager.LoadScene("PreBossScene2");
                }
                break;

        }
    }
    void ShakeEffect()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.2f, player.GetComponent<Rigidbody2D>().velocity.y);
        copilot.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.2f, player.GetComponent<Rigidbody2D>().velocity.y);
        scientist1.transform.position =Vector3.MoveTowards(scientist1.transform.position,
                                 scientist1points[1].transform.position, 0.2f * Time.deltaTime);

        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
    void Explosion()
    {
        ShakeEffect();
        if (!effectOnce) {
            foreach (ParticleSystem myParticleSystem in particles[explosionCounter]) {
                myParticleSystem.time = 0;
                myParticleSystem.Play();
            }
            effectOnce = true;
        }
    }
    void Scientist1walking()
    {
        scientist1.GetComponent<Animator>().SetBool("walking", true);
        scientist1.GetComponent<Animator>().SetBool("standing", false);
        scientist1.GetComponent<Animator>().SetBool("waving", false);
    }
    void Scientist1standing()
    {
        scientist1.GetComponent<Animator>().SetBool("walking", false);
        scientist1.GetComponent<Animator>().SetBool("standing", true);
        scientist1.GetComponent<Animator>().SetBool("waving", false);
    }
    void Scientist1waving()
    {
        scientist1.GetComponent<Animator>().SetBool("walking", false);
        scientist1.GetComponent<Animator>().SetBool("standing", false);
        scientist1.GetComponent<Animator>().SetBool("waving", true);
    }
    void playerwalking()
    {
        player.GetComponent<Animator>().SetBool("walking", true);
        player.GetComponent<Animator>().SetBool("standing", false);
    }
    void playerstanding()
    {
        player.GetComponent<Animator>().SetBool("walking", false);
        player.GetComponent<Animator>().SetBool("standing", true);
    }
    void copilotwalking()
    {
        copilot.GetComponent<Animator>().SetBool("walking", true);
        copilot.GetComponent<Animator>().SetBool("standing", false);
    }
    void copilotstanding()
    {
        copilot.GetComponent<Animator>().SetBool("walking", false);
        copilot.GetComponent<Animator>().SetBool("standing", true);
    }
    void turnObject(GameObject go)
    {
        go.transform.Rotate(0, 180, 0, Space.World);
    }
    void BaseNormal() {
        BaseAnim.SetBool("Normal",true);
        BaseAnim.SetBool("Explosion 1", false);
        BaseAnim.SetBool("Explosion 2", false);
        BaseAnim.SetBool("Explosion 3", false);

    }
    void BaseExplosion1()
    {
        BaseAnim.SetBool("Normal", false);
        BaseAnim.SetBool("Explosion 1", true);
        BaseAnim.SetBool("Explosion 2", false);
        BaseAnim.SetBool("Explosion 3", false);
    }
    void BaseExplosion2()
    {
        BaseAnim.SetBool("Normal", false);
        BaseAnim.SetBool("Explosion 1", false);
        BaseAnim.SetBool("Explosion 2", true);
        BaseAnim.SetBool("Explosion 3", false);
    }
    void BaseExplosion3()
    {
        BaseAnim.SetBool("Normal", false);
        BaseAnim.SetBool("Explosion 1", false);
        BaseAnim.SetBool("Explosion 2", false);
        BaseAnim.SetBool("Explosion 3", true);
    }
    void SetBaseAnim() {
        switch (BaseState)
        {
            case 1:
                BaseNormal();
                break;
            case 2:
                BaseExplosion1();
                break;
            case 3:
                BaseExplosion2();
                break;
            case 4:
                BaseExplosion3();
                break;
            default:
                break;
        }
    }
    void GetExplosionParticles(Transform t) {
        foreach (Transform child in t)
        {
            if (child.gameObject.GetComponent<ParticleSystem>())
            {
                particles[c].Add(child.gameObject.GetComponent<ParticleSystem>());
            }
            if (child.childCount > 0)
            {
                GetExplosionParticles(child);

            }
        }
    }

}
