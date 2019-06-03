using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    public GameObject planeObject;
    public List<GameObject> planepoints = new List<GameObject>();
    float planeChangeDirectionTime;
    float planeChangeDirectionDelay = 5f;
    float planeSpeed = 2f;
    float addedHeight = 2.3f;
    bool changeDirection = false;
    int pointsIterator;
    int direction = 1;
    int numberOfPoints;
    Vector3 addedHeightVector;

    //cactus random spawn
    public List<GameObject> cactusSpawnPoints = new List<GameObject>();
    public List<GameObject> cactusList = new List<GameObject>();
    float cactusDontShowTime;
    float cactusShowAliveTime;
    float cactusDontShow=1f;
    float cactusShowAlive = 1f;
    bool cactusShow=false;
    int cactusIndex;
    GameObject cactus;

    // Start is called before the first frame update
    void Start()
    {
        pointsIterator = 0;
        numberOfPoints = planepoints.Count - 1;
        addedHeightVector = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!cactusShow && Time.time> cactusDontShowTime) {
            cactusShow = true;
            cactusIndex = Random.Range(0, cactusList.Count - 1);
            cactus = Instantiate(cactusList[cactusIndex], new Vector3(Random.Range(cactusSpawnPoints[0].transform.position.x, 
                                                                                cactusSpawnPoints[1].transform.position.x),
                                                                                cactusSpawnPoints[0].transform.position.y, 1), 
                                                                                Quaternion.identity);
            cactusShowAliveTime = Time.time + cactusShowAlive;
        }
        if (cactusShow && Time.time > cactusShowAliveTime )
        {
            cactusShow = false;
            cactus.GetComponent<CactusPopUpScript>().direction = false;
            cactusDontShowTime = Time.time + cactusDontShow;
        }
        if (cactus != null) {
            //obrni proti scientistoma
        }
        //dodaj scientista, ki se sprehodita do kaktusa in mu malo pomahata
        if (direction == 1)
        {
            planeObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (direction == -1)
        {
            planeObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (Time.time > planeChangeDirectionTime && changeDirection) {
            changeDirection = false;
        }
        if (Vector2.Distance(planeObject.transform.position, planepoints[pointsIterator].transform.position + addedHeightVector) > 0.5f)
        {
            if (!changeDirection) {
                planeObject.transform.position = Vector2.MoveTowards(planeObject.transform.position, planepoints[pointsIterator].transform.position+ addedHeightVector, planeSpeed * Time.deltaTime);

            }
        }
        else
        {
            if (!changeDirection) {
                planeObject.transform.position = planepoints[pointsIterator].transform.position;
                pointsIterator = pointsIterator + direction;
                if (pointsIterator >= numberOfPoints)
                {
                    direction = -1;
                    pointsIterator = numberOfPoints;
                }
                else if (pointsIterator <= 0)
                {
                    direction = 1;
                    pointsIterator = 0;
                }
                changeDirection = true;
                planeChangeDirectionTime = Time.time + planeChangeDirectionDelay;
                addedHeightVector = new Vector3(0, Random.Range(0, addedHeight), 0);
                planeObject.transform.position = planeObject.transform.position+addedHeightVector;
            }
        }
    }
}
