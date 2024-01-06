using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCon : MonoBehaviour
{

    float bulletSpeed;

    //총알움직임 모드
    bool normalMode;    //한 방향으로 움직임

    void Start() 
    {
        bulletSpeed = 35;
        normalMode = true;
    }

    void OnEnable()
    {
        StartCoroutine(DeactivateAfterTime(3f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(normalMode)
        {
            NormalMoving();
        }
    }


    public void GetEnemyBulletMode(EnemyBulletType enemyBulletType)
    {
        normalMode = false;

        if(enemyBulletType == EnemyBulletType.NORMAL)
        {
            normalMode = true;
        }

        else
        {
            Debug.Log("??");
        }

    }

    IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !PlayerInfo.isInvincible)
        {
            other.gameObject.GetComponent<PlayerManager>().PlayerAttacked();

            gameObject.SetActive(false);
            Debug.Log("Player Hitting");
        }

        if(other.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }

        
    }

    public void VanishOnCollision()
    {
        StartCoroutine(DeactivateAfterTime(0.01f));
    }
    

    void NormalMoving()
    {
        this.gameObject.transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }
}

public enum EnemyBulletType
{
    NORMAL
}
