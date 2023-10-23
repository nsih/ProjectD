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
    public static int maxHp;
    public static int maxHpOffset;
    public static int hp;

    //Sanity
    public static int maxSanity;
    public static int maxSanityOffset;
    public static int sanity;

    //AP (Action Point)
    public static int maxActionPoint;
    public static int maxActionPointOffset;
    public static int actionPoint;

    //Money
    public static int coin;         //돈

    //데미지
    public static float playerDMG;
    public static float dmgOffset;
    public static float dmgMultifly;

    //공속
    public static float playerAttackDelay;
    public static float delayMultifly;
    public static float attackSpeed;    //0~5


    //이속
    public static float playerMoveSpeed;
    public static float playerCoolTime;

    public static float playerDashSpeed;



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
        MaxHpCalc();
        hp = maxHp;

        //sanity
        maxSanity = knowledge + maxSanityOffset;
        sanity = maxSanity;

        //action point
        maxActionPoint = willPower;
        actionPoint = maxActionPoint;

        //action Point
        maxActionPoint = 1;

        //coin
        coin = 0;

        //DMG
        dmgOffset = 0;
        dmgMultifly = 1;
        DmgCal();

        //attack delay
        playerAttackDelay = 1;
        delayMultifly = 1;
        AttackDelayCalc();

        //move speed
        playerMoveSpeed = 10;
        playerCoolTime = 0.3f;
        playerDashSpeed = 30;

    }



    //status
    public void PhysicalModify(int modifier)
    {
        int changedPhisical = physical + modifier;

        if (changedPhisical <= 0)
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

        if (changedWillPower <= 0)
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

        if (changedKnowledge <= 0)
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

        if (changedCharm <= 0)
        {
            charm = 1;
        }

        else
        {
            charm = changedCharm;
        }
    }



    //hp
    public void MaxHpCalc()//최대 hp 변경 (+-)
    {
        maxHp = (physical * 10) + maxHpOffset;
    }
    public void HpModify(int offset)//hp 변경시 호출 (+-)
    {
        int changedHp = hp + offset;

        if (changedHp <= 0)
        {
            Debug.Log("겜 오버");
        }
        else if (changedHp >= maxHp)
        {
            hp = maxHp;
        }
        else
        {
            hp = changedHp;
        }
    }

    //Sanity
    public void MaxSanityCalc()
    {
        maxSanity = (knowledge * 2) + maxSanityOffset;
    }
    public void SanityModify(int modifier)
    {
        int changedsanity = sanity + modifier;

        if (changedsanity < -maxSanity)
        {
            //게임 오바
        }
        else
        {
            sanity = changedsanity;
        }
    }

    //action point
    public void MaxActionPointCalc()
    {
        maxActionPoint = willPower + maxActionPointOffset;
    }
    public void ActionPointModify(int modifier)
    {
        int changedActionPoint = actionPoint + modifier;

        if (changedActionPoint <= 0)
        {
            actionPoint = 0;
        }
        else
        {
            actionPoint = changedActionPoint;
        }
    }

    //Damage
    void DmgCal()
    {
        if (dmgOffset <= 0)
            dmgOffset = 0;

        float calculatedDMG = (2 * Mathf.Sqrt((float)((dmgOffset * 1.2) + 1))) * dmgMultifly;

        playerDMG = MathF.Round(calculatedDMG, 2);
    }
    /*
    void DmgDebug()
    {
        for (int i = 0; i < 100; i = i + 1)
        {
            float result = 2 * Mathf.Sqrt((float)((i * 1.2) + 1));
            Debug.Log("i : " + i + "   dmg : " + MathF.Round(result,2));
        }
    }
    */


    //attack delay
    void AttackDelayCalc()
    {
        //limit
        if (attackSpeed < 0)
            attackSpeed = 0;
        else if (attackSpeed >= 5)
            attackSpeed = 5;


        float result = (float) (16 - (5 * Mathf.Sqrt((float)(attackSpeed * 1.3) + 1))  ) / 10;

        playerAttackDelay = result * delayMultifly;
    }
    /*
    void DelayDebug()
    {
        for (float i = 1; i <= 5; i = i + 0.1f)
        {
            float result = (float) (16 - (5 * Mathf.Sqrt((float)(i * 1.3) + 1)))/10;
            Debug.Log("i = " + i + "    result = " + result);

            //Mathf.Round((float)damage, 2))
        }
    }
    */

}
