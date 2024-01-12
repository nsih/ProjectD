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
        targetPosition += new Vector3(mouseOffset.x * 0.1f, mouseOffset.y * 0.1f, -10f);


        // 부드러운 이동을 위한 보간
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        
        // Z 축 값을 유지하여 카메라 위치 설정
        smoothPosition.z = transform.position.z;
        //transform.position = smoothPosition;
        transform.position = targetPosition;
    }

    
    public void ShakeCamera(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // 마우스 방향으로 일정 정도만 이동
            Vector3 mouseDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            Vector3 targetPosition = transform.position + mouseDirection * magnitude;

            Vector3 targetPosition2 = new Vector3(targetPosition.x, targetPosition.y, -10 );

            // 원래 위치에서 마우스 방향으로 이동한 후 다시 돌아오는 보간
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(transform.position, targetPosition2, t);

            elapsed += Time.deltaTime;

            yield return null;
        }
    }
}
