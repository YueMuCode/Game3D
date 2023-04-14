using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestNameButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Text questNameText;
    public QuestData_SO currentQuestData;
    public Text questDescription;
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(UpdateQuestContent);
    }
    void UpdateQuestContent()
    {
        questDescription.text = currentQuestData.description;
        QuestUIController.Instance.SetupRequireList(currentQuestData);

        foreach (Transform item in QuestUIController.Instance.rewardTransform)//点击按钮，销毁上一个任务的奖励物品显示
        {
            Destroy(item.gameObject);
        }

        foreach (var item in currentQuestData.rewards)//更新奖励的物品列表
        {
           
            QuestUIController.Instance.SetupRewardItem(item.itemData, item.amount);
        }
    }
    public void SetupNameButton(QuestData_SO questData)
    {

        currentQuestData = questData;
        if (questData.isComplete)
        {
            questNameText.text = questData.quesName + "(任务已经完成)";

        }
        else
        {
            questNameText.text = questData.quesName;
        }
    }
}
