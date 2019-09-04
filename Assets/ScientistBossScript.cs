using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistBossScript : MonoBehaviour
{
    public bool throwAnimation = false;
    float animLasts = 0.125f;
    float animTime;
    bool setTime = false;
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
}
