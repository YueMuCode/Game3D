using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : EnemyController
{
    public float kickForce = 10f;

    public GameObject rockPrefab;
    public Transform throwHandPos;

    public void ThrowEvent()
    {
        if(attackTarget!=null)
        {
           GameObject rock= Instantiate(rockPrefab, throwHandPos.position, Quaternion.identity);//�ֲ�����Ԥ���壬��ת��ʯͷ��ԭ����ת��
            rock.GetComponent<Rock>().hitTarget = attackTarget;//������Ŀ�괫��ʯͷ��ײ��Ŀ��

        }
         

    }
}
