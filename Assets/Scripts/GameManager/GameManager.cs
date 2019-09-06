using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject DeadMenu;
    public GameObject menutiptext;
    public GameObject player;
    public GameObject waterBottleUI;
    public GameObject specialAttackUI;
    public Keybindings k;
    public Sprite throwerIcon;
    public Sprite stevieIcon;
    public Sprite crusherIcon;
    public Sprite stopperIcon;
    int abilityCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetSpecialAttackIcons();
    }
    public void SetSpecialAttackIcons() {

        GameObject g= specialAttackUI.transform.GetChild(1).gameObject;
        foreach (Transform child in g.transform) {
            Destroy(child.gameObject);
        }
        float offset = 0f;
        foreach (GameObject attack in player.GetComponent<UseAbility>().projectileList) {
            GameObject icon = new GameObject(attack.name+"Icon");
            Image newImage = icon.AddComponent<Image>();
            if (attack.name == "ThrowerProjectile") {
                newImage.sprite = throwerIcon;
            }
            if (attack.name == "CrusherProjectile")
            {
                newImage.sprite = crusherIcon;
            }
            if (attack.name == "StopperProjectile")
            {
                newImage.sprite = stopperIcon;
            }
            if (attack.name == "LittleSteve")
            {
                newImage.sprite = stevieIcon;
            }
            newImage.GetComponent<RectTransform>().SetParent(g.transform);
            newImage.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 0.6f);
            newImage.GetComponent<RectTransform>().localPosition = new Vector3(0+offset,0,0);
            offset += 60;
            icon.SetActive(true);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<UseAbility>().projectileList.Count!= abilityCount) {
            abilityCount = player.GetComponent<UseAbility>().projectileList.Count;
            Invoke("SetSpecialAttackIcons", 0.2f);
        }
        if (player.GetComponent<UseAbility>().canUseWater)
        {
            waterBottleUI.SetActive(true);
            waterBottleUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = k.attack1.ToString()+" - Attack 1" ;
        }
        else
        {
            waterBottleUI.SetActive(false);
        }
        if (player.GetComponent<UseAbility>().canUseAbility)
        {
            specialAttackUI.SetActive(true);
            specialAttackUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = k.attack2.ToString() + " - Attack 2";
        }
        else
        {
            specialAttackUI.SetActive(false);
        }
        if (Input.GetKeyDown("escape") && !player.GetComponent<Move>().tiredRequest)
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
        if (!MainMenu.activeInHierarchy )
        {
            menutiptext.SetActive(true);
        }
        if (OptionsMenu.activeInHierarchy)
        {
            menutiptext.SetActive(false);
        }
        if (player.GetComponent<Move>().tiredRequest)
        {
            DeadMenu.SetActive(true);
            MainMenu.SetActive(false);
            OptionsMenu.SetActive(false);
        }
        else {
            DeadMenu.SetActive(false);
        }
    }
    
}
