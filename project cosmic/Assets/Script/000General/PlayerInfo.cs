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
    public static float attackDelay;

    public static float plusAttackDamageOffset;
    public static float multiplyAttackDamageOffset;

    public static float attackDamage;
    

    public static int attackID;



    //etc
    public static float invincibilityTime; //피격시 무적시간

    public static int coin;
    
    void Start()
    {
        PlayerStatusInitialize();
        DamageCal();
    }

    void Update ()
    {

    }

    public void DamageCal()
    {
        attackDamage = ( physical + plusAttackDamageOffset )   * multiplyAttackDamageOffset;
    }

    void PlayerStatusInitialize() //처음 게임 시작하거나 뒤지면 호출
    {
        //move
        speed = 15;
        dashCooldown = 0.75f;
        lastDashTime = 0;

        coin = 1;




        //hp
        maxHp = 20;
        hp = maxHp;
        //attack

        plusAttackDamageOffset = 0;
        multiplyAttackDamageOffset = 0;
        DamageCal();

        attackDelay = 0.75f;


        //action Stack
        actionStackLimit = 1;

        //stat
        physical = 1;
        willPower = 1;
        knowledge = 1;
        charm = 1;

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
    
    public void HpPlus(int offset)//hp 변경시 호출 (+-)
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


    //dice

    //battle cal
}
