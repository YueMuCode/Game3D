using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestUIController : SingleT<QuestUIController>
{
    [Header("Elements")]
    public GameObject questPanel;
    public ItemToolTip tooltip;
    bool isOpen;

    [Header("Quest Name")]
    public RectTransform questListTransform;
    public QuestNameButton questNameButton;

    [Header("Text Content")]
    public Text questContentText;

    [Header("Requirement")]
    public RectTransform requireTransform;
    public QuestRequirment requirement;

    [Header("Reward Panel")]
    public RectTransform rewardTransform;
    public SlotItemUI rewardUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            questPanel.SetActive(isOpen);
           
            questContentText.text = string.Empty;
            //显示面板的内容
            SetupQuestList();
            Debug.Log("按下tab");
            if (!isOpen)//如果面板是关闭的状态就把提示也关掉
            {
                tooltip.gameObject.SetActive(false);
            }
        }
    }
    public void SetupQuestList()
    {
       // Debug.Log("按下tab");
        foreach (Transform item in questListTransform)
        {
            Destroy(item.gameObject);// Debug.Log("按下tab1");
        }
        foreach (Transform item in rewardTransform)
        {
            Destroy(item.gameObject);// Debug.Log("按下tab2");
        }
        foreach (Transform item in requireTransform)
        {
            Destroy(item.gameObject); //Debug.Log("按下tab3");
        }
        
        foreach (var task in QuestManager.Instance.tasks)//销毁后寻找接收到的任务信息生成在指定的位置
        {
            var newTask = Instantiate(questNameButton, questListTransform);
            newTask.SetupNameButton(task.questData);
            newTask.questDescription= questContentText;
        }


    }
    public void SetupRequireList(QuestData_SO questData)
    {
       // questContentText.text = questData.description;
        foreach (Transform item in requireTransform)
        {
            Destroy(item.gameObject);
        }
        foreach (var require in questData.questRequires)
        {
            var q = Instantiate(requirement, requireTransform);
            if (questData.isFinished)
            {
                q.SetupRequiremennt(require.name, true);
            }
            else
            {
                q.SetupRequirement(require.name, require.requireAmount, require.currentAmount);
            }



        }
    }
    public void SetupRewardItem(ItemData_SO itemData, int amount)
    {
        //if (rewardUI == null) Debug.Log("null");
        var item = Instantiate(rewardUI, rewardTransform);
        item.SetActiveItemUI(itemData, amount);
    }
}
