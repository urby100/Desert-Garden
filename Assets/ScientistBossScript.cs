using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScientistBossScript : MonoBehaviour
{
    public GameObject potionSpawner;
    public GameObject effects;
    public bool throwAnimation = false;
    float animLasts = 0.125f;
    float animTime;
    bool setTime = false;
    bool tired = false;

    bool setEnd = false;
    float endDelay = 3f;
    float endTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float getAnimLasts() {
        return animLasts;
    }
    // Update is called once per frame
    void Update()
    {
        if (setEnd && Time.time > endTime) {
            SceneManager.LoadScene("AfterBossScene");
        }
        if (tired) {
            Scientist2tired();
            if (!setEnd) {
                endTime = Time.time + endDelay;
                setEnd = true;
            }
            return;
        }
        if (throwAnimation)
        {
            if (!setTime) {
                setTime = true;
                animTime = Time.time + animLasts;
            }
            Scientist2throwing();
            if (Time.time > animTime)
            {
                throwAnimation = false;
                setTime = false;
            }
        }
        else {
            Scientist2standing();
        }
    }
    void Scientist2walking()
    {
        GetComponent<Animator>().SetBool("walking", true);
        GetComponent<Animator>().SetBool("standing", false);
        GetComponent<Animator>().SetBool("waving", false);
        GetComponent<Animator>().SetBool("throwing", false);
    }
    void Scientist2standing()
    {
        GetComponent<Animator>().SetBool("walking", false);
        GetComponent<Animator>().SetBool("standing", true);
        GetComponent<Animator>().SetBool("waving", false);
        GetComponent<Animator>().SetBool("throwing", false);
    }
    void Scientist2waving()
    {
        GetComponent<Animator>().SetBool("walking", false);
        GetComponent<Animator>().SetBool("standing", false);
        GetComponent<Animator>().SetBool("waving", true);
        GetComponent<Animator>().SetBool("throwing", false);
    }
    void Scientist2throwing()
    {
        GetComponent<Animator>().SetBool("walking", false);
        GetComponent<Animator>().SetBool("standing", false);
        GetComponent<Animator>().SetBool("waving", false);
        GetComponent<Animator>().SetBool("throwing", true);
    }
    void Scientist2tired()
    {
        GetComponent<Animator>().SetBool("walking", false);
        GetComponent<Animator>().SetBool("standing", false);
        GetComponent<Animator>().SetBool("waving", false);
        GetComponent<Animator>().SetBool("throwing", false);
        GetComponent<Animator>().SetBool("tired", true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "WaterProjectile") {
            potionSpawner.SetActive(false);
            effects.SetActive(false);
            tired = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
