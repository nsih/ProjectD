using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCon : MonoBehaviour
{
    float bulletSpeed;
    bool normalMoving;

    void Start() 
    {
        bulletSpeed = 25;
        normalMoving = true;
    }

    #region  "pooling"
    void OnEnable()
    {
        GameObject gunHead = GameObject.Find("GunHead");
        gameObject.transform.position = gunHead.transform.position;
        gameObject.transform.rotation = gunHead.transform.rotation;

        StartCoroutine(DeactivateAfterTime(3f));
    }
    IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    #endregion

    void FixedUpdate()
    {
        if(normalMoving)
        {
            NormalMoving();
        }
    }

    public void VanishOnCollision()
    {
        StartCoroutine(DeactivateAfterTime(0.01f));
    }
    

    void NormalMoving()
    {
        this.gameObject.transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }
}
