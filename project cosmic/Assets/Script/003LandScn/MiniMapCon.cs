using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniMapCon : MonoBehaviour
{
    public RectTransform miniMapViewPort;
    public RectTransform miniMapContent;

    public Color whiteRoom = new Color(0f,0f,0f,0f);
    public Color grayRoom = new Color(0f,0f,0f,0f);
    public Color purpleRoom = new Color(0f,0f,0f,0f);

    public Image currentRoom;

    void Start()
    {
        miniMapViewPort = GameObject.Find("MiniMapViewport").GetComponent<RectTransform>();
        miniMapContent = GameObject.Find("MiniMapContent").GetComponent<RectTransform>();

        SetCurrentRoomLocation();
    }


    private Image FindCurrentRoom()
    {
        GameObject currentRoomObject = GameObject.Find("S"+GameManager.currentStage+"R"+GameManager.currentRoom);
        Debug.Log("S"+GameManager.currentStage+"R"+GameManager.currentRoom);
        
        if (currentRoomObject != null)
        {
            Image currentRoomImage = currentRoomObject.GetComponent<Image>();
            return currentRoomImage;
        }

        return null;
    }
    void SetCurrentRoomLocation()   //content를 0,0으로 초기화 한다음 current room을 viewport 중앙으로 위치시키도록 content 이동
    { 
        currentRoom = FindCurrentRoom();

        miniMapContent.anchoredPosition = Vector2.zero;

        Vector2 viewportCenter = miniMapViewPort.rect.center;
        Vector2 roomPosition = miniMapViewPort.InverseTransformPoint(currentRoom.rectTransform.position);
        Vector2 contentOffset = viewportCenter - roomPosition;

        miniMapContent.anchoredPosition = contentOffset;
    }

    void FindRevealedRoom()//탐색된 방 미니맵에서 타입 보여주기
    {
        foreach (var kvp in StageManager.map)
        {
            RoomData node = kvp.Value;
            int key = kvp.Key;

            GameObject tempRoomObject = GameObject.Find("S"+GameManager.currentStage+"R"+key);

            if(node.isRevealed)
            {
                if(node.roomType == RoomType.Null)
                {

                }
                else if(node.roomType == RoomType.Altar)
                {

                }
                else if(node.roomType == RoomType.Battle)
                {

                }
                else if(node.roomType == RoomType.Shop)
                {

                }
                else if(node.roomType == RoomType.Test)
                {

                }
                else if(node.roomType == RoomType.Event)
                {

                }
                else if(node.roomType == RoomType.Boss)
                {

                }
            }
        }
    }

    private Image FindConnectedRoom()
    {
        foreach (var kvp in StageManager.map)
        {
            RoomData node = kvp.Value;
            int key = kvp.Key;
        }

        return null;
    }
}