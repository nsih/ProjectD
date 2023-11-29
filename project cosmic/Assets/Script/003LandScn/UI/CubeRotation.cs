using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CubeRotation : MonoBehaviour
{
    public GameObject eye;



    public float throwForce = 1;
    public float throwDamping = 0.5f; // 회전 감속 계수
    public float stopThreshold = 30.0f; // 회전 멈춤 기준값

    public bool isThrowing = false;
    public Vector3 throwDirection;
    private float currentThrowForce;


    void Start()
    {
        eye = gameObject.transform.GetChild(0).gameObject;
    }


    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0) && !isThrowing) // 마우스 왼쪽 버튼을 누르면서 회전 중이 아닐 때
        {
            // 회전 코루틴 시작
            StartCoroutine(RotateCube());
        }
        */
    }

    public IEnumerator RotateCube()
    {
        // 랜덤한 회전 방향 생성
        float throwX = new float();//(Random.value < 0.5f) ? -360f : 360f;
        float throwY = new float();//(Random.value < 0.5f) ? -360f : 360f;
        float throwZ = new float();//(Random.value < 0.5f) ? -360f : 360f;

        throwX =  Random.Range(-360f, 360f);
        throwY =  Random.Range(-360f, 360f);
        throwZ = Random.Range(-360f, 360f);

        int diceEye = Random.Range(1, 7);

        throwDirection = new Vector3(throwX, throwY, throwZ).normalized;

        // 초기 회전 힘 설정
        currentThrowForce = throwForce;
        
        isThrowing = true;

        while (isThrowing)
        {
            //눈 ?
            
            eye.GetComponent<TextMeshProUGUI>().text = "";

            // 회전 방향으로 힘을 가해 회전
            this.gameObject.transform.Rotate(throwDirection * currentThrowForce * Time.fixedDeltaTime);

            // 회전 감속
            currentThrowForce *= throwDamping;

            if (currentThrowForce <= stopThreshold) // 회전 힘이 기준값보다 작으면
            {
                currentThrowForce = 0f;

                // 회전을 0, 0, 0으로 수렴시킴
                StartCoroutine(ConvergeRotation());

                // 눈 보여주기
                eye.GetComponent<TextMeshProUGUI>().text = diceEye.ToString();

                yield return new WaitForSeconds(0.5f);

                isThrowing = false;
            }

            yield return null;
        }
    }

    IEnumerator ConvergeRotation()
    {
        float convergeSpeed = 5f; // 조절 가능한 회전 수렴 속도

        while (transform.rotation != Quaternion.identity)
        {
            // 현재 회전 각도를 점진적으로 0, 0, 0으로 수렴시킴
            this.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, convergeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}