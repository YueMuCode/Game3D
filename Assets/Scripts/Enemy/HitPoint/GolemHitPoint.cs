using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHitPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("撞到玩家了");
            //transform.parent.GetComponent<Golem>().kickForce;
            //transform.parent.GetComponent<Golem>()
            Vector3 dir = (transform.parent.GetComponent<Golem>().attackTarget.transform.position - transform.parent.position).normalized;//获取敌人和玩家的方向向量
            transform.parent.GetComponent<Golem>().attackTarget.transform.GetComponent<PlayerController>().velocity = dir * transform.parent.GetComponent<Golem>().kickForce;
            other.transform.GetComponent<PlayerController>().GetHit(transform.parent.GetComponent<Golem>().characterStats.minDamage, transform.parent);
            other.transform.GetComponent<PlayerController>().anim.SetTrigger("Dizzy");
            Debug.Log("玩家受到撞击伤害" + transform.parent.GetComponent<Golem>().characterStats.minDamage);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("撞完玩家了");
            //transform.parent.GetComponent<Golem>().kickForce;
            //transform.parent.GetComponent<Golem>()
            Vector3 dir = (transform.parent.GetComponent<Golem>().attackTarget.transform.position - transform.parent.position).normalized;//获取敌人和玩家的方向向量
            transform.parent.GetComponent<Golem>().attackTarget.transform.GetComponent<PlayerController>().velocity = Vector3.zero;

        }
    }
}
