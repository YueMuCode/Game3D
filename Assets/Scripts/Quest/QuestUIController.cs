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
            //��ʾ��������
            SetupQuestList();
            Debug.Log("����tab");
            if (!isOpen)//�������ǹرյ�״̬�Ͱ���ʾҲ�ص�
            {
                tooltip.gameObject.SetActive(false);
            }
        }
    }
    public void SetupQuestList()
    {
       // Debug.Log("����tab");
        foreach (Transform item in questListTransform)
        {
            Destroy(item.gameObject);// Debug.Log("����tab1");
        }
        foreach (Transform item in rewardTransform)
        {
            Destroy(item.gameObject);// Debug.Log("����tab2");
        }
        foreach (Transform item in requireTransform)
        {
            Destroy(item.gameObject); //Debug.Log("����tab3");
        }
        
        foreach (var task in QuestManager.Instance.tasks)//���ٺ�Ѱ�ҽ��յ���������Ϣ������ָ����λ��
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
