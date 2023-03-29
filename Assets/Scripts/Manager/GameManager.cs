using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager :SingleT<GameManager>
{
    public CharacterStats playerStats;
    public CinemachineFreeLook cameraFollow;
    List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();//将“观察者”也就是每个敌人，添加到列表当中
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void RigisterPlayer(CharacterStats playerStats)//再注册的时候进行摄像机的跟随
    {
        this.playerStats = playerStats;
        cameraFollow = FindObjectOfType<CinemachineFreeLook>();
        if(cameraFollow)
        {
            cameraFollow.Follow = playerStats.GetComponent<PlayerController>().transform;
            cameraFollow.LookAt = playerStats.GetComponent<PlayerController>().transform;
        }
        
    }

    public void AddObserver(IEndGameObserver observer)//当要“观察者”启动就立马订阅这件事情（人物死亡），就将这个观察者存储起来
    {
        endGameObservers.Add(observer);
    }
    public void RemoveObserver(IEndGameObserver observer)
    {
        endGameObservers.Remove(observer);
    }
    public void NotifyObserver()
    {
        foreach(var observer in endGameObservers)
        {
            observer.EndNotify();
        }
    }
}
