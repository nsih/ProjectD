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
        Vector3 playerPosition = player.transform.position;
        Vector3 targetPosition = playerPosition;

        // 마우스 위치와 플레이어 사이의 벡터
        Vector3 mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerPosition;

        // Z 축 값을 유지하여 플레이어 중심 위치에 마우스 벡터의 10%를 더함
        targetPosition += new Vector3(mouseOffset.x * 0.1f, mouseOffset.y * 0.1f, 0f);

        
        // Z 축 값을 유지하여 카메라 위치 설정
        targetPosition.z = transform.position.z;
        transform.position = targetPosition;
    }
}
