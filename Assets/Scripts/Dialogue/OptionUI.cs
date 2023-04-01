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
           // Debug.Log("成功");
          //  Debug.Log("当前的index" + DialogueUIController.Instance.currentIndex);
            DialogueUIController.Instance.UpdateCurrentPiece(DialogueUIController.Instance.currentDialogueData.dialoguePieces[nextTargetID]);//这一段话这里就设置了，id必须从零开始设计\
            
        }
    }
}
