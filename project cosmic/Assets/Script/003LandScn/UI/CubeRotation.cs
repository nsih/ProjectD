using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CubeRotation : MonoBehaviour
{
    float throwForce = 3000;
    float throwDamping = 0.99f; // 회전 감속 계수
    float stopThreshold = 100.0f; // 회전 멈춤 기준값

    public bool isThrowing = false;
    public Vector3 throwDirection;
    private float currentThrowForce;



    public IEnumerator RotateCube(GameObject cube)
    {
        GameObject eye = cube.transform.GetChild(0).gameObject;

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
            Debug.Log("isThrowing");
            eye.GetComponent<TextMeshProUGUI>().text = "";

            // 회전 방향으로 힘을 가해 회전
            cube.transform.Rotate(throwDirection * currentThrowForce * Time.fixedDeltaTime);

            // 회전 감속
            currentThrowForce *= throwDamping;

            //Debug.Log( this.gameObject +"'s TF : "+ currentThrowForce);

            if (currentThrowForce <= stopThreshold) // 회전 힘이 기준값보다 작으면
            {
                Debug.Log("currentThrowForce <= stopThreshold");
                currentThrowForce = 0f;

                // 회전을 0, 0, 0으로 수렴시킴
                StartCoroutine(ConvergeRotation(cube));

                // 눈 보여주기
                eye.GetComponent<TextMeshProUGUI>().text = diceEye.ToString();

                yield return new WaitForSeconds(1.0f);


                //성공실패 결정
                if(diceEye >= 5)
                {
                    TestEventManager.isCurrentResultSuccess = true;
                    ActionManager.isCurrentResultSuccess = true;
                }

                isThrowing = false;
            }

            yield return null;
        }
    }

    IEnumerator ConvergeRotation(GameObject cube)
    {
        float convergeSpeed = 5f; // 조절 가능한 회전 수렴 속도

        while (Quaternion.Angle(cube.transform.rotation, Quaternion.identity) > 0.01f)
        {
            // 현재 회전 각도를 점진적으로 0, 0, 0으로 수렴시킴
            cube.gameObject.transform.rotation = Quaternion.Slerp(cube.transform.rotation, Quaternion.identity, convergeSpeed * Time.deltaTime);
            yield return null;
        }
        cube.transform.rotation = Quaternion.identity;
    }
}