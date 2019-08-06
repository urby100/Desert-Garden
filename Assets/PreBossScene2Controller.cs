using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PreBossScene2Controller : MonoBehaviour
{
    public List<ParticleSystem> effects;
    public ParticleSystem LittleStevies;
    public GameObject LittleStevePrefab;
    public Transform TopMax;
    public Transform BottomMax;
    public Transform GoalPoint;
    public TextMeshPro textBox;

    public Transform PlayerStartPoint;
    public Transform PlayerStopPoint;
    public Transform PlayerEndPoint;
    public GameObject player;
    Rigidbody2D rbPlayer;
    float movementSpeed = 3f;

    float sizeMin = 0.3f;
    float sizeMax = 0.7f;
    float size;
    float spawnDelayMin = 2f;
    float spawnDelayMax = 4f;
    float spawnDelay;
    float spawnTime;
    GameObject lastSpawnedStevie;
    SpriteRenderer sr;

    int sceneNumber = 1;

    float typingSpeed = 0.1f;
    float typingTime;
    int iterator = 0;
    float newsceneTime;
    float newsceneDelay = 1.5f;
    string alpha = "<alpha=#00>";
    string dialog = "Aha...   Great...";
    // Start is called before the first frame update
    void Start()
    {
        textBox.gameObject.SetActive(false);
        player.transform.position = PlayerStartPoint.position;
        rbPlayer = player.GetComponent<Rigidbody2D>();
        sr = LittleStevePrefab.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime)
        {
            size = Random.Range(sizeMin, sizeMax);
            LittleStevePrefab.transform.localScale = new Vector3(size, size, 0);
            float spriteSize = sr.bounds.center.y * 3.5f;
            LittleStevePrefab.GetComponent<LittleStevePreBossScene2>().to = new Vector3(GoalPoint.position.x, BottomMax.position.y + (spriteSize / 2), 0);
            Vector3 spawnPoint = new Vector3(BottomMax.position.x, BottomMax.position.y + (spriteSize / 2), 0);
            lastSpawnedStevie = Instantiate(LittleStevePrefab, spawnPoint, new Quaternion());
            spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
            spawnTime = Time.time + spawnDelay;
            playEffects();
        }
        switch (sceneNumber)
        {
            case 1:

                if (player.transform.position.x < PlayerStopPoint.position.x)
                {
                    rbPlayer.velocity = new Vector2(movementSpeed, rbPlayer.velocity.y);
                    player.GetComponent<Animator>().SetBool("walking", true);
                }
                else
                {
                    rbPlayer.velocity = Vector2.zero;
                    sceneNumber = 2;
                    typingTime = Time.time + typingSpeed;
                    textBox.text = alpha + dialog;
                    newsceneTime = Time.time + newsceneDelay;
                }
                break;
            case 2:
                player.GetComponent<Animator>().SetBool("standing", true);
                player.GetComponent<Animator>().SetBool("walking", false);
                if (Time.time < newsceneTime) {
                    break;
                }
                textBox.gameObject.SetActive(true);
                if (Time.time > typingTime)
                {
                    textBox.text = textBox.text.Replace(alpha, "");
                    textBox.text = textBox.text.Insert(iterator, alpha);
                    iterator++;
                    if (iterator > dialog.Length)
                    {
                        sceneNumber = 3;
                        newsceneTime = Time.time + newsceneDelay;
                    }
                    typingTime = Time.time + typingSpeed;
                }

                break;
            case 3:
                if (Time.time < newsceneTime)
                {
                    break;
                }
                textBox.gameObject.SetActive(false);
                if (player.transform.position.x < PlayerEndPoint.position.x)
                {
                    rbPlayer.velocity = new Vector2(movementSpeed, rbPlayer.velocity.y);
                    player.GetComponent<Animator>().SetBool("walking", true);
                    player.GetComponent<Animator>().SetBool("standing", false);
                }
                else
                {
                    rbPlayer.velocity = Vector2.zero;
                    sceneNumber = 4;
                    newsceneTime = Time.time + 1.5f*newsceneDelay;


                }
                break;
            case 4:
                if (Time.time < newsceneTime)
                {
                    break;
                }
                Debug.Log("Boss Fight");
                break;
            default:
                break;
                
        }

    }

    void playEffects()
    {
        //effects.ForEach(particle => { particle.Clear(); particle.Play(); });
        foreach (ParticleSystem particle in effects)
        {
            particle.Clear();
            particle.Play();
        }
    }
}
