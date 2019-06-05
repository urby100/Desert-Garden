using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject menutiptext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!MainMenu.activeInHierarchy)
            {
                if (!OptionsMenu.activeInHierarchy)
                {
                    MainMenu.SetActive(true);
                    menutiptext.SetActive(false);
                }
            }
        }
        if (!MainMenu.activeInHierarchy)
        {
            menutiptext.SetActive(true);
        }
    }
    
}
