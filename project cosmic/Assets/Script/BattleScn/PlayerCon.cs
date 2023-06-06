using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    GameObject player;

    public GameObject bullet;

    float speed;

    Vector2 bufVec = new Vector2();

    void Start()
    {
        
        player = GameObject.Find("player");
        speed = 3.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        OnPlayerConnected();
    }


    private void OnPlayerConnected() 
    {
            if (Input.GetKey(KeyCode.UpArrow))
            player.transform.Translate(Vector2.up * Time.deltaTime*speed);

            if (Input.GetKey(KeyCode.LeftArrow))
                player.transform.Translate(Vector2.left * Time.deltaTime*speed);

            if (Input.GetKey(KeyCode.DownArrow))
                player.transform.Translate(Vector2.down * Time.deltaTime *speed);

            if (Input.GetKey(KeyCode.RightArrow))
                player.transform.Translate(Vector2.right * Time.deltaTime*speed);

            if (Input.GetKeyDown(KeyCode.E))
            {
                bufVec = this.gameObject.transform.position;
                Instantiate(bullet,bufVec,Quaternion.identity);
            }
    }
}
