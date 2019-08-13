using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject player;

    public void PlayGame()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            SceneManager.LoadScene("Intro");
        }
        else
        {
            if (player != null)
            {
                if (SceneManager.GetActiveScene().name == "SampleScene")
                {
                    if (player.GetComponent<Move>().tiredRequest)
                    {
                        SceneManager.LoadScene("SampleScene");
                    }
                }
                if (SceneManager.GetActiveScene().name == "Boss")
                {
                    if (player.GetComponent<Move>().tiredRequest)
                    {
                        SceneManager.LoadScene("Boss");
                    }
                }
            }
            else
            {
                GameObject.Find("MainMenu").SetActive(false);
            }

        }
    }
    public void ExitGame()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
