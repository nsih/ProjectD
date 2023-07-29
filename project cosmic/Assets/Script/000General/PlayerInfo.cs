 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInfo : MonoBehaviour
{
    //HP
    public static float maxHp;
    public static float hp;

    public int actionStackLimit; //최대 액션스택


    //이동
    public static float speed;

    public static float dashCooldown;
    public static float lastDashTime;



    //status
    public static int physical;    //육체      공격력
    public static int willPower;   //의지      HP
    public static int knowledge;   //지식      의식 사용
    public static int charm;       //매력      주사위 가중치

    //attack
    public static float basicAttackDamage;
    public static float attackDamage;
    

    public static int attackID;



    //etc
    public static float invincibilityTime; //피격시 무적시간
    
    void Start()
    {
        PlayerStatusInitialize();
        DamageCal();
    }

    void Update ()
    {

    }

    void DamageCal()
    {
        basicAttackDamage = physical * 2;
    }

    void PlayerStatusInitialize() //처음 게임 시작하거나 뒤지면 호출
    {
        //move
        speed = 15;
        dashCooldown = 0.75f;
        lastDashTime = 0;




        //hp
        maxHp = 20;
        hp = maxHp;
        //Spell
        //action Stack
        actionStackLimit = 1;

        //stat
        physical = 1;
        willPower = 1;
        knowledge = 1;
        charm = 1;

    }




    //hp, maxhp
    void MaxHpPlus(int offset)//최대 hp 변경 (+-)
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

    void MaxHpMultiply(float offset)//최대체력이 최대체력 퍼센트 오르락내리락
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
    
    void HpPlus(float offset)//hp 변경시 호출 (+-)
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

    void HpMultiplyPlus(float offset)  //체력이 퍼센트 오르락내리락
    {
        float changedHp = hp + (maxHp*offset);

        if(changedHp >= maxHp)
        {
            hp = maxHp;
        }
        else
        {
            hp = changedHp;
        }
    }

    void HpMultiplyMinus(float offset)  //체력이 퍼센트 오르락내리락
    {
        float changedHp = hp - (hp*offset);

        if(changedHp <= 0)
        {
            Debug.Log("겜 오버");
        }
        else
        {
            hp = changedHp;
        }
    }

    void HpModify(bool life)    //풀회복or끝
    {
        if(life)
        {
            hp = maxHp;
        }

        else
        {
            hp = 0;
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


    //dice

    //battle cal
}
