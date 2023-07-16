using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovingRoomHandler : MonoBehaviour, IPointerClickHandler
{
    GameObject GameManagerObj;
    void Start ()
    {
        GameManagerObj = GameObject.Find("GameManager");
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.currentRoom = GetLastCharacterAsInt(this.gameObject);
        
        GameManagerObj.GetComponent<StageManager>().MoveRoom( GetLastCharacterAsInt(this.gameObject) );
        //Debug.Log(this.gameObject);
    }


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
}
