using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    GameObject player;

    Transform target;


    float cameraSpeed;


    void Start()
    {
        player = GameObject.Find("player");

        cameraSpeed = 5;
    }

    void FixedUpdate()
    {
        Chasing();
    }

    void Chasing()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 cameraPos = this.transform.position;

        Vector2 targetPos = playerPos - cameraPos;

        // 부드러운 이동을 위한 보간
        Vector2 smoothPos = Vector2.Lerp(Vector2.zero, targetPos, cameraSpeed * Time.deltaTime);

        this.gameObject.transform.Translate(smoothPos);

    }
}
