using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPont : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //��ֹ���ֹ����˺�Ϊ�㣬Ӧ�ü�mathfmax�ж�
            
            if(transform.parent.GetComponent<EnemyController>().isCritical)
            {
               // Debug.Log("��ҽ����յ������˺�");
               // other.GetComponent<PlayerController>().anim.SetTrigger("Hit");
                other.GetComponent<PlayerController>().GetHit(transform.parent.GetComponent<EnemyController>().characterStats.maxDamage* transform.parent.GetComponent<EnemyController>().characterStats.criticalMultiplier, transform.parent);
                
            }
            else
            {
                other.GetComponent<PlayerController>().GetHit(transform.parent.GetComponent<EnemyController>().characterStats.minDamage, transform.parent);
               //Debug.Log("����ܵ��˺�");
            }
        }    
    }
}
