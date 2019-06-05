using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybindings : MonoBehaviour
{
    public KeyCode forward, backward, jump, 
                    crouch, attack1, attack2;
    void Awake() {
        //dobi shranjeno spremenljivko ali pa default 
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forward", "D"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backward", "A"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jump", "W"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crouch", "S"));
        attack1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attack1", "Space"));
        attack2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attack2", "V"));
    }
    public void Reset()
    {
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forward", "D"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backward", "A"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jump", "W"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crouch", "S"));
        attack1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attack1", "Space"));
        attack2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attack2", "V"));
    }
}
