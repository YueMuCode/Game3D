using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootManHitPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {

            if (transform.parent.GetComponent<FootMan>().isCritical)
            {
                Debug.Log("玩家将会收到暴击伤害");
                // other.GetComponent<PlayerController>().anim.SetTrigger("Hit");
                other.transform.GetComponent<PlayerController>().GetHit(transform.parent.GetComponent<FootMan>().characterStats.maxDamage *
                 transform.parent.GetComponent<FootMan>().characterStats.criticalMultiplier, transform.parent);

            }
            else
            {
                Debug.Log("玩家将会收到伤害");
                other.transform.GetComponent<PlayerController>().GetHit(transform.parent.GetComponent<FootMan>().characterStats.minDamage, transform.parent);
            }
        }
        if (other.transform.CompareTag("Enemy"))
        {

            if (transform.parent.GetComponent<FootMan>().isCritical)
            {
                other.transform.GetComponent<EnemyController>().GetHit(transform.parent.GetComponent<FootMan>().characterStats.maxDamage *
              transform.parent.GetComponent<FootMan>().characterStats.criticalMultiplier, transform.parent);
            }
            else
            {
                other.transform.GetComponent<EnemyController>().GetHit(transform.parent.GetComponent<FootMan>().characterStats.maxDamage , transform.parent);
            }
        }
    }
}
