using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager :SingleT<GameManager>
{
    public CharacterStats playerStats;
    public CinemachineFreeLook cameraFollow;
    List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();//�����۲��ߡ�Ҳ����ÿ�����ˣ���ӵ��б���
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void RigisterPlayer(CharacterStats playerStats)//��ע���ʱ�����������ĸ���
    {
        this.playerStats = playerStats;
        cameraFollow = FindObjectOfType<CinemachineFreeLook>();
        if(cameraFollow)
        {
            cameraFollow.Follow = playerStats.GetComponent<PlayerController>().transform;
            cameraFollow.LookAt = playerStats.GetComponent<PlayerController>().transform;
        }
        
    }

    public void AddObserver(IEndGameObserver observer)//��Ҫ���۲��ߡ�������������������飨�������������ͽ�����۲��ߴ洢����
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
