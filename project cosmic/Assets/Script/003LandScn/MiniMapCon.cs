using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapCon : MonoBehaviour
{
    public RectTransform miniMapViewPort;
    public RectTransform miniMapContent;

    public Image currentRoom;



    void Start()
    {
        miniMapViewPort = GameObject.Find("MiniMapViewport").GetComponent<RectTransform>();
        miniMapContent = GameObject.Find("MiniMapContent").GetComponent<RectTransform>();

        GetCurrentRoom();
    }


    /*
    private Image FindCurrentRoom()
    {
        GameObject currentRoomObject = GameObject.FindGameObjectWithTag("CurrentRoom");

        if (currentRoomObject != null)
        {
            Image currentRoomImage = currentRoomObject.GetComponent<Image>();
            return currentRoomImage;
        }

        return null;
    }
    */
    
    private void GetCurrentRoom()   //content를 0,0으로 초기화 한다음 current room을 viewport 중앙으로 위치시키도록 content 이동
    {
        miniMapContent.anchoredPosition = Vector2.zero;

        currentRoom = GameObject.Find("Room2").GetComponent<Image>();
        //currentRoom = FindCurrentRoom(); <-

        Vector2 viewportCenter = miniMapViewPort.rect.center;
        Vector2 roomPosition = miniMapViewPort.InverseTransformPoint(currentRoom.rectTransform.position);
        Vector2 contentOffset = viewportCenter - roomPosition;

        miniMapContent.anchoredPosition = contentOffset;
    }
}