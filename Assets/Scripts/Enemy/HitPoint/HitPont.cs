using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPont : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //防止出现攻击伤害为零，应该加mathfmax判断
            
            if(transform.parent.GetComponent<EnemyController>().isCritical)
            {
               // Debug.Log("玩家将会收到暴击伤害");
               // other.GetComponent<PlayerController>().anim.SetTrigger("Hit");
                other.GetComponent<PlayerController>().GetHit(transform.parent.GetComponent<EnemyController>().characterStats.maxDamage* transform.parent.GetComponent<EnemyController>().characterStats.criticalMultiplier, transform.parent);
                
            }
            else
            {
                other.GetComponent<PlayerController>().GetHit(transform.parent.GetComponent<EnemyController>().characterStats.minDamage, transform.parent);
               //Debug.Log("玩家受到伤害");
            }
        }    
    }
}
