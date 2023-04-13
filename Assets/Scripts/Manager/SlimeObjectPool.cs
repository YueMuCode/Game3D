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
       InitPool();//�����ɣ�����ģʽ
    }

    void InitPool()
    {
        GameObject slime = null;
        for(int i=0;i<initNum;i++)
        {
            slime = Instantiate(slimePrefabs);
            slime.SetActive(false);
            slimePool.Enqueue(slime);//���
        }
    }
    
    public GameObject GetSlime()
    {
        if(slimePool!=null&&slimePool.Count>0)
        {
            GameObject slime = slimePool.Dequeue();//����
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
        slimePool.Enqueue(slime);//�������
    }

}
