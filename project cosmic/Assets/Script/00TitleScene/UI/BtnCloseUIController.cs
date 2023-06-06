using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCloseUIController : MonoBehaviour
{
    public void OnClick()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
