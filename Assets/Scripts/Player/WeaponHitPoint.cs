using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.transform.GetComponent<EnemyController>().anim.SetBool("Dizzy", false);
            if (FindObjectOfType<PlayerController>().isCritical)
            {
               // other.transform.GetComponent<EnemyController>().anim.SetTrigger("Dizzy");
                other.transform.GetComponent<EnemyController>().anim.SetBool("Dizzy", true);
                other.transform.GetComponent<EnemyController>().GetHit(5 * FindObjectOfType<PlayerController>().characterStats.criticalMultiplier, FindObjectOfType<PlayerController>().transform);
                 
                // anim.SetTrigger("Dizzy");

            }
            else
            {
                other.transform.GetComponent<EnemyController>().GetHit(5, FindObjectOfType<PlayerController>().transform);
               // other.transform.GetComponent<EnemyController>().anim.SetBool("Dizzy", false);
            }            
        }
    }

}
