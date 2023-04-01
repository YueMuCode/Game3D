using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest Data")]
public class QuestData_SO : ScriptableObject
{
    [System.Serializable]
    public class QuestRequire
    {
        public string name;//需要的物品的名称
        public int requireAmount;
        public int currentAmount;
    }
    //任务的名称和描述
    public string quesName;
    [TextArea]
    public string description;
    //任务的状态
    public bool isStarted;
    public bool isComplete;
    public bool isFinished;
    public List<QuestRequire> questRequires = new List<QuestRequire>();
     public List<ItemsInInventory> rewards = new List<ItemsInInventory>();
    public void CheckQuestProgress()
    {
        var finishRequires = questRequires.Where(r => r.requireAmount <= r.currentAmount);
        isComplete = finishRequires.Count() == questRequires.Count;//检测是不是任务的所有条件都已经完成
        if (isComplete)
        {
            Debug.Log("任务完成");
        }
    }
    //当前任务需要 收集/消灭 的目标名字列表
    public List<string> RequireTargetName()
    {
        List<string> targetNameList = new List<string>();
        foreach (var require in questRequires)
        {
            targetNameList.Add(require.name);
        }
        return targetNameList;
    }
    public void GiveRewards()
    {
        foreach (var reward in rewards)
        {
            if (reward.amount < 0)
            {
                //需要交付给NPC的物品数目（先设置为绝对值）
                int requireCount = Mathf.Abs(reward.amount);
                //如果背包的物品不为空，那么背包里面有两种情况，一种是够减去物品数量，一种是不够
                if (InventotyManager.Instance.QuestItemInBag(reward.itemData) != null)
                {
                    //背包中的物品数目小于或者等于交付数目
                    if (InventotyManager.Instance.QuestItemInBag(reward.itemData).amount <= requireCount)//应该不能等于
                    {
                        requireCount -= InventotyManager.Instance.QuestItemInBag(reward.itemData).amount;
                        InventotyManager.Instance.QuestItemInBag(reward.itemData).amount = 0;
                        //如果恰好等于，说明这时物品栏中没有物品了，需要先进行判断
                        if (InventotyManager.Instance.QuestItemInAction(reward.itemData) != null)
                        {
                            InventotyManager.Instance.QuestItemInAction(reward.itemData).amount -= requireCount;
                        }
                    }
                    //背包中的数目大于需要交付的数目
                    else
                    {
                        InventotyManager.Instance.QuestItemInBag(reward.itemData).amount -= requireCount;
                    }
                }
                else//背包为空直接从物品栏里面减去交付
                {
                    InventotyManager.Instance.QuestItemInAction(reward.itemData).amount -= requireCount;
                }
            }

            else
            {
                InventotyManager.Instance.AddItemToInventory(reward.itemData, reward.amount);
            }
            InventotyManager.Instance.bagContainer.UpdateEverySlotItemUI();
            InventotyManager.Instance.actionContainer.UpdateEverySlotItemUI();
        }
    }
}