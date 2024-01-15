using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using UnityEngine.UIElements;



public class PlayerInfo : MonoBehaviour
{
    GameObject canvas;


    //HP (Health Point)
    public static int maxHp;
    public static int maxHpOffset;
    public static int currentHP;

    //데미지
    public static float playerDMG;
    public static float dmgPlusOffset;
    public static float dmgMultiflyOffset;

    //공속 -> 공격 딜레이 계산
    public static float attackSpeed;    //0~6
    public static float playerAttackDelay;

    //이속
    public static float playerMoveSpeed;
    public static float playerDashCoolTime;
    public static float playerDashSpeed;

    //시야
    public static float playerVisionSize;//20 default

    //etc
    public static bool isInvincible;
    public static float invincibilityTime; //피격시 무적시간



    public void PlayerStatusInitialize() //Land SCN 진입시 호출
    {
        canvas = GameObject.Find("Canvas");


        //hp
        maxHp = 5;
        currentHP = maxHp;

        //DMG
        dmgPlusOffset = 0;
        dmgMultiflyOffset = 1;
        DmgCal();

        //attack delay
        attackSpeed = 2.75f;    //6is max
        AttackDelayCalc();

        //move speed
        playerMoveSpeed = 25;
        playerDashCoolTime = 0.3f;
        playerDashSpeed = 45;

        //etc
        playerVisionSize = 15;

        //invincible
        isInvincible = false;
        invincibilityTime = 1.5f;


        //UI
        canvas.GetComponent<UICon>().UpdateHPUI();
    }


    public void OutcomeOffsetApply(OutcomeOffset[] outcomeOffsets)
    {
        for (int i = 0; i < outcomeOffsets.Length; i++)
        {
            #region "HP, Bullet"
            //hp
            if (outcomeOffsets[i].offsetType == OutcomeOffsetType.HpOffset)
            {
                HpModify((int)outcomeOffsets[i].offset);
            }
            //max hp
            else if (outcomeOffsets[i].offsetType == OutcomeOffsetType.MaxHPOffset)
            {
                MaxHPOffsetModify((int)outcomeOffsets[i].offset);
            }
            #endregion

            #region "Ingame Control related"
            // Damage (plus offset)
            else if (outcomeOffsets[i].offsetType == OutcomeOffsetType.PlusPlayerDamageOffset)
            {
                DMGPlusModify(outcomeOffsets[i].offset);
            }
            // Damage (multifly offset)
            else if (outcomeOffsets[i].offsetType == OutcomeOffsetType.MultiplyPlayerDamageOffset)
            {
                DMGMultiflyModify(outcomeOffsets[i].offset);
            }

            // Attack delay (speed)
            else if (outcomeOffsets[i].offsetType == OutcomeOffsetType.AttackSpeedOffset)
            {
                AttacSpeedModify((int)outcomeOffsets[i].offset);
            }

            //MoveSpeed
            else if (outcomeOffsets[i].offsetType == OutcomeOffsetType.MoveSpeedOffset)
            {
                MoveSpeedModify(outcomeOffsets[i].offset);
            }
            #endregion

            //vision
            else if (outcomeOffsets[i].offsetType == OutcomeOffsetType.CameraSizeOffset)
            {
                VisionSizeModify(outcomeOffsets[i].offset);
            }
        }
    }



    //hp
    public void MaxHPOffsetModify(int offset)
    {
        int changedMaxHPOffset = maxHpOffset + offset;

        maxHpOffset = changedMaxHPOffset;

        if(offset > 0)
        {
            currentHP =+ offset;
        }

        canvas.GetComponent<UICon>().UpdateHPUI();

        //Debug.Log("MAX HP +" + offset);
    }
    public void HpModify(int offset)//hp 변경시 호출 (+-)
    {
        int changedHp = currentHP + offset;

        if (changedHp <= 0)
        {
            currentHP = changedHp;
            Debug.Log("겜 오버");
        }
        else if (changedHp >= maxHp)
        {
            currentHP = maxHp;
        }
        else
        {
            currentHP = changedHp;
        }

        canvas.GetComponent<UICon>().UpdateHPUI();
    }

    //Damage
    public void DMGPlusModify(float offset)
    {
        float changedDmgPlusOffset = dmgPlusOffset + offset;

        if (changedDmgPlusOffset <= 0)
        {
            dmgPlusOffset = 0;
        }
        else
        {
            dmgPlusOffset = changedDmgPlusOffset;
        }


        DmgCal();
    }
    public void DMGMultiflyModify(float offset)
    {
        float changedDmgOffset = dmgMultiflyOffset * offset;

        dmgMultiflyOffset = changedDmgOffset;


        DmgCal();
    }

    public void DmgCal()
    {
        if (dmgPlusOffset <= 0)
            dmgPlusOffset = 0;

        float calculatedDMG = (2 * Mathf.Sqrt((float)((dmgPlusOffset * 1.2) + 1))) * dmgMultiflyOffset;

        playerDMG = MathF.Round(calculatedDMG, 2);
    }



    //attack delay
    public void AttacSpeedModify(float offset)
    {
        float changedAttackSpeed = attackSpeed + offset;

        if (changedAttackSpeed <= 0)
        {
            attackSpeed = 0;
        }
        else if (changedAttackSpeed > 6.0f)
        {
            attackSpeed = 6;
        }
        else
        {
            attackSpeed = changedAttackSpeed;
        }

        AttackDelayCalc();
    }
    public void AttackDelayCalc()
    {
        playerAttackDelay = (float)(16 - (5 * Mathf.Sqrt((float)(attackSpeed * 1.3) + 1))) / 10;
    }


    //MoveSpeed
    public void MoveSpeedModify(float offset)
    {
        float changedMoveSpeed = offset + playerMoveSpeed;

        if (changedMoveSpeed < 1)
        {
            playerMoveSpeed = 1;
        }
        else
        {
            playerMoveSpeed = changedMoveSpeed;
        }
    }

    public void VisionSizeModify(float offset)
    {
        float changedPlayerVsionSize = playerVisionSize + offset;

        if (changedPlayerVsionSize <= 10)
        {
            playerVisionSize = 10;
        }

        else
        {
            playerVisionSize = changedPlayerVsionSize;
        }


        //
        GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = playerVisionSize;
    }

}


[System.Serializable]
public enum OutcomeOffsetType
{
    // HP
    MaxHPOffset,
    HpOffset,

    //AP
    MaxBulletOffset,

    // Damage
    PlusPlayerDamageOffset,
    MultiplyPlayerDamageOffset,

    // Attack delay (speed)
    AttackSpeedOffset,

    // Move speed
    MoveSpeedOffset,

    // Vision
    CameraSizeOffset,

    // Reward etc
    ItemID
}
