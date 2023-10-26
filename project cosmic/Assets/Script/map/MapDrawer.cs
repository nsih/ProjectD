using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class MapDrawer : MonoBehaviour
{
    //public GameObject mapGenerator;

    public GameObject pnlBackGround;
    public GameObject map;
    public GameObject mapContent;

    public GameObject linePool;
    public Image line;
    public List<Image> lines;


    public Sprite roomTypeStart;
    public Sprite roomTypeBoss;
    public Sprite roomTypeBattle;
    public Sprite roomTypeElite;
    public Sprite roomTypeFixedEvent;
    public Sprite roomTypeRandomEvent;
    public Sprite roomTypeShop;
    public Sprite roomTypeAlter;

    

    /*
    public void Start()
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        map = pnlBackGround.transform.Find("StageMap").gameObject;
        mapContent = map.transform.Find("Viewport").gameObject.transform.Find("MapContent").gameObject;

        linePool = mapContent.transform.Find("LinePool").gameObject;
    }
    */

    private bool hasClicked = false;    //스테이지 넘어갈떄마다 False

    public void UpdateDrawMap()
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        map = pnlBackGround.transform.Find("StageMap").gameObject;
        mapContent = map.transform.Find("Viewport").gameObject.transform.Find("MapContent").gameObject;
        linePool = mapContent.transform.Find("LinePool").gameObject;


        if(!map.activeSelf)
        {
            if(!hasClicked)
            {
                MappingRoom();
                GenerateLinePool();

                hasClicked = true;
            }

            map.SetActive(true);
        }

        else
        {
            map.SetActive(false);
        }
    }


    //로직맵이랑 맵이랑 로고 연결하고 위치조정
    public void MappingRoom()
    {
        int maxX = mapGenerator.mapGraph.Nodes.Max(node => node.X);
        int maxY = mapGenerator.mapGraph.Nodes.Max(node => node.Y);
        

        //StartRoom Draw
        Image startRoomPin = mapContent.transform.GetChild(1).gameObject.GetComponent<Image>();
        Room<RoomType> startRoomNode = mapGenerator.mapGraph.Nodes.FirstOrDefault(node => node.X == 0 && node.Y == 0);

        if( startRoomNode != null )
        {
            //mapping
            startRoomNode.roomPin = startRoomPin;
            
            //sprite 변경
            startRoomPin.GetComponent<Image>().sprite = GetRoomImage(startRoomNode.RoomType);       
        }

        //middle
        for (int i = 1; i <= maxY ; i++)
        {
            for (int j = 0; j <= maxX; j++)
            {
                Image roomPin;
                if(i != maxY)
                {
                    mapContent.transform.GetChild(i+1).gameObject.transform.GetChild(j).gameObject.SetActive(true);

                    roomPin = mapContent.transform.GetChild(i+1).gameObject.transform.GetChild(j).gameObject.GetComponent<Image>();
                    //Debug.Log(map.transform.GetChild(i+1).gameObject);
                    Room<RoomType> roomNode = mapGenerator.mapGraph.Nodes.FirstOrDefault(node => node.X == j && node.Y == i);

                    if( roomNode != null )
                    {
                        //mapping
                        roomNode.roomPin = roomPin;                        
                        //map.transform.GetChild(i).gameObject.transform.GetChild(j).gameObject.SetActive(true);
                        
                        //sprite 변경
                        roomPin.GetComponent<Image>().sprite = GetRoomImage(roomNode.RoomType);
                        //Debug.Log(GetRoomImage(mapGenerator.mapGraph.Nodes.FirstOrDefault(node => node.X == j && node.Y == i).RoomType));


                        //위치 조정
                        UnityEngine.Vector3 roomPinPos = roomPin.transform.position;

                        float randomX = Random.Range(-20, 20);
                        float randomY = Random.Range(-10, 10);

                        roomPinPos.x += randomX;
                        roomPinPos.y += randomY;

                        roomPin.transform.position = roomPinPos;
                    }

                    else
                    {
                        roomPin.gameObject.SetActive(false);
                        //map.transform.GetChild(i).gameObject.transform.GetChild(j).gameObject.SetActive(false);
                    }
                }

                else
                {
                    mapContent.transform.GetChild(i+1).gameObject.SetActive(true);

                    roomPin = mapContent.transform.GetChild(i+1).gameObject.GetComponent<Image>();
                    Room<RoomType> roomNode = mapGenerator.mapGraph.Nodes.FirstOrDefault(node => node.X == j && node.Y == i);

                    //Debug.Log(roomPin);
                    //Debug.Log(j+","+i);

                    if( roomNode != null )
                    {
                        //mapping
                        roomNode.roomPin = roomPin;
                        
                        //sprite 변경
                        roomPin.GetComponent<Image>().sprite = GetRoomImage(roomNode.RoomType);


                        return;
                    }
                }

                

            }
        }

    }

    //클릭시 해당 이미지의 타입 반환
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

    //연결선 생성
    public void GenerateLinePool()
    {
        int maxX = mapGenerator.mapGraph.Nodes.Max(node => node.X);
        int maxY = mapGenerator.mapGraph.Nodes.Max(node => node.Y);

        //Debug.Log(maxY);
        //Debug.Log(mapGenerator.mapGraph.Nodes.FirstOrDefault(node => node.X == 0 && node.Y == maxY+1)  );    ////이거 boss로 나오면 위에 매핑

        foreach (Transform child in linePool.transform)
        {
            // 하위 오브젝트를 삭제
            Destroy(child.gameObject);
        }

        for(int i = 0 ; i <= maxX ; i++)
        {
            Room<RoomType> startNode = mapGenerator.mapGraph.Nodes.FirstOrDefault(node => node.X == i && node.Y == 0);

            if( startNode != null )
            {
                foreach (var room in startNode.Neighbors)
                {
                    Vector3 startPos = startNode.roomPin.transform.position;
                    Vector3 endPos = room.roomPin.transform.position;           //

                    Vector3 middlePos = (startPos + endPos) / 2.0f;
                    GameObject newObj = Instantiate(line.gameObject, middlePos, Quaternion.identity);

                    Vector3 direction = endPos - startPos;
                    Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
                    newObj.transform.rotation = rotation;

                    float distance = Vector3.Distance(startPos, endPos);
                    newObj.transform.localScale = new Vector3(7, distance, newObj.transform.localScale.z);
                    

                    newObj.transform.SetParent(linePool.transform);
                }
            }
        }

        for (int i = 1; i <= maxY ; i++)            //<=
        {
            for (int j = 0; j <= maxX; j++)
            {
                Room<RoomType> startNode = mapGenerator.mapGraph.Nodes.FirstOrDefault(node => node.X == j && node.Y == i);
                
                if( startNode != null )
                {
                    foreach (var room in startNode.Neighbors)
                    {
                        Vector3 startPos = startNode.roomPin.transform.position;
                        Vector3 endPos = room.roomPin.transform.position;           //

                        Vector3 middlePos = (startPos + endPos) / 2.0f;
                        GameObject newObj = Instantiate(line.gameObject, middlePos, Quaternion.identity);

                        Vector3 direction = endPos - startPos;
                        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
                        newObj.transform.rotation = rotation;

                        float distance = Vector3.Distance(startPos, endPos);
                        newObj.transform.localScale = new Vector3(7, distance, newObj.transform.localScale.z);
                        

                        newObj.transform.SetParent(linePool.transform);
                    }
                }
            }
        }
    }
}
