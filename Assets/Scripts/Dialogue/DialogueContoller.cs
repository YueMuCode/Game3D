using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContoller : MonoBehaviour
{
    public DialogueData_so currentData;
    bool canTalk = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentData != null)
        {
            canTalk = true;
            Debug.Log("你进入了交互的范围");
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueUIController.Instance.dialoguePanelPrefab.SetActive(false);
        }
        canTalk = false;
    }
    void Update()
    {
        if (canTalk && Input.GetKeyDown(KeyCode.E))//按下鼠标R按键
        {
            OpenDialogue();
            //Debug.Log("按下了E建");
        }
    }


    void OpenDialogue()
    {
        //打开UI面板
        //传输对话内容信息

        DialogueUIController.Instance.UpdateDialogueData(currentData);
        DialogueUIController.Instance.UpdateCurrentPiece(currentData.dialoguePieces[0]);
    }
}
