using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionUI : MonoBehaviour
{
    public Text optionText;
    private Button thisButton;
    private DialoguePiece currentPiece;
    private int nextTargetID;
    private void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnOptionClicked);
    }
    public void UpdateOption(DialoguePiece piece,DialogueOptions option)
    {
        currentPiece = piece;
        optionText.text = option.text;
        nextTargetID = option.targetPieceID;
    }
    public void OnOptionClicked()
    {
        if(nextTargetID<0)
        {
            DialogueUIController.Instance.dialoguePanelPrefab.SetActive(false);
            return;
        }
        else
        {
            DialogueUIController.Instance.currentIndex = nextTargetID;
           // Debug.Log("�ɹ�");
          //  Debug.Log("��ǰ��index" + DialogueUIController.Instance.currentIndex);
            DialogueUIController.Instance.UpdateCurrentPiece(DialogueUIController.Instance.currentDialogueData.dialoguePieces[nextTargetID]);//��һ�λ�����������ˣ�id������㿪ʼ���\
            
        }
    }
}
