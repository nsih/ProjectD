using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    GameObject gameManager; //키정보
    GameObject playerInfo;  //player status
    GameObject player;
    GameObject handPivot;

    Animator playerAnimator;


    KeyCode playerUpKey;
    KeyCode playerDownKey;
    KeyCode playerLeftKey;
    KeyCode playerRightKey;


    bool isWalk;
    bool canAttack;
    bool isAttack;
    bool isMouseLeft;


    float speed;
    float attackSpeed;  //초당회전각도

    float attackTimer;
    float attackTerm;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("player");
        playerAnimator = this.gameObject.GetComponent<Animator>();

        handPivot = GameObject.Find("handPivot");

        //나중에 gameGanager에서 설정된 키 설정을 가져오란 말입니다.
        playerUpKey = KeyCode.W;
        playerLeftKey = KeyCode.A;
        playerDownKey = KeyCode.S;
        playerRightKey = KeyCode.D;


        //Game Manager의 PlayerInfo에서 가져오세요
        speed = 10f;
        attackSpeed = 1200;   //초당회전각도 .. 인데 텀이 중요하지 휘두르는 속도가 중요할까? 검술에 따라서 바뀌는 경우는 있어도 능력치로 넣기는 좀

        canAttack = true;
        attackTerm = 0.5f;
        
        isAttack = false;
    }


    void Update()
    {
        attackTimer -= Time.deltaTime;

        if(Input.GetMouseButtonDown(0) && !isAttack && canAttack)
        {
            AttackTimerStart();
            StartCoroutine(BasicAttack());
        }


        //animating
        
        CheckWalk();
    
    }
    void FixedUpdate()
    {
        PlayerMovement();
        RotatingPivot();
    }
    void LateUpdate()
    {
        if (attackTimer <= 0f)
        {
            canAttack = true;
        }
    }

    
    void PlayerMovement()
{
    if (Input.GetKey(playerUpKey) || Input.GetKey(playerLeftKey) || Input.GetKey(playerDownKey) || Input.GetKey(playerRightKey))
    {
        isWalk = true;

        if (Input.GetKey(playerUpKey))
        {
            player.transform.Translate(Vector2.up * Time.deltaTime * speed);
        }

        if (Input.GetKey(playerLeftKey))
        {
            player.transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        if (Input.GetKey(playerDownKey))
        {
            player.transform.Translate(Vector2.down * Time.deltaTime * speed);
        }

        if (Input.GetKey(playerRightKey))
        {
            player.transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
    }
    else
    {
        isWalk = false;
    }
}



    #region "attacking"

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


    void CheckMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(player.transform.position);

        if (mousePosition.x < playerPosition.x)
            isMouseLeft = true;
        else
            isMouseLeft = false;
    }
    
    IEnumerator BasicAttack()
    {
        Quaternion startRotation = handPivot.transform.rotation;

        Quaternion targetRotation0 = startRotation * Quaternion.Euler(0f, 0f, 60);
        Quaternion targetRotation1 = startRotation * Quaternion.Euler(0f, 0f, -60);

        CheckMousePosition();

        if(isMouseLeft)
        {
            float elapsedTime = 0f;
            float rotationTime = Quaternion.Angle(startRotation, targetRotation1) / (attackSpeed/2);
            while (elapsedTime < rotationTime)
            {
                isAttack = true;

                float t = elapsedTime / rotationTime;
                handPivot.transform.rotation = Quaternion.Lerp(startRotation, targetRotation1, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            //
            elapsedTime = 0f;
            rotationTime = Quaternion.Angle(targetRotation1, targetRotation0) / attackSpeed;
            while (elapsedTime < rotationTime)
            {
                isAttack = true;

                float t = elapsedTime / rotationTime;
                handPivot.transform.rotation = Quaternion.Lerp(targetRotation1, targetRotation0, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            //
            elapsedTime = 0f;
            rotationTime = Quaternion.Angle(targetRotation0, startRotation) / attackSpeed;
            while (elapsedTime < rotationTime)
            {
                isAttack = true;

                float t = elapsedTime / rotationTime;
                handPivot.transform.rotation = Quaternion.Lerp(targetRotation0, startRotation, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            isAttack = false;
            
        }
        else
        {
            float elapsedTime = 0f;
            float rotationTime = Quaternion.Angle(startRotation, targetRotation0) / (attackSpeed/2);
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
            rotationTime = Quaternion.Angle(targetRotation0, targetRotation1) / attackSpeed;
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
            rotationTime = Quaternion.Angle(targetRotation1, startRotation) / attackSpeed;
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

    void AttackTimerStart()
    {
        canAttack = false;
        attackTimer = attackTerm;
    }    
    
    #endregion


    #region "animation"


    void CheckWalk()
    {
        
        if(isWalk == true)
            playerAnimator.SetBool("isWalking", true);

        else
            playerAnimator.SetBool("isWalking", false);
    }
    #endregion

}
