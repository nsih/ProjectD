using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon : MonoBehaviour
{
    GameObject gameManager;
    GameObject player;


    public EnemyData enemyData;

    private CircleCollider2D circleCollider;



    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("player");        
    }

    void OnEnable()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = enemyData.enemySprite;
    }

    void OnDisable() 
    {
        if(gameManager.GetComponent<BattleEventManager>().isPoolAllDisable() == true)
        {
            gameManager.GetComponent<StageManager>().EndRoomEventPhase(StageManager.map[ GameManager.currentRoom ].roomType);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider == player.GetComponent<Collider2D>() )
        {
            this.gameObject.SetActive(false);
        }
    }


    void AddCircleColliderToGameObject(float radius, Vector2 offset)
    {
        circleCollider = gameObject.AddComponent<CircleCollider2D>();

        circleCollider.radius = radius;

        circleCollider.offset = offset;
    }

    void RemoveCircleColliderFromGameObject()
    {
        // 원 콜라이더 컴포넌트 제거
        if (circleCollider != null)
        {
            Destroy(circleCollider);
        }
    }


    void Moving() 
    {

    }
}
