using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class MapDrawer : MonoBehaviour
{
    //public GameObject mapGenerator;

    public GameObject map;


    public Sprite roomTypeStart;
    public Sprite roomTypeBoss;
    public Sprite roomTypeBattle;
    public Sprite roomTypeElite;
    public Sprite roomTypeFixedEvent;
    public Sprite roomTypeRandomEvent;
    public Sprite roomTypeShop;
    public Sprite roomTypeAlter;

    public void Start()
    {
        map = GameObject.Find("MapContent");
    }

    public void Update()
    {
         if (Input.GetMouseButtonDown(0))
         {
            MappingRoom();
         }
    }


    public void MappingRoom()
    {
        int maxX = mapGenerator.mapGraph.Nodes.Max(node => node.X);
        int maxY = mapGenerator.mapGraph.Nodes.Max(node => node.Y);


        for (int i = 1; i <= maxY ; i++)
        {
            for (int j = 0; j < maxX; j++)
            {
                GameObject roomPin = map.transform.GetChild(i).gameObject.transform.GetChild(j).gameObject;
                Room<RoomType> roomNode = mapGenerator.mapGraph.Nodes.FirstOrDefault(node => node.X == j && node.Y == i);

                if( roomNode != null )
                {
                    //mapping
                    roomNode.roomPin = roomPin;
                    
                    roomPin.SetActive(true);
                    
                    //sprite 변경
                    roomPin.GetComponent<Image>().sprite = GetRoomImage(roomNode.RoomType);

                    //Debug.Log(GetRoomImage(mapGenerator.mapGraph.Nodes.FirstOrDefault(node => node.X == j && node.Y == i).RoomType));


                    //위치 조정
                    Vector3 roomPinPos = roomPin.transform.position;

                    float randomX = Random.Range(-50, 50);
                    float randomY = Random.Range(-20, 20);

                    roomPinPos.x += randomX;
                    roomPinPos.y += randomY;

                    roomPin.transform.position = roomPinPos;


                }

                else
                {
                    map.transform.GetChild(i).gameObject.transform.GetChild(j).gameObject.SetActive(false);
                }

            }
        }
    }

    public Sprite GetRoomImage(RoomType roomType)
    {
        if(roomType == RoomType.Start)
            return roomTypeStart;

        else if(roomType == RoomType.Boss)
            return roomTypeBoss;

        else if(roomType == RoomType.Battle)
            return roomTypeBattle;

        else if(roomType == RoomType.EliteBattle)
            return roomTypeElite;

        else if(roomType == RoomType.FixedEvent)
            return roomTypeFixedEvent;

        else if(roomType == RoomType.RandomEvent)
            return roomTypeRandomEvent;

        else if(roomType == RoomType.Alter)
            return roomTypeAlter;

        else if(roomType == RoomType.Shop)
            return roomTypeShop;

        else
            Debug.Log("wtf");


        return roomTypeStart;
    }

    

    public void DrawPath()
    {

    }

}
