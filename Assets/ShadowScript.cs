using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    GameObject shadow;
    SpriteRenderer sprRndShadow;
    float height;

    // Start is called before the first frame update
    void Start()
    {
        shadow = new GameObject();
        shadow.name = "ShadowSprite";
        shadow.transform.parent = transform;
        sprRndShadow = shadow.AddComponent<SpriteRenderer>();
        /*
        Material m = (Material)AssetDatabase.LoadAssetAtPath("Assets/Shaders/ShadowMaterial.mat", typeof(Material));
        sprRndShadow.material = m;
        */
        sprRndShadow.color = new Color(0, 0, 0, 0.5f);
        sprRndShadow.flipY = true;
        sprRndShadow.sortingLayerName = "Foreground";
        sprRndShadow.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        sprRndShadow.sortingOrder = 99;
        sprRndShadow.transform.localScale = new Vector3(1, 1, 1);
    }
    // Update is called once per frame
    void LateUpdate()
    {

        height = GetComponent<Renderer>().bounds.size.y / 2;
        shadow.transform.position = new Vector3(gameObject.transform.position.x,
            0.01f - height, 0);
        shadow.transform.position += new Vector3(0,
            (height- gameObject.transform.position.y),
            0);
        shadow.transform.rotation = gameObject.transform.rotation;
    }
    void FixedUpdate() {

        sprRndShadow.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
