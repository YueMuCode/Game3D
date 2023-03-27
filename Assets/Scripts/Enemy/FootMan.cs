using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootMan : EnemyController
{
    public override bool isFoundPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRadius);//检测圆圈范围内的的collider，如果是玩家就进行发现操作
        foreach (Collider target in colliders)
        {
            if (target.transform.CompareTag("Player")|| target.transform.CompareTag("Enemy"))
            {
                attackTarget = target.transform.gameObject;//将获取的玩家对象赋值给attackTarget变量
                return true;
            }

        }
        attackTarget = null;
        return false;
    }
}
