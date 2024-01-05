using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPoolManager : MonoBehaviour
{
    GameObject gameManager;
    GameObject player;
    
    public GameObject bullet;


    public List<GameObject> playerBulletsPool = new List<GameObject>();
    public List<PlayerAttackData> playerAttackList = new List<PlayerAttackData>();
    static int playerBulletSpriteID;

    void Start()
    {
        InitializePlayerBulletsPool();
    }

    void InitializePlayerBulletsPool()  //초기 생성
    {
        gameManager =  GameObject.Find("GameManager");
        player = GameObject.Find("player");

        for (int i = 0; i <= 30; i++)
        {
            GameObject var = Instantiate(bullet, gameManager.transform);

            var.SetActive(false);
            playerBulletsPool.Add(var);
        }
    }

    void InitializePlayerBullets()  //총알 변경
    {
        //size & sprite
        foreach (GameObject bullet in playerBulletsPool)
        {
            bullet.GetComponent<SpriteRenderer>().sprite = playerAttackList[playerBulletSpriteID].bulletSprite;
        }
    }

    void ReSizeBullet(GameObject bullet)    //크기 변경
    {
        Vector3 newSize = new Vector3(PlayerInfo.playerDMG * 0.1f, PlayerInfo.playerDMG * 0.1f, 0f);
        if (PlayerInfo.playerDMG < 100)
        {
            //보류
        }
    }




}