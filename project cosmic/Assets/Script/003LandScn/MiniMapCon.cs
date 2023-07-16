using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniMapCon : MonoBehaviour
{
    public RectTransform miniMapViewPort;
    public RectTransform miniMapContent;
    

    public Image currentRoom;

    public Sprite[] roomTypeSprite = new Sprite[6];

    void Awake()
    {
        miniMapViewPort = GameObject.Find("MiniMapViewport").GetComponent<RectTransform>();
        miniMapContent = GameObject.Find("MiniMapContent").GetComponent<RectTransform>();

        SetCurrentRoomLocation();

        FindRevealedRoom();
    }

    void OnEnable() 
    {
        SetCurrentRoomLocation();

        FindRevealedRoom();
    }


    private Image FindCurrentRoom()
    {
        GameObject currentRoomObject = GameObject.Find("S"+GameManager.currentStage+"R"+GameManager.currentRoom);
        //Debug.Log("S"+GameManager.currentStage+"R"+GameManager.currentRoom);
        
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
                    tempRoomObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                }
                else if(node.roomType == RoomType.Altar)
                {
                    tempRoomObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = roomTypeSprite[0];
                }
                else if(node.roomType == RoomType.Battle)
                {
                    tempRoomObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = roomTypeSprite[1];
                }
                else if(node.roomType == RoomType.Boss)
                {
                    tempRoomObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = roomTypeSprite[2];
                }
                else if(node.roomType == RoomType.Event)
                {
                    tempRoomObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = roomTypeSprite[3];
                }
                else if(node.roomType == RoomType.Shop)
                {
                    tempRoomObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = roomTypeSprite[4];
                }
                else if(node.roomType == RoomType.Test)
                {
                    tempRoomObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = roomTypeSprite[5];
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