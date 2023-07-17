using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInfo : MonoBehaviour
{
    //보이는 것
    public int maxHp;
    public int hp;

    public int actionLimit; //최대 액션스택

    public float maxSpellGuage;
    public float spellGauge;


    //기초 스탯 + (곱연산자, 합연산자)
    


    //보이는 것 2
    public static int physical;    //육체      공격력
    public static int willPower;   //의지      방어력
    public static int knowledge;   //지식      spell recharge
    public static int charm;       //매력      주사위 가중치

    //attack
    public static int attackType;
    public static float attackSpeed;   //휘두르는 속도
    public static float attackTerm;   //휘두르는 텀

    //spell
    public static int spellType;
    public static float spellPower;     //폭탄 계수 (not 개수)
    public static float spellRecharge;  //원충효


    //etc
    public static float invincibilityTime; //무적시간
    
    void Start()
    {
        PlayerStatusInitialize();
    }


    void PlayerStatusInitialize() //처음 게임 시작하거나 뒤지면 호출
    {
        //hp
        maxHp = 10;
        hp = maxHp;
        //Spell
        maxSpellGuage = 100;
        spellGauge = maxSpellGuage;

        //action Stack
        actionLimit = 2;

        //stat
        physical = 1;
        willPower = 1;
        knowledge = 1;
        charm = 1;

        //
    }




    //hp, maxhp
    void MaxHpModify(int modifier)//최대 hp 변경 (+-)
    {
        int changedMaxHp = maxHp + modifier;
        if(changedMaxHp <= 0)
        {
            changedMaxHp = 1;
            maxHp = changedMaxHp;
            hp = changedMaxHp;
        }

        else
        {
            maxHp = changedMaxHp;
            hp = hp + modifier;
        }
    }
    void MaxHpModify(float modifierP,bool inc)//최대체력이 최대체력 퍼센트 오르락내리락
    {
        int modifier = (int)Math.Floor(maxHp * (modifierP/100) );

        int changedMaxHp;
        if(inc == true)
        {
            changedMaxHp = maxHp+modifier;
        }
        else
        {
            changedMaxHp = maxHp-modifier;
        }

        if(changedMaxHp <= 0)
        {
            changedMaxHp = 1;
            hp = changedMaxHp;
        }

        else
        {
            maxHp = changedMaxHp;
            hp = hp + modifier;
        }
    }
    
    void HpModify(int modifier)//hp 변경시 호출 (+-)
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
    void HpModify(double percentHP,bool max,bool inc)  //체력이 퍼센트 오르락내리락
    {
        int modifier;
        if(max == true)
        {
            modifier = (int)Math.Floor(maxHp * (percentHP/100) );
        }
        else
        {
            modifier = (int)Math.Floor(hp * (percentHP/100) );
        }

        int changedHp;
        if(inc == true)
        {
            changedHp = hp+modifier;
        }
        else
        {
            changedHp = hp-modifier;
        }


        if(changedHp >= maxHp)
        {
            hp  = maxHp;
        }
        else if (changedHp <= 0)
        {
            hp = 0;
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
    
    //Spell
    void SpellGuageModify(float modifier)   //게이지 변화시 호출 (쓰거나 충전)
    {
        float changedSpellGuage = spellGauge+modifier;

        if(changedSpellGuage > maxSpellGuage)
        {
            spellGauge  = maxSpellGuage;
        }
        else if (changedSpellGuage <= 0)
        {
            spellGauge = 0;
        }
        else
        {
            spellGauge = changedSpellGuage;
        }
    }
    
    void SpellRechargeModify(float modifier) //붐충효
    {
        float changedSpellRecharge = spellRecharge + modifier;

        if(changedSpellRecharge <= 0)
        {
            spellRecharge = 1;
        }

        else
        {
            spellRecharge = changedSpellRecharge;
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
