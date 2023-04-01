using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueContoller))]
public class QuestGiver : MonoBehaviour
{
    DialogueContoller controller;
    QuestData_SO currentQuest;
    public DialogueData_so startDialogue;
    public DialogueData_so progressDialogue;
    public DialogueData_so completeDialogue;
    public DialogueData_so finishDialogue;

    #region 获取任务的进行状态
    public bool IsStarted
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(currentQuest))
            {
                return QuestManager.Instance.GetTask(currentQuest).isStarted;
            }
            else
            {
                return false;
            }
        }
    }
    public bool IsComplete
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(currentQuest))
            {
                return QuestManager.Instance.GetTask(currentQuest).isComplete;
            }
            else
            {
                return false;
            }
        }
    }
    public bool IsFinished
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(currentQuest))
            {
                return QuestManager.Instance.GetTask(currentQuest).isFinished;
            }
            else
            {
                return false;
            }
        }
    }
    #endregion
    void Awake()
    {
        controller = GetComponent<DialogueContoller>();
    }
    void Start()
    {
        controller.currentData = startDialogue;
        currentQuest = controller.currentData.GetQuest();
    }

    void Update()
    {
        if (IsStarted)
        {
            if (IsComplete)
            {
                controller.currentData = completeDialogue;
            }
            else
            {
                controller.currentData = progressDialogue;
            }
        }

        if (IsFinished)
        {
            controller.currentData = finishDialogue;
        }
    }
}
