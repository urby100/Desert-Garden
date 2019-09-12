using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CactusPopUpScript : MonoBehaviour
{
    AudioSource audioSource;
    public bool mute;
    public AudioClip show;
    public AudioClip hide;

    bool showOnce;
    bool hideOnce;


    GameObject popEffect;
    bool effect = false;
    GameObject child;
    Vector3 targetUp;
    Vector3 targetDown;
    Vector3 velocity = Vector3.zero;
    public float moveSpeed = 0.08f;
    public bool direction = true;
    RectTransform rt;
    public GameObject scientists;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        popEffect = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LandEffect.prefab", typeof(GameObject));
        child =this.gameObject.transform.GetChild(0).gameObject;
        
        targetUp = child.transform.position + new Vector3(0f, child.GetComponent<SpriteRenderer>().bounds.extents.y * 2, 0f);
        targetDown = child.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position.x > scientists.transform.position.x)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (direction)
        {
            if (!showOnce && !mute) {
                audioSource.PlayOneShot(show);
                showOnce = true;
            }
            if (!effect)
            {
                var em = popEffect.GetComponent<ParticleSystem>().emission;
                em.rateOverTime = 250;
                var gm = popEffect.GetComponent<ParticleSystem>().main.gravityModifier;
                gm.constant = Random.Range(1.5f, 2);
                var sh = popEffect.GetComponent<ParticleSystem>().shape;
                sh.shapeType = ParticleSystemShapeType.Sphere;
                GameObject particle = Instantiate(popEffect,   new Vector3(gameObject.transform.position.x, 0, 0), gameObject.transform.rotation);
                particle.name = "PopEffectPopper";
                particle.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                Destroy(particle, 0.4f);
                effect = true;
            }
            child.transform.position = Vector3.SmoothDamp(child.transform.position,
                                                                targetUp,
                                                                ref velocity,
                                                                moveSpeed);
        }
        else
        {
            if (!hideOnce && !mute)
            {
                audioSource.PlayOneShot(hide);
                hideOnce = true;
            }
            effect = false;
            child.transform.position = Vector3.SmoothDamp(child.transform.position,
                                                                targetDown,
                                                                ref velocity,
                                                                moveSpeed);

        }
        if (!direction && child.transform.position == targetDown) {
            Destroy(gameObject,hide.length);
        }
    }
}
