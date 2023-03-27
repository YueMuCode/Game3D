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
                Debug.Log("��ҽ����յ������˺�");
                // other.GetComponent<PlayerController>().anim.SetTrigger("Hit");
                other.transform.GetComponent<PlayerController>().GetHit(transform.parent.GetComponent<FootMan>().characterStats.maxDamage *
                 transform.parent.GetComponent<FootMan>().characterStats.criticalMultiplier, transform.parent);

            }
            else
            {
                Debug.Log("��ҽ����յ��˺�");
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
