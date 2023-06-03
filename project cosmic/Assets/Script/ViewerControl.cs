using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewerControl : MonoBehaviour
{
    GameObject txtViewer;
    int viwerCount = 5695214;
    bool isCoroutineRunning = false;
    
    void Start()
    {
        txtViewer = GameObject.Find("txtViewer");
    }
    // Update is called once per frame
    void Update()
    {
        if (!isCoroutineRunning && viwerCount < 999999999)
        {
            ViewerChange();
        }

        txtViewer.GetComponent<TextMeshProUGUI>().text = "Live  " + viwerCount.ToString();
    }

    public void ViewerChange()
    {
        StartCoroutine(ViewerChangeCoroutine());
    }

    private System.Collections.IEnumerator ViewerChangeCoroutine()
    {
        int changed = Random.Range(viwerCount-500,viwerCount+800);
        isCoroutineRunning = true;

        if(changed == viwerCount)
        {
            isCoroutineRunning = false;
            yield break;
        }
        

        else
        {
            yield return new WaitForSeconds(3f);

            while (viwerCount != changed)
            {
               if (viwerCount < changed)
                    viwerCount++;

                else if(viwerCount > changed)
                 viwerCount--;


                yield return new WaitForSeconds(0.003f);
            }
            isCoroutineRunning = false;
            yield break;
        }
    }
}
