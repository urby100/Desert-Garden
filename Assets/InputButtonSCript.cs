using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputButtonSCript : MonoBehaviour
{
    public GameObject flower;
    bool input = false;

    void Update()
    {
        if (input)
        {
            if (Input.GetKeyDown("escape"))
            {
                input = false;
            }
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {//nastavi 
                    ChangeInput(vKey);
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
            PlayerPrefs.SetString("Horizontal", keypressed.ToString());
        }
        else if (gameObject.name == "BackwardButton")
        {
            PlayerPrefs.SetString("Horizontal", keypressed.ToString());
        }
        else if (gameObject.name == "JumpButton")
        {
            PlayerPrefs.SetString("Jump", keypressed.ToString());
        }
        else if (gameObject.name == "CrouchButton")
        {
            PlayerPrefs.SetString("Crouch", keypressed.ToString());
        }
        else if (gameObject.name == "Attack1Button")
        {
            PlayerPrefs.SetString("Attack1", keypressed.ToString());
        }
        else if (gameObject.name == "Attack2Button")
        {
            PlayerPrefs.SetString("Attack2", keypressed.ToString());
        }
    }
}
