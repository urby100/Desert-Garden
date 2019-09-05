using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerObject;
    public float ypos = 2.5f;

    float smooth = 7f;
    float minXPosition = 2.5f;
    float maxXPosition = 1000f;
    float dynamicPosXmin;
    float dynamicPosXmax;
    float xPosition;
    Scene m_Scene;
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        dynamicPosXmin = minXPosition;
        dynamicPosXmax = maxXPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sceneName == "PreBossScene1" || sceneName=="AfterBossScene")
        {

            return;
        }
        else if (sceneName == "Boss") {
            return;
        }
        xPosition = Mathf.Clamp(playerObject.transform.position.x, dynamicPosXmin, dynamicPosXmax);
        transform.position = Vector3.Lerp(transform.position,
                                    new Vector3(xPosition, ypos, transform.position.z),
                                    smooth * Time.deltaTime);
    }
}
