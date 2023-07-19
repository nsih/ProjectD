using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArchiveBtnCon : MonoBehaviour
{
    GameObject pnlBackGround;
    GameObject archivePopup;

    Button btnClose;


    void Start()
    {
        pnlBackGround = GameObject.Find("PnlBackGround");
        archivePopup = pnlBackGround.transform.Find("ArchivePopup").gameObject;

        btnClose = archivePopup.transform.Find("BtnClose").gameObject.GetComponent<Button>();
    }

    public void OnclickArchive()
    {
        OpenPopup();
    }


    //
    void OpenPopup()
    {
        archivePopup.SetActive(true);
        btnClose.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        archivePopup.SetActive(false);
        btnClose.onClick.RemoveAllListeners();
    }


}
