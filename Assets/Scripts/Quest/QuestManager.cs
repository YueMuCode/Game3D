using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class QuestManager : SingleT<QuestManager>
{
    [System.Serializable]
    public class QuestTask
    {
        public QuestData_SO questData;
        #region Is
        public bool isStarted
        {
            get
            {
                return questData.isStarted;
            }
            set
            {
                questData.isStarted = value;
            }
        }
        public bool isComplete
        {
            get
            {
                return questData.isComplete;
            }
            set
            {
                questData.isComplete = value;
            }
        }
        public bool isFinished
        {
            get
            {
                return questData.isFinished;
            }
            set
            {
                questData.isFinished = value;
            }
        }
        #endregion
    }

    public List<QuestTask> tasks = new List<QuestTask>();

    public bool HaveQuest(QuestData_SO data)//检查是否已经接受过任务，即检查当前接受的任务列表里面是否有这个任务
    {
        if (data != null)
        {
            return tasks.Any(q => q.questData.quesName == data.quesName);//用的linq的表达式，可以用循环实现就是查找tasks里面是否有questname
        }
        else
        {
            return false;
        }
    }
    public QuestTask GetTask(QuestData_SO data)
    {
        return tasks.Find(q => q.questData.quesName == data.quesName);//在已经接受的任务
    }
    //敌人死亡 寻找物品

    public void UpdateQuestProgress(string requireName, int amount)//更新任务的进度
    {
        foreach (var task in tasks)//可能两个任务都需要检测同一个物品

        {
            if (task.isFinished)
            {
                
                continue;//如果任务已经完成，则跳过本次
            }
            var matchTask = task.questData.questRequires.Find(r => r.name == requireName);
            if (matchTask != null)
            {
                matchTask.currentAmount += amount;
            }
            task.questData.CheckQuestProgress();
        }
    }
    private void Start()
    {
        LoadQuestManaget();
    }
    public void LoadQuestManaget()
    {
        var questCount = PlayerPrefs.GetInt("QuestCount");
        for (int i = 0; i < questCount; i++)
        {
            var newQuest = ScriptableObject.CreateInstance<QuestData_SO>();
            SaveManager.Instance.LoadData( "task" + i, newQuest);
            tasks.Add(new QuestTask { questData = newQuest });
        }
    }
    public void SaveQuestManager()
    {
        PlayerPrefs.SetInt("QuestCount", tasks.Count);
        for (int i = 0; i < tasks.Count; i++)
        {
            SaveManager.Instance.SaveCurrentData("task" + i, tasks[i].questData);
        }
    }

}
