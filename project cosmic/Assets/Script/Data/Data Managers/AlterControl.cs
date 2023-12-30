using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
using System;


public class AlterControl : MonoBehaviour
{
    AlterData alterData = null;
    
    void OnEnable()
    {
        alterData = GameObject.Find("GameManager").gameObject.GetComponent<AlterManager>().GetAlterData();

        gameObject.GetComponent<SpriteRenderer>().sprite = alterData.alterSprite;   
    }

    void OnDisable()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        alterData = null;
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //상호작용
            if(Input.GetKeyDown(KeyCode.E) && !GameManager.isLandTalking)
            {
                Debug.Log("inter Action Btn Pressed!!!!");

                GameObject.Find("GameManager").GetComponent<AlterManager>().AlterInteraction(alterData);
            }
        }
    }




}
