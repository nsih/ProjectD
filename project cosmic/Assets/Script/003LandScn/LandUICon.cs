using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandUICon : MonoBehaviour
{
    GameObject pnlBackGround;

    void Start()
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
    }

    // Update is called once per frame
    void Update()
    {
        ShowMiniMap();
    }

    void ShowMiniMap()
    {
        GameObject miniMap;

        if(Input.GetKey(KeyCode.Tab))
        {
            if(GameManager.currentStage == 0)
            {
                miniMap = pnlBackGround.transform.Find("Stage0MiniMap").gameObject;

                if(miniMap != null)
                    miniMap.SetActive(true);
            }

            else
            {
                Debug.Log(GameManager.currentStage);
            }
        }





        if(Input.GetKeyUp(KeyCode.Tab))
        {
            if(GameManager.currentStage == 0)
            {
                miniMap = GameObject.Find("Stage0MiniMap");

                miniMap.SetActive(false);
            }

            else
            {
                Debug.Log(GameManager.currentStage);
            }
        }

        
    }
}
