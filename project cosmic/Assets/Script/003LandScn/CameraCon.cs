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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Vector3 playerPosition = player.transform.position;

        Vector3 targetPosition = (mousePosition + playerPosition) / 10f;
        targetPosition.z = transform.position.z;

        // 부드러운 이동을 위한 보간
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);

        transform.position = smoothPosition;
    }
}
