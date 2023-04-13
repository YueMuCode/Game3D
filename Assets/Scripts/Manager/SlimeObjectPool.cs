using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeObjectPool : SingleT<SlimeObjectPool>
{
    public GameObject slimePrefabs;
    public int initNum;
    [SerializeField]
    public Queue<GameObject> slimePool = new Queue<GameObject>();


    private void Start()
    {
       InitPool();//先生成，懒汉模式
    }

    void InitPool()
    {
        GameObject slime = null;
        for(int i=0;i<initNum;i++)
        {
            slime = Instantiate(slimePrefabs);
            slime.SetActive(false);
            slimePool.Enqueue(slime);//入队
        }
    }
    
    public GameObject GetSlime()
    {
        if(slimePool!=null&&slimePool.Count>0)
        {
            GameObject slime = slimePool.Dequeue();//出队
            slime.SetActive(true);
            return slime;
        }
        else
        {
            return Instantiate(slimePrefabs);
        }
    }
    public void RecoverSlime(GameObject slime)
    {
        slime.SetActive(false);
        slimePool.Enqueue(slime);//重新入队
    }

}
