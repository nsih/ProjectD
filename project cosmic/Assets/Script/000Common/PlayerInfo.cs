using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    //보이는 것
    public int maxHp;
    public int hp;

    public float maxBombGuage;
    public float bombGauge;
    


    //보이는 것 2
    public int Physical;
    public int willPower;
    public int knowledge;
    public int charm;

    //attack
    public int attackType;
    public float attackSpeed;   //휘두르는 속도
    public float attackTerm;   //휘두르는 텀

    //bomb
    public float bombPower;     //폭탄 계수
    public float bombRecharge;  //원충효


    //etc
    public float invincibilityTime; //무적시간
    
    void Start()
    {
        PlayerStatusInitialize();
    }

    void Update()
    {
        
    }


    void PlayerStatusInitialize() //처음 게임 시작하거나 뒤지면 호출
    {
        //hp
        maxHp = 10;
        hp = maxHp;
        //bomb
        maxBombGuage = 100;
        bombGauge = maxBombGuage;

        //stat
        Physical = 1;
        willPower = 1;
        knowledge = 1;
        charm = 2;

        //
    }




    //hp
    void MaxHpModify(int maxHpModifier)//최대 hp
    {
        int changedMaxHp = maxHp = maxHpModifier;
        if(changedMaxHp <= 0)
        {
            changedMaxHp = 1;
        }

        else
        {
            maxHp = changedMaxHp;
        }
    }
    void HpModify(int hpModifier)//hp 변경시 호출
    {
        int changedHp = hp+hpModifier;

        if(changedHp >= maxHp)
        {
            hp  = maxHp;
        }
        else if (changedHp <= 0)
        {
            Debug.Log("game over");
        }
        else
        {
            hp = changedHp;
        }
    }
    
    //bomb
    void BombGuageModify(float bombGuageModifier)   //게이지 변화시 호출 (쓰거나 충전)
    {
        if(bombGauge+bombGuageModifier > maxHp)
        {
            bombGauge  = maxBombGuage;
        }
        else if (bombGauge+bombGuageModifier <= 0)
        {
            bombGauge = 0;
        }
        else
        {
            bombGauge = bombGauge+bombGuageModifier;
        }
    }
    void BombRechargeModify(float BombRechargeModifier) //붐충효
    {
        float changedBombRecharge = bombRecharge + BombRechargeModifier;

        if(changedBombRecharge <= 0)
        {
            bombRecharge = 1;
        }

        else
        {
            bombRecharge = changedBombRecharge;
        }
    }

    //stat

    //battle cal
}
