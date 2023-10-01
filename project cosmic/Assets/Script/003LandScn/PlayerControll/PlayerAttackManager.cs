using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    public List<GameObject>playerBulletsPool = new List<GameObject>();

    public List<PlayerAttackData>playerAttackList = new List<PlayerAttackData>();


    public static int playerBulletSpriteID;


    public static float playerAttackDelay;


    public static float playerDMG;
    public static float dmgOffset;
    public static float dmgMultifly;





    #region "DMG"
    void DmgCal()
    {
        float calculatedDMG = (2 * Mathf.Sqrt((float)((dmgOffset * 1.2) + 1))) * dmgMultifly;

        playerDMG = (float)MathF.Round(calculatedDMG, 2);//둘째자리
    }


    void InitializePlayerBulletsPool()  //초기 생성
    {
        for (int i = 0 ; i <= 99 ; i++)
        {
            GameObject var = Instantiate(bullet,player.transform);

            var.SetActive(false);
            playerBulletsPool.Add(var);
        }
    }
    #endregion

    #region "attack delay"
    void AttackDelayLimitCheck()
    {
        if(playerAttackDelay >= 3)
        {
            playerAttackDelay = 3;
        }

        else if( playerAttackDelay < 0.3)
        {
            playerAttackDelay = 0.3f;
        }
    }

    


    #endregion

    void InitializePlayerBullets()  //총알 변경
    {
        //size & sprite
        foreach (GameObject bullet in playerBulletsPool)
        {
            bullet.GetComponent<SpriteRenderer>().sprite = playerAttackList[playerBulletSpriteID].bulletSprite;
        }
    }

    void ReSizeBullet(GameObject bullet)
    {
        Vector3 newSize = new Vector3(playerDMG*0.1f, playerDMG*0.1f, 0f);
        if(playerDMG < 100)
        {

        }
    }




}