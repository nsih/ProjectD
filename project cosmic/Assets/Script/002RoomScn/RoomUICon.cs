using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RoomUICon : MonoBehaviour
{
    GameObject gameManager;
    GameObject pnlBackGround;
    GameObject phaseType;



    void Start()
    {
        gameManager = GameObject.Find("Gameamanager");
        pnlBackGround = GameObject.Find("PnlBackGround");
        phaseType = GameObject.Find("PhaseType");
    }
}
