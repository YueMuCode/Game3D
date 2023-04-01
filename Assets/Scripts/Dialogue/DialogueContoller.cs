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
            Debug.Log("������˽����ķ�Χ");
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
        if (canTalk && Input.GetKeyDown(KeyCode.E))//�������R����
        {
            OpenDialogue();
            //Debug.Log("������E��");
        }
    }


    void OpenDialogue()
    {
        //��UI���
        //����Ի�������Ϣ

        DialogueUIController.Instance.UpdateDialogueData(currentData);
        DialogueUIController.Instance.UpdateCurrentPiece(currentData.dialoguePieces[0]);
    }
}
