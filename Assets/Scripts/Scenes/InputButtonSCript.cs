using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputButtonSCript : MonoBehaviour
{
    public GameObject flower;
    bool input = false;
    public GameManager gameManager;

    void Awake() {
        if (gameObject.name == "ForwardButton")
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("forward", "D");
        }
        else if (gameObject.name == "BackwardButton")
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("backward", "A");
        }
        else if (gameObject.name == "JumpButton")
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("jump", "W");
        }
        else if (gameObject.name == "CrouchButton")
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("crouch", "S");
        }
        else if (gameObject.name == "Attack1Button")
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("attack1", "Space");
        }
        else if (gameObject.name == "Attack2Button")
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("attack2", "V");
        }
    }

    void Update()
    {
        if (input)
        {
            if (Input.GetKeyDown("escape"))
            {
                input = false;
                flower.SetActive(false);
            }
            else {

                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(vKey))
                    {//nastavi 
                        ChangeInput(vKey);
                    }
                }
            }
        }
    }
    public void AskForInput()
    {
        flower.SetActive(true);
        input = true;
    }

    void ChangeInput(KeyCode keypressed)
    {
        input = false;
        flower.SetActive(false);
        gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = keypressed.ToString();

        if (gameObject.name == "ForwardButton")
        {
            PlayerPrefs.SetString("forward", keypressed.ToString());
        }
        else if (gameObject.name == "BackwardButton")
        {
            PlayerPrefs.SetString("backward", keypressed.ToString());
        }
        else if (gameObject.name == "JumpButton")
        {
            PlayerPrefs.SetString("jump", keypressed.ToString());
        }
        else if (gameObject.name == "CrouchButton")
        {
            PlayerPrefs.SetString("crouch", keypressed.ToString());
        }
        else if (gameObject.name == "Attack1Button")
        {
            PlayerPrefs.SetString("attack1", keypressed.ToString());
        }
        else if (gameObject.name == "Attack2Button")
        {
            PlayerPrefs.SetString("attack2", keypressed.ToString());
        }
        if (gameManager != null)
        {
            gameManager.GetComponent<Keybindings>().Reset();
        }
    }
}
