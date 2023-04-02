using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RHandHitPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("FootMan"))
        {
            // other.transform.GetComponent<EnemyController>().anim.SetBool("Dizzy", false);
            if (FindObjectOfType<PlayerController>().isCritical)
            {
                // other.transform.GetComponent<EnemyController>().anim.SetTrigger("Dizzy");
                //  other.transform.GetComponent<EnemyController>().anim.SetBool("Dizzy", true);
                other.transform.GetComponent<EnemyController>().GetHit(FindObjectOfType<PlayerController>().characterStats.maxDamage * FindObjectOfType<PlayerController>().characterStats.criticalMultiplier, FindObjectOfType<PlayerController>().transform);

                // anim.SetTrigger("Dizzy");

            }
            else
            {
                other.transform.GetComponent<EnemyController>().GetHit(FindObjectOfType<PlayerController>().characterStats.minDamage, FindObjectOfType<PlayerController>().transform);
                // other.transform.GetComponent<EnemyController>().anim.SetBool("Dizzy", false);
            }
        }
    }
}
