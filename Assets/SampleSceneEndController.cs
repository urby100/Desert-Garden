using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleSceneEndController : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> playerpoints;
    public GameObject copilot;
    public List<GameObject> copilotpoints;
    float movementSpeed = 1.5f;
    public TextMeshPro DialogText;
    public TextMeshPro skipBox;
    List<string> Talk = new List<string>() {
        "Thanks for getting me out boss, it was really dark in there.",
        "So what now?",
        "Let's go back to the SUPER S...",
        "Let's go back to base.",
        "Do you think we'll be back by lunch time?"
    };
    string alpha = "<alpha=#00>";
    float newLineDelay = 1f;
    float newLineTime;
    bool newLine = false;
    float typingSpeed = 0.05f;
    float typingTime;
    int iterator = 0;
    int iterator2 = 0;
    Vector2 move = Vector2.zero;
    bool endBool = false;
    float endDelay = 3f;
    float endTime;
    // Start is called before the first frame update
    void Start()
    {
        skipBox.text = "Press " + GetComponent<Keybindings>().attack1.ToString() + " to skip.";

        player.transform.position = playerpoints[0].transform.position;
        copilot.transform.position = copilotpoints[0].transform.position;
        
        DialogText.gameObject.SetActive(false);
        skipBox.gameObject.SetActive(false);
        DialogText.text = alpha + Talk[iterator2];

    }

    // Update is called once per frame
    void Update()
    {
        move = playerpoints[1].transform.position - player.transform.position;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
        playerwalking();
        move = copilotpoints[1].transform.position - copilot.transform.position;
        copilot.GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, copilot.GetComponent<Rigidbody2D>().velocity.y);
        copilotwalking();

        DialogText.gameObject.SetActive(true);
        skipBox.gameObject.SetActive(true);
        if (Input.GetKeyDown(GetComponent<Keybindings>().attack1))
        {
            iterator = Talk[iterator2].Length;
        }
        
        if (Time.time > typingTime && !endBool)
        {
            if (newLine && Time.time > newLineTime)
            {
                DialogText.text = alpha + Talk[iterator2];
                newLine = false;
            }
            if (!newLine)
            {
                DialogText.text = DialogText.text.Replace(alpha, "");
                DialogText.text = DialogText.text.Insert(iterator, alpha);
                iterator++;
                typingTime = Time.time + typingSpeed;
            }
            if (iterator > Talk[iterator2].Length)
            {
                iterator = 0;
                iterator2++;
                if (iterator2 == Talk.Count)
                {
                    endBool = true;
                    endTime = Time.time + endDelay;
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
        if (endBool && Time.time>endTime)
        {
            SceneManager.LoadScene("PreBossScene1");
        }

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
