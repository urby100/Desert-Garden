using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryScene : MonoBehaviour
{
    public GameObject scientist1;
    public List<GameObject> scientist1points;
    public GameObject scientist2;
    float scientistSpeed = 1.5f;
    public List<GameObject> scientist2points;
    public GameObject player;
    public List<GameObject> playerpoints;
    public GameObject copilot;
    public List<GameObject> copilotpoints;
    float sceneNumber = 1;
    public float movementSpeed = 4f;
    public TextMeshPro DialogText;
    public TextMeshPro skipBox;
    public GameObject playerSitting;
    public GameObject copilotSitting;
    public GameObject planeObject;
    public List<GameObject> planepoints;
    float planeSpeed = 2f;

    string dialog =
        "Hey there fellas, welcome to the super secret experimental military base." +
        " See what you need to do is test some super secret stuff for us." +
        " Drop the bomb on the super secret bomb site and thats it." +
        " Lots of luck.";
    string alpha = "<alpha=#00>";
    List<string> scientistsTalk = new List<string>() {
        "So what's for lunch.",
        "I have no idea. It's...",
        "Yeah, yeah, SUPER SECRET. That was funny 2 weeks ago."
    };
    float newLineDelay = 1f;
    float newLineTime;
    bool newLine = false;
    float typingSpeed = 0.05f;
    float typingTime;
    int iterator = 0;
    int iterator2 = 0;
    Vector2 move = Vector2.zero;
    float newsceneTime;
    float newsceneDelay=5f;
    // Start is called before the first frame update
    void Start()
    {
        skipBox.text = "Press "+ GetComponent<Keybindings>().attack1.ToString() + " to skip.";
        scientist1.transform.position = scientist1points[0].transform.position;
        scientist2.transform.position = scientist2points[0].transform.position;
        player.transform.position = playerpoints[0].transform.position;
        copilot.transform.position = copilotpoints[0].transform.position;
        planeObject.transform.position = planepoints[0].transform.position;
        turnObject(scientist1);
        turnObject(scientist2);
        //scientists still animation
        Scientist1standing();
        Scientist2standing();
        //
        DialogText.text = "";
        DialogText.gameObject.SetActive(false);
        skipBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (sceneNumber)
        {
            case 1://player in kopilot se sprehodita do prve točke
                move = Vector2.zero;
                if (player.transform.position.x < playerpoints[1].transform.position.x)
                {
                    move = playerpoints[1].transform.position - player.transform.position;
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
                    //
                }
                if (copilot.transform.position.x < copilotpoints[1].transform.position.x)
                {
                    move = copilotpoints[1].transform.position - copilot.transform.position;
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
                    //
                }
                if ((player.transform.position.x > playerpoints[1].transform.position.x) &&
                    (copilot.transform.position.x > copilotpoints[1].transform.position.x))
                {
                    sceneNumber = 2;
                    typingTime = Time.time + typingSpeed;
                    DialogText.text = alpha + dialog;
                }
                break;
            case 2://dialog 
                if (Input.GetKeyDown(GetComponent<Keybindings>().attack1))
                {
                    iterator = dialog.Length;
                }
                DialogText.gameObject.SetActive(true);
                skipBox.gameObject.SetActive(true);
                if (Time.time > typingTime)
                {
                    DialogText.text = DialogText.text.Replace(alpha, "");
                    DialogText.text = DialogText.text.Insert(iterator, alpha);
                    iterator++;
                    if (iterator > dialog.Length)
                    {
                        sceneNumber = 3;
                        turnObject(scientist1);
                        turnObject(scientist2);
                        //scientist wave animation
                        Scientist1waving();
                        Scientist2waving();
                        //
                    }
                    typingTime = Time.time + typingSpeed;
                }
                break;
            case 3:

                move = Vector2.zero;
                if (player.transform.position.x < playerpoints[2].transform.position.x)
                {
                    move = playerpoints[2].transform.position - player.transform.position;
                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * movementSpeed, player.GetComponent<Rigidbody2D>().velocity.y);

                    // walking animation
                    playerwalking();
                    //
                }
                else
                {
                    player.SetActive(false);
                    playerSitting.SetActive(true);
                }
                if (copilot.transform.position.x < copilotpoints[2].transform.position.x)
                {
                    move = copilotpoints[2].transform.position - copilot.transform.position;
                    copilot.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * movementSpeed, copilot.GetComponent<Rigidbody2D>().velocity.y);

                    // walking animation
                    copilotwalking();
                    //
                }
                else
                {
                    copilot.SetActive(false);
                    copilotSitting.SetActive(true);
                }
                if ((player.transform.position.x > playerpoints[2].transform.position.x) &&
                    (copilot.transform.position.x > copilotpoints[2].transform.position.x))
                {
                    sceneNumber = 4;
                }
                break;
            case 4://se odpeljeta z letalom
                DialogText.gameObject.SetActive(false);
                skipBox.gameObject.SetActive(false);
                planeObject.transform.position =
                    Vector3.MoveTowards(planeObject.transform.position, planepoints[1].transform.position, planeSpeed * Time.deltaTime);

                if (planeObject.transform.position.x == planepoints[1].transform.position.x)
                {
                    sceneNumber = 5;
                    iterator = 0;
                    typingTime = Time.time + typingSpeed;
                    DialogText.text = alpha + scientistsTalk[iterator2];
                    turnObject(scientist1);
                    turnObject(scientist2);

                }
                break;
            case 5://scientist walk and talk back to base
                   //scientist walk animation
                Scientist1walking();
                Scientist2walking();
                //
                DialogText.gameObject.SetActive(true);
                skipBox.gameObject.SetActive(true);
                if (Input.GetKeyDown(GetComponent<Keybindings>().attack1))
                {
                    iterator = scientistsTalk[iterator2].Length;
                }

                scientist1.transform.position =
                    Vector3.MoveTowards(scientist1.transform.position, scientist1points[1].transform.position, scientistSpeed * Time.deltaTime);
                scientist2.transform.position =
                    Vector3.MoveTowards(scientist2.transform.position, scientist2points[1].transform.position, scientistSpeed * Time.deltaTime);

                if (Time.time > typingTime)
                {
                    if (newLine && Time.time > newLineTime)
                    {
                        DialogText.text = alpha + scientistsTalk[iterator2];
                        newLine = false;
                    }
                    if (!newLine)
                    {
                        DialogText.text = DialogText.text.Replace(alpha, "");
                        DialogText.text = DialogText.text.Insert(iterator, alpha);
                        iterator++;
                        typingTime = Time.time + typingSpeed;
                    }
                    if (iterator > scientistsTalk[iterator2].Length)
                    {
                        iterator = 0;
                        iterator2++;
                        if (iterator2 == scientistsTalk.Count)
                        {
                            newsceneTime = Time.time + newsceneDelay;
                            sceneNumber = 6;
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
            case 6:
                scientist1.transform.position =
                    Vector3.MoveTowards(scientist1.transform.position, scientist1points[1].transform.position, scientistSpeed * Time.deltaTime);
                scientist2.transform.position =
                    Vector3.MoveTowards(scientist2.transform.position, scientist2points[1].transform.position, scientistSpeed * Time.deltaTime);

                if (Time.time > newsceneTime)
                {
                    SceneManager.LoadScene("Intro2");
                }
                break;
            default:
                break;
        }
    }
    void turnObject(GameObject go)
    {
        go.transform.Rotate(0, 180, 0, Space.World);
    }

    void Scientist1walking() {
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
    void Scientist2walking()
    {
        scientist2.GetComponent<Animator>().SetBool("walking", true);
        scientist2.GetComponent<Animator>().SetBool("standing", false);
        scientist2.GetComponent<Animator>().SetBool("waving", false);
    }
    void Scientist2standing()
    {
        scientist2.GetComponent<Animator>().SetBool("walking", false);
        scientist2.GetComponent<Animator>().SetBool("standing", true);
        scientist2.GetComponent<Animator>().SetBool("waving", false);
    }
    void Scientist2waving()
    {
        scientist2.GetComponent<Animator>().SetBool("walking", false);
        scientist2.GetComponent<Animator>().SetBool("standing", false);
        scientist2.GetComponent<Animator>().SetBool("waving", true);
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
}
