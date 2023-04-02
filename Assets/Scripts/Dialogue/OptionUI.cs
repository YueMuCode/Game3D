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
    private bool takeQuest;
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
        takeQuest = option.isTakeQuest;
    }
    public void OnOptionClicked()
    {
       if(currentPiece.questdata!=null)
        {
            var tasksQuestData = new QuestManager.QuestTask();
            tasksQuestData.questData = Instantiate(currentPiece.questdata);//类似模板


            if (takeQuest)//选择接受了任务
            {
                if(QuestManager.Instance.HaveQuest(tasksQuestData.questData))
                {
                    //任务已经接受过，检测是否完成
                    if (QuestManager.Instance.GetTask(tasksQuestData.questData).isComplete)
                    {
                        tasksQuestData.questData.GiveRewards(); ;
                        GameManager.Instance.playerStats.currentExp += 30;
                        QuestManager.Instance.GetTask(tasksQuestData.questData).isFinished = true;
                    }
                }
                else//任务还没有接受过，接受
                {
                   //为什么不能 tasksQuestData.isStarted=true?
                    QuestManager.Instance.tasks.Add(tasksQuestData);
                    QuestManager.Instance.GetTask(tasksQuestData.questData).isStarted = true;//将放进去的任务的状态改变为什么不能直接在前面加
                    foreach (var requireItem in tasksQuestData.questData.RequireTargetName())//直接查找任务物品是否在背包里面已经有了
                    {
                        InventotyManager.Instance.CheckQuestItemInBag(requireItem);
                    }
                }
            }
        }


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
