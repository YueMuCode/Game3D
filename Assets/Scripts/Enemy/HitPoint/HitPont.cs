using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPont : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            if(transform.parent.GetComponent<EnemyController>().isCritical)
            {
               // Debug.Log("玩家将会收到暴击伤害");
               // other.GetComponent<PlayerController>().anim.SetTrigger("Hit");
                other.GetComponent<PlayerController>().GetHit(1* transform.parent.GetComponent<EnemyController>().characterStats.criticalMultiplier, transform.parent);
                
            }
            else
            {
                other.GetComponent<PlayerController>().GetHit(1f, transform.parent);
               //Debug.Log("玩家受到伤害");
            }
        }    
    }
}
