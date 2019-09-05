using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterBossSceneController : MonoBehaviour
{
    public GameObject abilitySpawnPoint;
    public GameObject nuke;
    public GameObject nukeGoal;
    float nukeSpeed = 4f;
    public GameObject scientist2;
    public List<GameObject> scientist2points;
    float moveSpeed = 2f;
    public GameObject player;
    public List<GameObject> playerpoints;
    float sceneNumber = 1;
    public TextMeshPro DialogText;

    bool turnOnce = false;



    string alpha = "<alpha=#00>";
    List<string> firstConvo = new List<string>() {
        "Hey, thanks for calming me down. Those chemical fumes really got me going crazy.",
        "No problem, I didn't have any plans for today anyway."
    };
    List<string> secondConvo = new List<string>() {
        "What was that ?",
        "Oh, that's the SUPER SECRET NUKE. It's kinda like the SUPER SECRET BOMB you guys dropped, but a 100 000 times more powerful."
    };
    List<string> thirdConvo = new List<string>() {
        "Guess I forgot to disable it. My bad... Do you have any plans for tomorrow ?"
    };
    float newLineDelay = 1f;
    float newLineTime;
    bool newLine = false;
    float typingSpeed = 0.05f;
    float typingTime;
    int iterator = 0;
    int iterator2 = 0;
    Vector2 move = Vector2.zero;
    float newsceneTime;
    float newsceneDelay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = camTransform.position;
        scientist2.transform.position = scientist2points[0].transform.position;
        player.transform.position = playerpoints[0].transform.position;
        turnObject(scientist2);
        turnObject(player);
        Scientist2standing();
        playerstanding();
        //
        DialogText.text = alpha + firstConvo[0];
        DialogText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (sceneNumber)
        {
            case 1://player in znanstvenik 2 se sprehodita iz stavbe, in se pogovarjata
                //neki neki hvala ko si me rešu, ... neki neki ni za kej ...
                //ko je konec pogovora in sta na sredini scene, scene number ++
                if (Time.time > typingTime)
                {
                    if (newLine && Time.time > newLineTime)
                    {
                        DialogText.text = alpha + firstConvo[iterator2];
                        newLine = false;
                    }
                    if (!newLine)
                    {
                        DialogText.text = DialogText.text.Replace(alpha, "");
                        DialogText.text = DialogText.text.Insert(iterator, alpha);
                        iterator++;
                        typingTime = Time.time + typingSpeed;
                    }
                    if (iterator > firstConvo[iterator2].Length)
                    {
                        iterator = 0;
                        iterator2++;
                        if (iterator2 == firstConvo.Count)
                        {
                            newsceneTime = Time.time + newsceneDelay;
                            sceneNumber = 2;
                        }
                        else
                        {//novi stavek
                            if (!newLine)
                            {
                                newLineTime = Time.time + newLineDelay;
                                newLine = true;
                            }

                        }
                    }
                }
                if (player.transform.position.x != playerpoints[1].transform.position.x)
                {

                    player.transform.position =
                        Vector3.MoveTowards(player.transform.position, playerpoints[1].transform.position, moveSpeed * Time.deltaTime);
                    playerwalking();
                }
                else
                {
                    playerstanding();
                    if (!turnOnce)
                    {
                        turnObject(player);
                        turnOnce = true;
                    }
                }
                if (scientist2.transform.position.x != scientist2points[1].transform.position.x)
                {

                    scientist2.transform.position =
                        Vector3.MoveTowards(scientist2.transform.position, scientist2points[1].transform.position, moveSpeed * Time.deltaTime);

                    // walking animation
                    Scientist2walking();
                    //
                }
                else
                {
                    Scientist2standing();
                }
                break;
            case 2:// iz stavbe prileti bomba in gre izven scene, oba se obračata proti raketi, scenenumber ++
                if (Time.time < newsceneTime)
                {
                    return;
                }
                DialogText.gameObject.SetActive(false);
                if (player.transform.position.x < nuke.transform.position.x)
                {
                    player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 0, player.transform.eulerAngles.z);
                }
                else
                {
                    player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 180, player.transform.eulerAngles.z);
                }
                if (scientist2.transform.position.x < nuke.transform.position.x)
                {
                    scientist2.transform.eulerAngles = new Vector3(scientist2.transform.eulerAngles.x, 0, scientist2.transform.eulerAngles.z);
                }
                else
                {
                    scientist2.transform.eulerAngles = new Vector3(scientist2.transform.eulerAngles.x, 180, scientist2.transform.eulerAngles.z);
                }
                float step = nukeSpeed * Time.deltaTime;
                nuke.transform.position = Vector2.MoveTowards(nuke.transform.position, nukeGoal.transform.position, step);

                if (nuke.transform.position.x == nukeGoal.transform.position.x)
                {
                    newsceneTime = Time.time + newsceneDelay;
                    sceneNumber = 3;
                    turnOnce = false;
                    DialogText.text = alpha + secondConvo[0];
                    iterator = 0;
                    iterator2 = 0;
                }
                break;
            case 3://glavni se obrne proti znanstveniku
                   //glavni ga vpraša kaj je blo to, on odgovori da je tk ko ona bomba na letalu sam da 100 000x močnejša
                   //pozabu jo je ugasnati ?
                   //scene number ++
                if (Time.time < newsceneTime)
                {
                    return;
                }
                DialogText.gameObject.SetActive(true);
                if (!turnOnce)
                {
                    turnObject(player);
                    turnOnce = true;

                }
                if (Time.time > typingTime)
                {
                    if (newLine && Time.time > newLineTime)
                    {
                        DialogText.text = alpha + secondConvo[iterator2];
                        newLine = false;
                    }
                    if (!newLine)
                    {
                        DialogText.text = DialogText.text.Replace(alpha, "");
                        DialogText.text = DialogText.text.Insert(iterator, alpha);
                        iterator++;
                        typingTime = Time.time + typingSpeed;
                    }
                    if (iterator > secondConvo[iterator2].Length)
                    {
                        iterator = 0;
                        iterator2++;
                        if (iterator2 == secondConvo.Count)
                        {
                            newsceneTime = Time.time + newsceneDelay;
                            sceneNumber = 4;
                            turnOnce = false;
                        }
                        else
                        {//novi stavek
                            if (!newLine)
                            {
                                newLineTime = Time.time + newLineDelay;
                                newLine = true;
                            }

                        }
                    }
                }
                break;
            case 4://bomba eksplodira, iz leve strani scene je zelo velika eksplozija, scenenumber ++
                if (Time.time < newsceneTime)
                {
                    return;
                }
                if (!turnOnce)
                {
                    turnObject(player);
                    turnOnce = true;
                    explosionTime = Time.time + explosionDelay;

                }
                DialogText.gameObject.SetActive(false);
                Explosion();
                if (Time.time > explosionTime)
                {
                    newsceneTime = Time.time + newsceneDelay;
                    sceneNumber = 5;
                    turnOnce = false;
                    DialogText.text = alpha + thirdConvo[0];
                    iterator = 0;
                    iterator2 = 0;
                }
                break;
            case 5://znanstvenik reče "my bad" scenenumber ++
                if (Time.time < newsceneTime)
                {
                    return;
                }
                if (!turnOnce)
                {
                    turnObject(player);
                    turnOnce = true;

                }
                DialogText.gameObject.SetActive(true);
                if (Time.time > typingTime)
                {
                    if (newLine && Time.time > newLineTime)
                    {
                        DialogText.text = alpha + thirdConvo[iterator2];
                        newLine = false;
                    }
                    if (!newLine)
                    {
                        DialogText.text = DialogText.text.Replace(alpha, "");
                        DialogText.text = DialogText.text.Insert(iterator, alpha);
                        iterator++;
                        typingTime = Time.time + typingSpeed;
                    }
                    if (iterator > thirdConvo[iterator2].Length)
                    {
                        iterator = 0;
                        iterator2++;
                        if (iterator2 == thirdConvo.Count)
                        {
                            newsceneTime = Time.time + newsceneDelay;
                            sceneNumber = 6;
                            turnOnce = false;
                        }
                        else
                        {//novi stavek
                            if (!newLine)
                            {
                                newLineTime = Time.time + newLineDelay;
                                newLine = true;
                                explosionTime = Time.time + explosionDelay + newLineDelay;
                            }

                        }
                    }
                }
                break;
            case 6://glavni znanstvenika zalije z vodo, konec igre
                if (Time.time < newsceneTime)
                {
                    useAbilityStart = Time.time + useAbilityLenght;
                    scientist2onhittime = Time.time + scientist2delayonhit;
                    return;
                }
                DialogText.gameObject.SetActive(false);
                if (Time.time > scientist2onhittime)
                {
                    if (Time.time > hurtTime && hurtOnce)
                    {

                        Scientist2standing();
                    }
                    else
                    {
                        Scientist2hurt();
                    }
                    if (!hurtOnce)
                    {
                        hurtOnce = true;
                        hurtTime = Time.time + hurtLasts;
                    }
                }
                else
                {

                    Scientist2standing();
                }

                if (Time.time <= useAbilityStart)
                {
                    GameObject projectile = Instantiate(projectilePrefab, abilitySpawnPoint.transform.position, new Quaternion());
                    projectile.name = "WaterProjectile";
                    projectile.GetComponent<WaterProjectile>().direction = 1;
                    playerwater();
                }
                else
                {
                    playerstanding();
                }

                break;
            default:
                break;
        }
    }

    float hurtLasts = 0.33f;
    float hurtTime;

    float scientist2delayonhit = 0.3f;
    float scientist2onhittime;
    bool hurtOnce = false;
    float useAbilityLenght = 0.2f;
    float useAbilityStart;

    public GameObject projectilePrefab;

    public Transform camTransform;

    float shakeDuration = 1.8f;
    float shakeAmount = 0.1f;
    float decreaseFactor = 1.0f;

    float explosionTime;
    float explosionDelay = 2f;

    Vector3 originalPos;
    void ShakeEffect()
    {
        scientist2.transform.position = Vector3.MoveTowards(scientist2.transform.position,
                                 scientist2points[0].transform.position, 0.2f * Time.deltaTime);
        player.transform.position = Vector3.MoveTowards(player.transform.position,
                                 playerpoints[0].transform.position, 0.2f * Time.deltaTime);

        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
    void Explosion()
    {
        ShakeEffect();
    }
    void turnObject(GameObject go)
    {
        go.transform.Rotate(0, 180, 0, Space.World);
    }

    void Scientist2walking()
    {
        scientist2.GetComponent<Animator>().SetBool("walking", true);
        scientist2.GetComponent<Animator>().SetBool("standing", false);
        scientist2.GetComponent<Animator>().SetBool("waving", false);
        scientist2.GetComponent<Animator>().SetBool("hurt", false);
    }
    void Scientist2standing()
    {
        scientist2.GetComponent<Animator>().SetBool("walking", false);
        scientist2.GetComponent<Animator>().SetBool("standing", true);
        scientist2.GetComponent<Animator>().SetBool("waving", false);
        scientist2.GetComponent<Animator>().SetBool("hurt", false);
    }
    void Scientist2waving()
    {
        scientist2.GetComponent<Animator>().SetBool("walking", false);
        scientist2.GetComponent<Animator>().SetBool("standing", false);
        scientist2.GetComponent<Animator>().SetBool("waving", true);
        scientist2.GetComponent<Animator>().SetBool("hurt", false);
    }
    void Scientist2hurt()
    {
        scientist2.GetComponent<Animator>().SetBool("walking", false);
        scientist2.GetComponent<Animator>().SetBool("standing", false);
        scientist2.GetComponent<Animator>().SetBool("waving", false);
        scientist2.GetComponent<Animator>().SetBool("hurt", true);
    }
    void playerwalking()
    {
        player.GetComponent<Animator>().SetBool("walking", true);
        player.GetComponent<Animator>().SetBool("standing", false);
        player.GetComponent<Animator>().SetBool("water", false);
    }
    void playerstanding()
    {
        player.GetComponent<Animator>().SetBool("walking", false);
        player.GetComponent<Animator>().SetBool("standing", true);
        player.GetComponent<Animator>().SetBool("water", false);
    }
    void playerwater()
    {
        player.GetComponent<Animator>().SetBool("walking", false);
        player.GetComponent<Animator>().SetBool("standing", false);
        player.GetComponent<Animator>().SetBool("water", true);
    }
}
