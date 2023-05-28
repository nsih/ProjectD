using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnQuitController : MonoBehaviour
{
    public void OnClick()
    {
        Application.Quit();
        Debug.Log("GameQuit");
    }
}
