using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    GameObject gameManager; //키정보
    GameObject playerInfo;  //player status
    GameObject player;
    GameObject playerHit;
    GameObject handPivot;
    GameObject sword;
    GameObject strikePivot;
    GameObject strike;

    Animator playerAnimator;



    public Sprite sword0;
    public Sprite sword1;


    bool isWalk;
    bool isDash;
    bool canAttack;
    bool isAttack;
    bool isMouseLeft;


    //for con
    float attackTimer;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("player");
        playerAnimator = this.gameObject.GetComponent<Animator>();


        playerHit = GameObject.Find("PlayerHit");
        handPivot = GameObject.Find("HandPivot");


        sword = GameObject.Find("sword");
        strikePivot = GameObject.Find("StrikePivot");
        strike = strikePivot.transform.GetChild(0).gameObject;


        isDash = false;

        canAttack = true;
        
        isAttack = false;
    }


    void Update()
    {
        CheckMousePosition();
        attackTimer -= Time.deltaTime;

        if(Input.GetKeyDown(InputData.attackKey) && !isAttack && canAttack)
        {
            AttackTimerStart();

            
            //StartCoroutine(StickAttack());
        }

        if(Input.GetKeyDown(InputData.dashKey) && !isAttack && !isDash)
        {
            Dash();
        }


        //animating
        CheckWalk();
        //


    
    }
    void FixedUpdate()
    {
        PlayerMovement();
        HandPivotCon();
        StrikePivotCon();
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
        if(!isDash)
        {
            if (Input.GetKey(InputData.moveDownKey) || Input.GetKey(InputData.moveLeftKey) || Input.GetKey(InputData.moveRightKey) || Input.GetKey(InputData.moveUpKey))
            {
                isWalk = true;

                if (Input.GetKey(InputData.moveUpKey))
                {
                    player.transform.Translate(Vector2.up * Time.deltaTime * PlayerInfo.speed);
                }

                if (Input.GetKey(InputData.moveLeftKey))
                {
                    player.transform.Translate(Vector2.left * Time.deltaTime * PlayerInfo.speed);
                }

                if (Input.GetKey(InputData.moveDownKey))
                {
                    player.transform.Translate(Vector2.down * Time.deltaTime * PlayerInfo.speed);
                }

                if (Input.GetKey(InputData.moveRightKey))
                {
                    player.transform.Translate(Vector2.right * Time.deltaTime * PlayerInfo.speed);
                }
            }
            else
            {
                isWalk = false;
            }
        }
    }

    void Dash()
    {
        if (!isDash && Time.time - PlayerInfo.lastDashTime >= PlayerInfo.dashCooldown)
        {
            Vector3 direction = Vector3.zero;

            // WASD 키 입력을 확인하여 대시 방향을 설정합니다.
            if (Input.GetKey(KeyCode.W))
                direction += Vector3.up;
            if (Input.GetKey(KeyCode.S))
                direction += Vector3.down;
            if (Input.GetKey(KeyCode.A))
                direction += Vector3.left;
            if (Input.GetKey(KeyCode.D))
                direction += Vector3.right;

            // 대각선 방향일 경우, 정규화된 벡터를 사용합니다.
            if (direction.magnitude > 1f)
                direction = direction.normalized;

            // 대시 방향이 설정되었을 때에만 대시를 수행합니다.
            if (direction != Vector3.zero)
            {
                StartCoroutine(DoDash(direction.normalized));
            }
        }
    }

    IEnumerator DoDash(Vector3 Direction)
    {
        playerHit.GetComponent<CircleCollider2D>().enabled = false;
        
        float timer = 0f;
        while (timer < 0.15f)
        {
            isDash = true;
            timer += Time.deltaTime;
            transform.position += Direction * PlayerInfo.speed * 5.0f * Time.deltaTime;
            yield return null;
        }
        isDash = false;
        playerHit.GetComponent<CircleCollider2D>().enabled = true;
        PlayerInfo.lastDashTime = Time.time; // 대쉬가 끝났을 때 마지막 대쉬 시간을 기록
    }



    #region "attacking"

    void HandPivotCon()    //
    {

        if(isMouseLeft)
        {
            handPivot.transform.position = new Vector3(-0.25f,-0.25f,0);
        }

        else
        {
            handPivot.transform.position = new Vector3(0.25f,-0.25f,0);
        }


        if(!isAttack)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector3 direction = mousePosition - handPivot.transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            handPivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void StrikePivotCon()
    {
        if(!isAttack)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector3 direction = mousePosition - strikePivot.transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            strikePivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

    void AttackTimerStart()
    {
        canAttack = false;
        attackTimer = PlayerInfo.attackDelay;
    }    


    //IEnumerator 


    //Stick swing
    public void StrikeCon()
    {
        
        if(isAttack && !strike.activeSelf)
        {
            strike.SetActive(true);
        }

        else if(strike.activeSelf)
        {
            strike.SetActive(false);
        }

    }


    IEnumerator StickAttack()
    {
        Quaternion startRotation = handPivot.transform.rotation;
        


        Quaternion targetRotation0 = startRotation * Quaternion.Euler(0f, 0f, -85);
        Quaternion targetRotation1 = startRotation * Quaternion.Euler(0f, 0f, 70);
        Quaternion targetRotation2 = startRotation * Quaternion.Euler(0f, 0f, 85);


        Quaternion targetRotation3 = startRotation * Quaternion.Euler(0f, 0f, 85);
        Quaternion targetRotation4 = startRotation * Quaternion.Euler(0f, 0f, -70);
        Quaternion targetRotation5 = startRotation * Quaternion.Euler(0f, 0f, -85);

        CheckMousePosition();

        if(isMouseLeft)
        {
            isAttack = true;
            StrikeCon();

            handPivot.transform.rotation = targetRotation0;
            yield return new WaitForSeconds(0.1f);

            handPivot.transform.rotation = targetRotation1;

            float elapsedTime = 0f;
            float rotationTime = Quaternion.Angle(targetRotation1, targetRotation2) / (1000/2);
            while (elapsedTime < rotationTime)
            {
                isAttack = true;

                float t = elapsedTime / rotationTime;
                handPivot.transform.rotation = Quaternion.Lerp(startRotation, targetRotation1, t);

                sword.GetComponent<SpriteRenderer>().sprite = sword1;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            sword.GetComponent<SpriteRenderer>().sprite = sword0;
            yield return new WaitForSeconds(0.15f);
            isAttack = false;
            StrikeCon();


            
           
            
        }
        

        else
        {
            isAttack = true;
            StrikeCon();

            handPivot.transform.rotation = targetRotation0;
            yield return new WaitForSeconds(0.1f);

            handPivot.transform.rotation = targetRotation1;

            float elapsedTime = 0f;
            float rotationTime = Quaternion.Angle(targetRotation1, targetRotation2) / (1000/2);
            while (elapsedTime < rotationTime)
            {
                isAttack = true;

                float t = elapsedTime / rotationTime;
                handPivot.transform.rotation = Quaternion.Lerp(startRotation, targetRotation1, t);

                sword.GetComponent<SpriteRenderer>().sprite = sword1;

                elapsedTime += Time.deltaTime;
                yield return null;
            }   

            sword.GetComponent<SpriteRenderer>().sprite = sword0;
            yield return new WaitForSeconds(0.15f);
            isAttack = false;
            StrikeCon();
        
        }
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
