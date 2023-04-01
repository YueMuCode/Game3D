using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    [System.Serializable]//��������ܹ���ʾ
   public class TheDropItem//�������ɽṹ��
    {
        public GameObject itemPrefabs;
        [Range(0,1)]
        public float randomValue;
    }
    public TheDropItem [] mayDropItemArrs;
    public void  BeginCheckIsDrpoItem()
    {
        float value = Random.value;//��0��1ȡһ������value;
        for(int i=0;i<mayDropItemArrs.Length;i++)
        {
            if(value<=mayDropItemArrs[i].randomValue)
            {
                if(mayDropItemArrs[i].itemPrefabs!=null)
                {
                    Instantiate(mayDropItemArrs[i].itemPrefabs, transform.position + Vector3.up * 2, Quaternion.identity);
                }
                

            }
        }
    }
}
