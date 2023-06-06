using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    GameObject gameManager; //키정보
    GameObject playerInfo;  //player status
    GameObject player;



    KeyCode playerUpKey;
    KeyCode playerDownKey;
    KeyCode playerLeftKey;
    KeyCode playerRightKey;

    float speed;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("player");

        //나중에 gameGanager에서 설정된 키 설정을 가져오세요
        playerUpKey = KeyCode.W;
        playerLeftKey = KeyCode.A;
        playerDownKey = KeyCode.S;
        playerRightKey = KeyCode.D;


        //PlayerInfo에서 가져오세요
        speed = 3.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
    }


    private void PlayerMovement() 
    {
            if (Input.GetKey(playerUpKey))
            player.transform.Translate(Vector2.up * Time.deltaTime*speed);

            if (Input.GetKey(playerLeftKey))
                player.transform.Translate(Vector2.left * Time.deltaTime*speed);

            if (Input.GetKey(playerDownKey))
                player.transform.Translate(Vector2.down * Time.deltaTime *speed);

            if (Input.GetKey(playerRightKey))
                player.transform.Translate(Vector2.right * Time.deltaTime*speed);
    }
}
