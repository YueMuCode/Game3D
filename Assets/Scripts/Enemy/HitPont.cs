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
                Debug.Log("��ҽ����յ������˺�");
                other.GetComponent<PlayerController>().GetHit(1* transform.parent.GetComponent<EnemyController>().characterStats.criticalMultiplier);
            }else
            {
                other.GetComponent<PlayerController>().GetHit(1f);
                Debug.Log("����ܵ��˺�");
            }
        }    
    }
}
