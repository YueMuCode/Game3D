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
        public string name;//��Ҫ����Ʒ������
        public int requireAmount;
        public int currentAmount;
    }
    //��������ƺ�����
    public string quesName;
    [TextArea]
    public string description;
    //�����״̬
    public bool isStarted;
    public bool isComplete;
    public bool isFinished;
    public List<QuestRequire> questRequires = new List<QuestRequire>();
     public List<ItemsInInventory> rewards = new List<ItemsInInventory>();
    public void CheckQuestProgress()
    {
        var finishRequires = questRequires.Where(r => r.requireAmount <= r.currentAmount);
        isComplete = finishRequires.Count() == questRequires.Count;//����ǲ�������������������Ѿ����
        if (isComplete)
        {
            Debug.Log("�������");
        }
    }
    //��ǰ������Ҫ �ռ�/���� ��Ŀ�������б�
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
                //��Ҫ������NPC����Ʒ��Ŀ��������Ϊ����ֵ��
                int requireCount = Mathf.Abs(reward.amount);
                //�����������Ʒ��Ϊ�գ���ô�������������������һ���ǹ���ȥ��Ʒ������һ���ǲ���
                if (InventotyManager.Instance.QuestItemInBag(reward.itemData) != null)
                {
                    //�����е���Ʒ��ĿС�ڻ��ߵ��ڽ�����Ŀ
                    if (InventotyManager.Instance.QuestItemInBag(reward.itemData).amount <= requireCount)//Ӧ�ò��ܵ���
                    {
                        requireCount -= InventotyManager.Instance.QuestItemInBag(reward.itemData).amount;
                        InventotyManager.Instance.QuestItemInBag(reward.itemData).amount = 0;
                        //���ǡ�õ��ڣ�˵����ʱ��Ʒ����û����Ʒ�ˣ���Ҫ�Ƚ����ж�
                        if (InventotyManager.Instance.QuestItemInAction(reward.itemData) != null)
                        {
                            InventotyManager.Instance.QuestItemInAction(reward.itemData).amount -= requireCount;
                        }
                    }
                    //�����е���Ŀ������Ҫ��������Ŀ
                    else
                    {
                        InventotyManager.Instance.QuestItemInBag(reward.itemData).amount -= requireCount;
                    }
                }
                else//����Ϊ��ֱ�Ӵ���Ʒ�������ȥ����
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