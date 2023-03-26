using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.transform.GetComponent<EnemyController>().GetHit(5);
            Debug.Log("�����ܵ��˺�");
        }
        if(other.CompareTag("Ground"))
        {
            Debug.Log("���Գɹ�");
        }
    }
    
}
