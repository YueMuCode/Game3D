using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if (FindObjectOfType<PlayerController>().isCritical)
            {
                other.transform.GetComponent<EnemyController>().GetHit(5 * FindObjectOfType<PlayerController>().characterStats.criticalMultiplier, FindObjectOfType<PlayerController>().transform);

            }
            else
            {
                other.transform.GetComponent<EnemyController>().GetHit(5, FindObjectOfType<PlayerController>().transform);
            }            
        }
    }
    
}
