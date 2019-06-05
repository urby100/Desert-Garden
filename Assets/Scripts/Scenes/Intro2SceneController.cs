using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro2SceneController : MonoBehaviour
{

    public GameObject player;
    public GameObject playerFalling;
    public GameObject copilot;
    public TextMeshPro DialogText;
    public TextMeshPro SkipBox;
    public GameObject planeObject;
    public List<GameObject> planepoints;
    float planeSpeed = 4f;
    float sceneNumber = 1;

    float newsceneTime;
    float newsceneDelay = 5f;


    List<string> pilotsTalk = new List<string>() {
        "Hey, we're right above the drop zone. Pull the lever to drop the bomb.",
        "Uhhh, boss?",
        "What's up?",
        "The bomb got stuck.",
        "Alright, just use the screwdriver and drop it manually.",
        "Ok, be right back.",
        "How's it looking down there?",
        "Everything is labeled 'Super Secret'.",
        "Yeah, that scientist is taking this joke way too far.",
        "Ok, I think this is the right one."
    };
    string bailOut= "Wrong one. Bail out, Bail out!";
    float mistakeAwareDelay = 2f;
    float mistakeAwareTime;
    string huh = "Huh? ";
    string ohNo = "Oh boy.";

    string alpha = "<alpha=#00>";
    float newLineDelay = 1f;
    float newLineTime;
    bool newLine = false;
    float typingSpeed = 0.05f;
    float typingTime;
    int iterator = 0;
    int iterator2 = 0;
    bool move = true;
    float slowing = 1f;
    // Start is called before the first frame update
    void Start()
    {
        SkipBox.text = "Press " + GetComponent<Keybindings>().attack1.ToString() + " to skip.";
        playerOnPlane();
        DialogText.text = "";
        typingTime = Time.time;
        DialogText.text = alpha + pilotsTalk[0];

    }

    // Update is called once per frame
    void Update()
    {
        if (!move)
        {
            planeSpeed = planeSpeed - (slowing*Time.deltaTime);
            if (planeSpeed < 0) {
                planeSpeed = 0;
            }
        }
            planeObject.transform.position =
                Vector3.MoveTowards(planeObject.transform.position, planepoints[1].transform.position, planeSpeed * Time.deltaTime);
        switch (sceneNumber)
        {
            case 1:
                if (Input.GetKeyDown(GetComponent<Keybindings>().attack1))
                {
                    iterator = pilotsTalk[iterator2].Length;
                }
                if (Time.time > typingTime)
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
                        if (iterator2 == 6)
                        {
                            copilot.SetActive(false);
                        }
                        if (iterator2 == pilotsTalk.Count)
                        {
                            sceneNumber = 2;
                            move = false;
                            planeObject.transform.Find("PlaneBody").GetComponent<Animator>().enabled = false;
                            playerLooking();
                            newLineTime = Time.time + newLineDelay;
                            newLine = true;
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
                }
                break;
            case 2:
                SkipBox.gameObject.SetActive(false);
                if (Time.time > typingTime && Time.time > newLineTime)
                {
                    if (newLine)
                    {
                        DialogText.text = alpha + bailOut;
                        newLine = false;
                    }
                    else
                    {
                        DialogText.text = DialogText.text.Replace(alpha, "");
                        DialogText.text = DialogText.text.Insert(iterator, alpha);
                        iterator++;
                        if (iterator > bailOut.Length)
                        {
                            sceneNumber = 3;
                            player.SetActive(false);
                            playerFalling.SetActive(true);
                            playerFalling.GetComponent<Animator>().SetBool("falling", true);
                            mistakeAwareTime = Time.time + mistakeAwareDelay;
                            planeObject.transform.Find("Main Camera").parent = null;
                            planeObject.AddComponent<Rigidbody2D>();
                            planeObject.GetComponent<Rigidbody2D>().gravityScale = 0.01f;
                            planeObject.GetComponent<Rigidbody2D>().AddTorque(-2, ForceMode2D.Force);
                            planeObject.GetComponent<Rigidbody2D>().velocity = new Vector2(planeSpeed, planeObject.GetComponent<Rigidbody2D>().velocity.y);

                            iterator = 0;
                            newLine = true;
                            newLineTime = Time.time + newLineDelay;
                        }
                        typingTime = Time.time + typingSpeed;
                    }
                }
                break;
            case 3:
                if (Time.time > typingTime && Time.time > newLineTime)
                {
                    if (newLine)
                    {
                        DialogText.text = alpha + huh;
                        newLine = false;
                    }
                    else
                    {
                        if (iterator < huh.Length )
                        {
                            DialogText.text = DialogText.text.Replace(alpha, "");
                            DialogText.text = DialogText.text.Insert(iterator, alpha);
                        }
                        iterator++;
                        typingTime = Time.time + typingSpeed;
                    }
                }
                if (iterator > bailOut.Length)
                {
                    copilot.SetActive(true);
                    copilot.GetComponent<Animator>().enabled = false;
                    sceneNumber = 4;
                    iterator = 0;
                    newLine = true;
                    newLineTime = Time.time + newLineDelay;
                    planeObject.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
                    planeObject.GetComponent<Rigidbody2D>().AddTorque(-2, ForceMode2D.Force);
                }

                break;
            case 4:
                if (Time.time > typingTime && Time.time > newLineTime)
                {
                    if (newLine)
                    {
                        DialogText.text = alpha + ohNo;
                        newLine = false;
                        planeObject.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
                    }
                    else
                    {
                        DialogText.text = DialogText.text.Replace(alpha, "");
                        DialogText.text = DialogText.text.Insert(iterator, alpha);
                        iterator++;
                        if (iterator > ohNo.Length)
                        {
                            newsceneTime = Time.time + newsceneDelay;
                            sceneNumber = 5;
                        }
                        typingTime = Time.time + typingSpeed;
                    }
                }
                break;
            case 5:

                if (Time.time > newsceneTime)
                {
                    SceneManager.LoadScene("SampleScene");
                }
                break;
            default:
                break;
        }
    }
    void playerLooking()
    {
        player.GetComponent<Animator>().SetBool("onplane", false);
        player.GetComponent<Animator>().SetBool("looking", true);
    }
    void playerOnPlane()
    {
        player.GetComponent<Animator>().SetBool("onplane", true);
        player.GetComponent<Animator>().SetBool("looking", false);
    }
}

