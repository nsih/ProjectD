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
    public int physical;    //육체
    public int willPower;   //의지
    public int knowledge;   //지식
    public int charm;       //매력

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
        physical = 1;
        willPower = 1;
        knowledge = 1;
        charm = 2;

        //
    }




    //hp
    void MaxHpModify(int modifier)//최대 hp
    {
        int changedMaxHp = maxHp = modifier;
        if(changedMaxHp <= 0)
        {
            changedMaxHp = 1;
        }

        else
        {
            maxHp = changedMaxHp;
        }
    }
    void HpModify(int modifier)//hp 변경시 호출
    {
        int changedHp = hp+modifier;

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
    void BombGuageModify(float modifier)   //게이지 변화시 호출 (쓰거나 충전)
    {
        float changedBombGuage = bombGauge+modifier;

        if(changedBombGuage > maxHp)
        {
            bombGauge  = maxBombGuage;
        }
        else if (changedBombGuage <= 0)
        {
            bombGauge = 0;
        }
        else
        {
            bombGauge = changedBombGuage;
        }
    }
    void BombRechargeModify(float modifier) //붐충효
    {
        float changedBombRecharge = bombRecharge + modifier;

        if(changedBombRecharge <= 0)
        {
            bombRecharge = 1;
        }

        else
        {
            bombRecharge = changedBombRecharge;
        }
    }

    //status
    void PhysicalModify(int modifier)
    {
        int changedPhisical = physical + modifier;

        if(changedPhisical <= 0)
        {
            physical = 1;
        }

        else
        {
            physical = changedPhisical;
        }
    }
    void WillPowerModify(int modifier)
    {
        int changedWillPower = willPower + modifier;

        if(changedWillPower <= 0)
        {
            willPower = 1;
        }

        else
        {
            willPower = changedWillPower;
        }
    }
    void KnowledgeModify(int modifier)
    {
        int changedKnowledge = knowledge + modifier;

        if(changedKnowledge <= 0)
        {
            knowledge = 1;
        }

        else
        {
            knowledge = changedKnowledge;
        }
    }
    void CharmModify(int modifier)
    {
        int changedCharm = charm + modifier;

        if(changedCharm <= 0)
        {
            charm = 1;
        }

        else
        {
            charm = changedCharm;
        }
    }


    //battle cal
}
