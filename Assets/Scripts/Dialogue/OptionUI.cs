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
            var tasksQuestData = new QuestManager.QuestTask();//��������������QuestData_so,��manager����洢�Ѿ����ܵ������õ�������QuestTask������ڲ������QuestData_so����Ҫ��ת��
            tasksQuestData.questData = Instantiate(currentPiece.questdata);//����ģ��


            if (takeQuest)//ѡ�����������
            {
                if(QuestManager.Instance.HaveQuest(tasksQuestData.questData))
                {
                    //�����Ѿ����ܹ�������Ƿ����
                    if (QuestManager.Instance.GetTask(tasksQuestData.questData).isComplete)
                    {
                        tasksQuestData.questData.GiveRewards(); ;
                        GameManager.Instance.playerStats.currentExp += 30;
                        QuestManager.Instance.GetTask(tasksQuestData.questData).isFinished = true;
                    }
                }
                else//����û�н��ܹ�������
                {
                   //Ϊʲô���� tasksQuestData.isStarted=true?
                    QuestManager.Instance.tasks.Add(tasksQuestData);
                    QuestManager.Instance.GetTask(tasksQuestData.questData).isStarted = true;
                    foreach (var requireItem in tasksQuestData.questData.RequireTargetName())//ֱ�Ӳ���ÿһ��Ҫ��������Ʒ�Ƿ��ڱ��������Ѿ�����
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
           // Debug.Log("�ɹ�");
          //  Debug.Log("��ǰ��index" + DialogueUIController.Instance.currentIndex);
            DialogueUIController.Instance.UpdateCurrentPiece(DialogueUIController.Instance.currentDialogueData.dialoguePieces[nextTargetID]);//��һ�λ�����������ˣ�id������㿪ʼ���\
            
        }
    }
}
