using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootMan : EnemyController
{
    public override bool isFoundPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRadius);//���ԲȦ��Χ�ڵĵ�collider���������Ҿͽ��з��ֲ���
        foreach (Collider target in colliders)
        {
            if (target.transform.CompareTag("Player")|| target.transform.CompareTag("Enemy"))
            {
                attackTarget = target.transform.gameObject;//����ȡ����Ҷ���ֵ��attackTarget����
                return true;
            }

        }
        attackTarget = null;
        return false;
    }
}
