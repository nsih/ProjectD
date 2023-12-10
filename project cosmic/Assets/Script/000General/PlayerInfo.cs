using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using UnityEngine.UIElements;



public class PlayerInfo : MonoBehaviour
{
    //status
    public static int physical;    //육체
    public static int mental;   //정신력
    public static int charm;       //매력

    //HP (Health Point)
    public static int maxHp;
    public static int maxHpOffset;
    public static int currentHP;

    //AP (Action Point)
    public static int maxAP;
    public static int maxAPOffset;
    public static int currentAP;

    //Money
    public static int coin;         //돈

    //데미지
    public static float playerDMG;
    public static float dmgPlusOffset;
    public static float dmgMultiflyOffset;

    //공속 -> 공격 딜레이 계산
    public static float attackSpeed;    //0~5
    public static float playerAttackDelay;

    //이속
    public static float playerMoveSpeed;
    public static float playerDashCoolTime;
    public static float playerDashSpeed;

    //시야
    public static float playerVisionSize;//20 default

    //etc
    public static float invincibilityTime; //피격시 무적시간



    //얻은 Item List
    public static List<ItemData> playerItemList = new List<ItemData>();

    //얻은 action List
    public static List<ActionData> playerActionList = new List<ActionData>();



    //
    GameObject landUICanvas;



    public void PlayerStatusInitialize() //Land SCN 진입시 호출
    {
        landUICanvas = GameObject.Find("LandUICanvas");

        //stat
        physical = 2;
        mental = 2;
        charm = 2;

        //hp
        MaxHpCalc();
        currentHP = maxHp;

        //action point
        maxAP = mental;
        currentAP = maxAP;

        maxAPOffset = 0;

        //coin
        coin = 0;

        //DMG
        dmgPlusOffset = 0;
        dmgMultiflyOffset = 1;
        DmgCal();

        //attack delay
        attackSpeed = 1;
        playerAttackDelay = 1;
        AttackDelayCalc();

        //move speed
        playerMoveSpeed = 10;
        playerDashCoolTime = 0.3f;
        playerDashSpeed = 30;

        //etc
        playerVisionSize = 20;


        //UI
        landUICanvas.GetComponent<LandUICon>().UpdateHPUI();
        landUICanvas.GetComponent<LandUICon>().UpdateAPUI();
        landUICanvas.GetComponent<LandUICon>().UpdateStatusText();
        landUICanvas.GetComponent<LandUICon>().UpdateCoinText();
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

        MaxHpCalc();
        landUICanvas.GetComponent<LandUICon>().UpdateStatusText();
    }
    public void MentalModify(int modifier)
    {
        int changedWillPower = mental + modifier;

        if (changedWillPower <= 0)
        {
            mental = 1;
        }

        else
        {
            mental = changedWillPower;
        }

        MaxAPCalc();

        landUICanvas.GetComponent<LandUICon>().UpdateStatusText();
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

        
        landUICanvas.GetComponent<LandUICon>().UpdateStatusText();
    }



    //hp
    public void MaxHpCalc()//최대 hp 변경 (+-)
    {
        maxHp = physical + maxHpOffset;

        landUICanvas.GetComponent<LandUICon>().UpdateHPUI();
    }

    public void MaxHPOffsetModify(int offset)
    {
        int changedMaxHPOffset = maxAPOffset + offset;
        currentAP = changedMaxHPOffset;


        MaxHpCalc();

        landUICanvas.GetComponent<LandUICon>().UpdateHPUI();
    }
    public void HpModify(int offset)//hp 변경시 호출 (+-)
    {
        int changedHp = currentHP + offset;

        if (changedHp <= 0)
        {
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

        landUICanvas.GetComponent<LandUICon>().UpdateHPUI();
    }

    //action point
    public void MaxAPCalc()
    {
        maxAP = mental + maxAPOffset;

        landUICanvas.GetComponent<LandUICon>().UpdateAPUI();
    }

    public void MaxAPOffsetModify(int offset)
    {
        int changedMaxAPOffset = maxAPOffset + offset;

        if (changedMaxAPOffset <= 0)
        {
            currentAP = 0;
        }
        else
        {
            currentAP = changedMaxAPOffset;
        }
        

        MaxAPCalc();
        landUICanvas.GetComponent<LandUICon>().UpdateAPUI();
    }


    public void APModify(int offset)
    {
        int changedActionPoint = currentAP + offset;

        if (changedActionPoint <= 0)
        {
            currentAP = 0;
        }
        else
        {
            currentAP = changedActionPoint;
        }

        landUICanvas.GetComponent<LandUICon>().UpdateAPUI();
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
    public void AttacSpeedModify(float offset)
    {
        float changedAttackSpeed = attackSpeed + offset;

        if(changedAttackSpeed <= 0)
        {
            attackSpeed = 0;
        }
        else if(changedAttackSpeed > 5.0f)
        {
            attackSpeed = 5;
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

        if(changedMoveSpeed < 1)
        {
            playerMoveSpeed = 1;
        }
        else
        {
            playerMoveSpeed = changedMoveSpeed;
        }
    }

    //money
    public void CoinModify(int offset)
    {
        int changedCoin = coin + offset;

        if(changedCoin <= 0)
        {
            coin = 0;
        }

        else
        {
            coin = changedCoin;
        }

        landUICanvas.GetComponent<LandUICon>().UpdateCoinText();
    }

    public void VisionSizeModify(float offset)
    {
        float changedPlayerVsionSize = playerVisionSize + offset;

        if(changedPlayerVsionSize <= 10)
        {
            playerVisionSize = 10;
        }

        else
        {
            playerVisionSize = changedPlayerVsionSize;
        }


        //
        GameObject.Find("MainCamara").GetComponent<Camera>().orthographicSize = playerVisionSize;
    }

}
