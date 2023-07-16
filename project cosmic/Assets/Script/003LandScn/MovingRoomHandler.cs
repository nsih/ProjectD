using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovingRoomHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        
        Debug.Log(this.gameObject);
    }
}
