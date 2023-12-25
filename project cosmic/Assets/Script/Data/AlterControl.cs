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
        alterData = GameObject.Find("GameManager").GetComponent<AlterManager>().GetAlterData();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //상호작용 가능 표시.
            //gameObject.GetComponent<SpriteRenderer>().color = 


            //상호작용
            if(Input.GetKeyDown(KeyCode.E))
            {
                GameObject.Find("GameManager").GetComponent<AlterManager>().AlterInteraction(alterData);
            }
        }
    }




}
