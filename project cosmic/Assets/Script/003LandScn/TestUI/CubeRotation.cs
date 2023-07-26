using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class CubeRotation : MonoBehaviour
{
    public GameObject eye;




    private Quaternion initialRotation;
    private float currentTime = 0f;
    private bool isRotating = false;

    private void Start()
    {
        eye = gameObject.transform.GetChild(0).gameObject;

        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            isRotating = true;
            StartCoroutine(RotateCubeCoroutine());
        }
    }

    public IEnumerator RotateCubeCoroutine()
    {
        // 1초 동안 무작위로 회전
        float randomTime = 1f;
        while (currentTime < randomTime)
        {
            eye.GetComponent<TextMeshProUGUI>().text = "?";


            Vector3 randomAxis = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f)).normalized;
            float randomSpeed = Random.Range(100f, 200f);

            float angle = randomSpeed * Time.deltaTime;
            transform.Rotate(randomAxis * angle, Space.Self);

            currentTime += Time.deltaTime;
            yield return null;
        }

        //float returnTime = 2f;
        while (currentTime >= 0f )
        {
            eye.GetComponent<TextMeshProUGUI>().text = "";

            Vector3 targetEulerAngles = initialRotation.eulerAngles;
            Vector3 currentEulerAngles = transform.rotation.eulerAngles;
            Vector3 diff = targetEulerAngles - currentEulerAngles;

            transform.Rotate(diff * Time.deltaTime * 2.0f, Space.Self);

            currentTime -= Time.deltaTime;
            yield return null;
        }

        // 초기 회전 상태로 돌아온 후 초기화
        transform.rotation = initialRotation;
        currentTime = 0f;
        isRotating = false;

        eye.GetComponent<TextMeshProUGUI>().text = "0";
    }
}