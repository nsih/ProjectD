using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueTest : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    void Start()
    {
        dialogueRunner = GameObject.Find("DialogueRunner").GetComponent<DialogueRunner>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            dialogueRunner.StartDialogue("NewYarnScript");

            Debug.Log("y");
        }
    }
}
