using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    GameObject gameManager; //키정보
    GameObject playerInfo;  //player status
    GameObject player;
    GameObject handPivot;



    KeyCode playerUpKey;
    KeyCode playerDownKey;
    KeyCode playerLeftKey;
    KeyCode playerRightKey;


    bool isAttack;


    float speed;
    float attackSpeed;  //초당회전각도

    float attackDelay;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("player");
        handPivot = GameObject.Find("handPivot");

        //나중에 gameGanager에서 설정된 키 설정을 가져오란 말입니다.
        playerUpKey = KeyCode.W;
        playerLeftKey = KeyCode.A;
        playerDownKey = KeyCode.S;
        playerRightKey = KeyCode.D;


        //PlayerInfo에서 가져오세요
        speed = 1f;
        attackSpeed = 700;   //초당회전각도
        isAttack = false;
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isAttack)
        {
            StartCoroutine(BasicAttack());
        }
    }
    void FixedUpdate()
    {
        PlayerMovement();
        RotatingPivot();
    }

    
    void PlayerMovement() 
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

    void RotatingPivot()    //
    {
        if(!isAttack)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector3 direction = mousePosition - handPivot.transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            handPivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    
    IEnumerator BasicAttack()
    {
        Quaternion startRotation = handPivot.transform.rotation;

        Quaternion targetRotation0 = startRotation * Quaternion.Euler(0f, 0f, 90);
        Quaternion targetRotation1 = startRotation * Quaternion.Euler(0f, 0f, -90);


        //
        float elapsedTime = 0f;
        float rotationTime = Quaternion.Angle(startRotation, targetRotation0) / 1000;
        while (elapsedTime < rotationTime)
        {
            isAttack = true;

            float t = elapsedTime / rotationTime;
            handPivot.transform.rotation = Quaternion.Lerp(startRotation, targetRotation0, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //
        elapsedTime = 0f;
        rotationTime = Quaternion.Angle(targetRotation0, targetRotation1) / 1800;
        while (elapsedTime < rotationTime)
        {
            isAttack = true;

            float t = elapsedTime / rotationTime;
            handPivot.transform.rotation = Quaternion.Lerp(targetRotation0, targetRotation1, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //
        elapsedTime = 0f;
        rotationTime = Quaternion.Angle(targetRotation1, startRotation) / 1800;
        while (elapsedTime < rotationTime)
        {
            isAttack = true;

            float t = elapsedTime / rotationTime;
            handPivot.transform.rotation = Quaternion.Lerp(targetRotation1, startRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        isAttack = false;
    }
}
