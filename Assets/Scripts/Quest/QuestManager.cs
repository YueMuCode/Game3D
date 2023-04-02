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

    public bool HaveQuest(QuestData_SO data)
    {
        if (data != null)
        {
            return tasks.Any(q => q.questData.quesName == data.quesName);//�õ�linq�ı��ʽ�����Ǻ���⣬���ǿ�����ѭ��ʵ�־��ǲ���tasks�����Ƿ���questname
        }
        else
        {
            return false;
        }
    }
    public QuestTask GetTask(QuestData_SO data)
    {
        return tasks.Find(q => q.questData.quesName == data.quesName);//�ҵ���Ӧ��ֵ
    }
    //�������� Ѱ����Ʒ

    public void UpdateQuestProgress(string requireName, int amount)
    {
        foreach (var task in tasks)

        {
            if (task.isFinished)
            {
                
                continue;//��������Ѿ���ɣ�����������
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
