using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CubeRotation : MonoBehaviour
{
    public GameObject eye;



    public float throwForce = 1000;
    public float throwDamping = 0.99f; // 회전 감속 계수
    public float stopThreshold = 50.0f; // 회전 멈춤 기준값

    private bool isThrowing = false;
    private Vector3 throwDirection;
    private float currentThrowForce;


    void Start()
    {
        eye = gameObject.transform.GetChild(0).gameObject;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isThrowing) // 마우스 왼쪽 버튼을 누르면서 회전 중이 아닐 때
        {
            // 회전 코루틴 시작
            StartCoroutine(RotateCube());
        }
    }

    IEnumerator RotateCube()
    {
        isThrowing = true;

        // 랜덤한 회전 방향 생성
        float throwX = Random.Range(-360f, 360f);
        float throwY = Random.Range(-360f, 360f);
        float throwZ = Random.Range(-360f, 360f);

        throwDirection = new Vector3(throwX, throwY, throwZ).normalized;

        // 초기 회전 힘 설정
        currentThrowForce = throwForce;
        
        while (isThrowing)
        {
            //눈 ?
            eye.GetComponent<TextMeshProUGUI>().text = "";


            // 회전 방향으로 힘을 가해 회전
            transform.Rotate(throwDirection * currentThrowForce * Time.deltaTime);

            // 회전 감속
            currentThrowForce *= throwDamping;

            if (currentThrowForce <= stopThreshold) // 회전 힘이 기준값보다 작으면
            {
                isThrowing = false;
                currentThrowForce = 0f;

                // 회전을 명확히 0으로 맞춰줌
                transform.rotation = Quaternion.identity;


                //눈 보여주기
                //if 숫자가 5,6이면 색도 다르게 할수 잊지 아늘까??????
                eye.GetComponent<TextMeshProUGUI>().text = "0"; //
            }

            yield return null;
        }
    }
}