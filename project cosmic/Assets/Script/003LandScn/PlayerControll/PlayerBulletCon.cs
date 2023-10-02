using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCon : MonoBehaviour
{
    public float bulletSpeed = 25;
    public bool normalMoving = true;


    void OnEnable()
    {
        Invoke("DisablePooledObject", 3f);
    }

    void DisablePooledBullet()
    {
        gameObject.SetActive(false);
    }
    
    void Update()
    {
        if(normalMoving)
        {
            NormalMoving();
        }
    }
    

    void NormalMoving()
    {
        this.gameObject.transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
}
