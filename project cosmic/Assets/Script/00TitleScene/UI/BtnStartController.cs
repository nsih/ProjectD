using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnStartController : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("ScnRoom1");
    }
}
