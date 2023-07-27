using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovingRoomHandler : MonoBehaviour, IPointerClickHandler
{
    GameObject gameManager;
    GameObject LandCanvus;

    int thisKey;
    bool isConnect; //current Room이랑 붙어 있는가?


    void Awake ()
    {
        gameManager = GameObject.Find("GameManager");
        LandCanvus = GameObject.Find("LandUICanvas");
        thisKey = GetLastCharacterAsInt(this.gameObject);
    }

    void OnEnable() 
    {
        CheckConnect();
        ShowRoomConnect();
    }



    private IEnumerator MovingRoom()//사실상 이동함수
    {
        gameManager.GetComponent<GameManager>().ShowLoadingScreen();

        yield return new WaitForSeconds(1.0f);

        GameManager.currentRoom = thisKey;  //current room Update

        if(StageManager.map[thisKey].isRevealed == false)
        {
            StageManager.map[thisKey].isRevealed = true;    //is Revealed Update
        }

        gameManager.GetComponent<GameManager>().OpenNewRoom();


        //Start current StartRoomEventPhase
        gameManager.GetComponent<StageManager>().StartRoomEventPhase( StageManager.map[ thisKey ].roomType );

        //move-> closeTap
        LandCanvus.GetComponent<LandUICon>().CloseMiniMap();
    }


    public void OnPointerClick(PointerEventData eventData)
    {        
        if(GameManager.isActionPhase && isConnect && GameManager.doomCount != 0 && !GameManager.isLoading)
        {
            gameManager.GetComponent<GameManager>().ShowLoadingScreen();

            GameManager.currentRoom = thisKey;  //current room Update

            if(StageManager.map[thisKey].isRevealed == false)
            {
                StageManager.map[thisKey].isRevealed = true;    //is Revealed Update
            }

            gameManager.GetComponent<GameManager>().OpenNewRoom();


            //Start current StartRoomEventPhase
            gameManager.GetComponent<StageManager>().StartRoomEventPhase( StageManager.map[ thisKey ].roomType );

            //move-> closeTap
            LandCanvus.GetComponent<LandUICon>().CloseMiniMap();
        }
    }




    //room key
    public int GetLastCharacterAsInt(GameObject obj)
    {
        string objectName = obj.name;
        if (!string.IsNullOrEmpty(objectName))
        {
            char lastCharacter = objectName[objectName.Length - 1];
            string lastCharacterString = lastCharacter.ToString();
            if (int.TryParse(lastCharacterString, out int result))
            {
                return result;
            }
        }

        return 0; // 기본값 반환 또는 오류 처리
    }

    //인접 키 리스트 받아서 이 오브젝트가 해당 리스트안의 키중에서 해당 사항 있는오브젝트인지 판별
    void CheckConnect()
    {
        isConnect = gameManager.GetComponent<StageManager>().FindAttachedKey(GameManager.currentRoom).Contains(thisKey);
    }

    //visualization
    void ShowRoomConnect()
    {
        if(thisKey == GameManager.currentRoom)
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
        }

        else
        {
            if(isConnect)
            {
                this.gameObject.GetComponent<Image>().color = Color.grey;
            }
            else if(!isConnect)
            {
                this.gameObject.GetComponent<Image>().color = Color.black;
            }
        }
    }
}
