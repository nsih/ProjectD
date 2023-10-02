using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputData : MonoBehaviour
{
    public static KeyCode pauseKey;
    public static KeyCode interactionKey;
    public static KeyCode mapKey;

    public static KeyCode attackKey;
    public static KeyCode dashKey;

    public static KeyCode moveUpKey;
    public static KeyCode moveDownKey;
    public static KeyCode moveLeftKey;
    public static KeyCode moveRightKey;



    void Start()
    {
        pauseKey = KeyCode.Escape;
        interactionKey = KeyCode.E;
        mapKey = KeyCode.Tab;

        attackKey = KeyCode.Mouse0;
        dashKey = KeyCode.Mouse1;

        moveUpKey = KeyCode.W;
        moveDownKey = KeyCode.S;
        moveLeftKey = KeyCode.A;
        moveRightKey = KeyCode.D;
    }

}
