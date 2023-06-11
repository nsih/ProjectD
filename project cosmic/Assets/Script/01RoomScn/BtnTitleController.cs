using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnTitleController : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("ScnTitle");
    }
}
