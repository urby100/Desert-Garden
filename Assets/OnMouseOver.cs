using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject spinningFlower1;
    public GameObject spinningFlower2;

    public void OnPointerEnter(PointerEventData eventData)
    {
        spinningFlower1.SetActive(true);
        spinningFlower2.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spinningFlower1.SetActive(false);
        spinningFlower2.SetActive(false);
    }
}
