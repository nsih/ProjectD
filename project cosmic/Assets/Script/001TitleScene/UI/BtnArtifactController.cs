using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnArtifactController : MonoBehaviour
{
    GameObject pnlBackGround;
    GameObject ArtifactView;

    private void Awake() 
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        ArtifactView = pnlBackGround.transform.Find("RitualPopup").gameObject;
    }
    public void EnableClick()
    {
        ArtifactView.SetActive(true);
    }

    public void DisableClick()
    {
        ArtifactView.SetActive(false);
    }
}