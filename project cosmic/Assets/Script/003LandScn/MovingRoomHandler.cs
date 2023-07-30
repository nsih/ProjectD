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



    //////////
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!GameManager.isLoading)
        {
            if(GameManager.isActionPhase && isConnect && GameManager.mentality != 0)
            {
                StartCoroutine("MovingRoom");
            }
        }
    }


    //사실상 이동함수
    private IEnumerator MovingRoom()
    {        
        gameManager.GetComponent<GameManager>().StartLoading();

        yield return new WaitForSeconds(1.0f);

        gameManager.GetComponent<GameManager>().EndLoading();

        GameManager.currentRoom = thisKey;  //current room Update
        if(StageManager.map[thisKey].isRevealed == false)
        {
            StageManager.map[thisKey].isRevealed = true;    //is Revealed Update
        }

        //이벤트 시작
        gameManager.GetComponent<GameManager>().OpenNewRoom();


        //Start current StartRoomEventPhase
        gameManager.GetComponent<StageManager>().StartRoomEventPhase( StageManager.map[ thisKey ].roomType );

        LandCanvus.GetComponent<LandUICon>().CloseMiniMap();

        LandCanvus.GetComponent<LandUICon>().StartShowRoomIntroPanel();

        LandCanvus.GetComponent<LandUICon>().UpdateRoomTypeUI();

        yield return null;
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
