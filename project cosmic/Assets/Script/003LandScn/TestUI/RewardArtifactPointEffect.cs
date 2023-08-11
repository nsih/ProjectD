using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class RewardArtifactPointEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color normalColor;
    private Color hoverColor;


    void Start()
    {
        normalColor = new Color(1f, 1f, 1f, 1f);
        hoverColor = new Color(1f, 1f, 1f, 0f);
    }

    public void Onclick()
    {
        Debug.Log("asd");
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = normalColor;
        Debug.Log("asd");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = hoverColor;
    }
}
