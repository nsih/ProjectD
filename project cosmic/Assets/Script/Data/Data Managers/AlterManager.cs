using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AlterManager : MonoBehaviour
{
    public List<AlterData> alterDatas = new();


    //interaction
    GameObject gameManager;
    GameObject landUICanvas;
    GameObject dialogueBox;
    GameObject dialogueTxt;
    GameObject dialogueOption;

    GameObject rewardAfterPopup;

    //bool isTyping;


    public AlterData GetAlterData()
    {
        int selectedIndex = 0;

        do
        {
            selectedIndex = UnityEngine.Random.Range(0, alterDatas.Count);
        } while (alterDatas[selectedIndex].isUsed == true); //true 아니면 ㄱㄱ

        alterDatas[selectedIndex].isUsed = true;

        return alterDatas[selectedIndex];
    }


    public void AlterInteraction(AlterData alterData)
    {
        GameManager.isLandTalking = true;


        gameManager = GameObject.Find("GameManager");

        landUICanvas = GameObject.Find("LandUICanvas");
        dialogueBox = landUICanvas.transform.Find("DialogueBox").gameObject;
        dialogueTxt = dialogueBox.transform.Find("DialogueText").gameObject;
        dialogueOption = GameObject.Find("DialogueOption");

        rewardAfterPopup = landUICanvas.transform.Find("RewardAfterPopup").gameObject;


        typingCoroutine = StartCoroutine(TypeText(alterData));
    }

    #region  "UI Con"
    private Coroutine typingCoroutine;

    private IEnumerator TypeText(AlterData alterData)
    {
        //isTyping = true;
        dialogueBox.SetActive(true);

        dialogueTxt.GetComponent<TextMeshProUGUI>().text = "";

        foreach (char c in alterData.alterText)
        {
            dialogueTxt.GetComponent<TextMeshProUGUI>().text += c;
            gameManager.GetComponent<SFXManager>().PlaySound(SfxType.DialogueTyping);

            yield return new WaitForSeconds(0.05f);
        }

        //isTyping = false;

        GameObject option1 = dialogueOption.transform.GetChild(0).gameObject;
        GameObject option2 = dialogueOption.transform.GetChild(1).gameObject;

        option1.SetActive(true);
        option2.SetActive(true);

        option1.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "제단에 기도한다.";
        option2.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "그만둔다";

        option1.GetComponent<Button>().onClick.AddListener(() => GetAlterEffect(alterData));
        option2.GetComponent<Button>().onClick.AddListener(() => EndAlterInteraction(alterData));
        yield break;
    }

    void GetAlterEffect(AlterData alterData)
    {
        //결과 적용
        this.gameObject.GetComponent<PlayerInfo>().OutcomeOffsetApply(alterData.outcomeOffset);


        //afterword
        rewardAfterPopup.transform.Find("Image").GetComponent<Image>().sprite = alterData.alterSprite;
        rewardAfterPopup.transform.Find("Text").GetComponent<TMP_Text>().text = alterData.afterWord;
        rewardAfterPopup.SetActive(true);

        EndAlterInteraction(alterData);
    }

    void EndAlterInteraction(AlterData alterData)
    {
        //옵션 UI 비활성화, listener 삭제
        dialogueOption.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        dialogueOption.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();

        dialogueOption.transform.GetChild(0).gameObject.SetActive(false);
        dialogueOption.transform.GetChild(1).gameObject.SetActive(false);

        //dialogue box disable
        dialogueTxt.GetComponent<TextMeshProUGUI>().text = "";
        dialogueBox.SetActive(false);

        GameManager.isLandTalking = false;


        Invoke( "CloseAfterRewardPopup" , 3f);

    }

    public void CloseAfterRewardPopup()
    {
        rewardAfterPopup.transform.Find("Image").GetComponent<Image>().sprite = null;
        rewardAfterPopup.transform.Find("Text").GetComponent<TMP_Text>().text = null;

        rewardAfterPopup.SetActive(false);
    }

    #endregion

}
