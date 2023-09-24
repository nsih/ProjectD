using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class MovingRoomHandler : MonoBehaviour, IPointerClickHandler
{
    GameObject gameManager;
    GameObject LandCanvus;

    private Room<RoomType> thisNode;


    void Awake ()
    {
        gameManager = GameObject.Find("GameManager");
        LandCanvus = GameObject.Find("LandUICanvas");
    }

    void OnEnable() 
    {
        thisNode = mapGenerator.mapGraph.Nodes.FirstOrDefault(node => node.roomPin == this.gameObject.GetComponent<Image>());

        ShowRoomPin();
    }



    //////////
    ////*
    public void OnPointerClick(PointerEventData eventData)
    {
        if( CheckRoomConnect() && GameManager.isEventEnd)
        {
            //StartCoroutine("MovingRoom");
        }
    }


    //사실상 이동함수
    private IEnumerator MovingRoom()
    {
        GameManager.playerLocationX = thisNode.X;
        GameManager.playerLocationY = thisNode.Y;


        //이벤트 시작
        gameManager.GetComponent<GameManager>().OpenNewRoom();
        LandCanvus.GetComponent<LandUICon>().CloseStageMap();

        yield return null;
    }


    //여기가 현재 위치인가
    bool CheckRoomCurrent()
    {
        if(thisNode == mapGenerator.mapGraph.Nodes.FirstOrDefault(node => node.X == GameManager.playerLocationX && node.Y == GameManager.playerLocationY))
        {
            return true;
        }

        else
        {
            return false;
        }
    }


    //여기가 현재위치와 연결된 방인가
    bool CheckRoomConnect()
    {
        return mapGenerator.mapGraph.Nodes.FirstOrDefault(node => node.X == GameManager.playerLocationX && node.Y == GameManager.playerLocationY).
        Neighbors.Contains(thisNode);
    }

    //visualization
    void ShowRoomPin()
    {
        if(CheckRoomCurrent())
        {
            this.gameObject.GetComponent<Image>().color = Color.black;
        }

        else
        {
            if(CheckRoomConnect())
            {
                this.gameObject.GetComponent<Image>().color = Color.grey;
            }
            else
            {
                this.gameObject.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
