using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICon : MonoBehaviour
{
    GameObject gameManager;
    GameObject Canvas;


    //status UI
    public GameObject[] maxHPUI = new GameObject[15];
    public GameObject[] currentHPUI = new GameObject[15];


    GameObject bulletUI;
    

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        Canvas = GameObject.Find("Canvas");

        //HP
        for (int i = 0; i < 15; i++)
        {
            maxHPUI[i] = GameObject.Find("MaxHP").transform.GetChild(i).gameObject;
            currentHPUI[i] = GameObject.Find("CurrentHP").transform.GetChild(i).gameObject;
        }

        //Bullet
        bulletUI = GameObject.Find("APText");
    }

    void Update()
    {

    }


    #region "update UI data"
    public void UpdateHPUI()
    {
        int currentHP = PlayerInfo.currentHP;
        int maxHP = PlayerInfo.maxHp;


        for (int i = 0; i < maxHPUI.Length; i++)
        {
            if (i < maxHP)
            {
                maxHPUI[i].SetActive(true);
            }
            else
            {
                maxHPUI[i].SetActive(false);
            }

            if (i < currentHP)
            {
                currentHPUI[i].SetActive(true);
            }
            else
            {
                currentHPUI[i].SetActive(false);
            }
        }
    }

    public void UpdateBulletUI()
    {
        // int currentAP = PlayerInfo.currentAP;
        // int maxAP = PlayerInfo.maxAP;

        // bulletUI.GetComponent<TMP_Text>().text = currentAP + " / " + maxAP;
    }

    #endregion


}
