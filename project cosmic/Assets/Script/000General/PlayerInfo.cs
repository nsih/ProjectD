 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInfo : MonoBehaviour
{
    //status
    public static int physical;    //육체
    public static int willPower;   //의지
    public static int knowledge;   //지식
    public static int charm;       //매력

    //HP (Health Point)
    public static float maxHp;
    public static float hp;

    //Sanity
    public static int maxSanity;
    public static int sanity;

    //AP (Action Point)
    public static int MaxActionPoint;
    public static int actionPoint;

    //Money
    public static int coin;         //돈

    /*
    //이동
    public static float speed;

    public static float dashCooldown;
    public static float lastDashTime;

    //attack
    public static float playerAttackDelay;
    public static float playerDMG;
    */



    //etc
    public static float invincibilityTime; //피격시 무적시간
    
    void Start()
    {
        PlayerStatusInitialize();
    }

    void PlayerStatusInitialize() //처음 게임 시작하거나 뒤지면 호출
    {
        //stat
        physical = 1;
        willPower = 1;
        knowledge = 1;
        charm = 1;

        //hp
        maxHp = 100;
        hp = maxHp;

        //sanity
        maxSanity = knowledge * 2;
        sanity = maxSanity;

        //action point
        MaxActionPoint = willPower;
        actionPoint = MaxActionPoint;

        //coin
        coin = 0;

        //action Point
        MaxActionPoint = 1;


        /*
        //speed
        speed = 5;

        //dash
        dashCooldown = 0.75f;
        lastDashTime = 0;

        //attack
        playerAttackDelay = 0.75f;
        playerDMG = physical*2.5f;
        */
    }



    //status
    public void PhysicalModify(int modifier)
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
    public void WillPowerModify(int modifier)
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
    public void KnowledgeModify(int modifier)
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
    public void CharmModify(int modifier)
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



    //hp, maxhp
    public void MaxHpPlus(int offset)//최대 hp 변경 (+-)
    {
        float changedMaxHp = maxHp + offset;
        if(changedMaxHp <= 0)
        {
            changedMaxHp = 1;
            maxHp = changedMaxHp;
            hp = changedMaxHp;
        }

        else
        {
            maxHp = changedMaxHp;
            hp = hp + offset;
        }
    }

    public void MaxHpMultiply(float offset)//최대체력이 최대체력 퍼센트 오르락내리락
    {
        float changedMaxHp = maxHp*offset;

        if(changedMaxHp <= 0)
        {
            changedMaxHp = 1;
        }

        else
        {
            changedMaxHp = offset;
        }

        maxHp = changedMaxHp;
    }
    
    public void HpModify(int offset)//hp 변경시 호출 (+-)
    {
        float changedHp = hp+offset;

        if(changedHp <= 0)
        {
            Debug.Log("겜 오버");
        }
        else if(changedHp >= maxHp)
        {
            hp = maxHp;
        }
        else
        {
            hp = changedHp;
        }
    }

    public void HpModify(bool live)    //풀회복or끝
    {
        if(live)
        {
            hp = maxHp;
        }

        else
        {
            hp = 0;
        }
    }

    public void ActionPointModify(int modifier)
    {
        int changedActionPoint = actionPoint+modifier;

        if (changedActionPoint <= 0)
        {
            actionPoint = 0;
        }
        else
        {
            actionPoint = changedActionPoint;
        }
    }


    public void sanityModify(int modifier)
    {
        int changedsanity = sanity+modifier;

        if (changedsanity < -10)
        {
            //게임 오바
        }
        else
        {
            sanity = changedsanity;
        }
    }
}
