using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        this.gameObject.transform.Translate(Vector2.up * Time.deltaTime *10);
    }
}
