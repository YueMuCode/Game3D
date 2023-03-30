using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public SlotHolder[] slotHolders;
    public void UpdateEverySlotItemUI()
    {
        for (int i = 0; i < slotHolders.Length; i++)
        {
         
            slotHolders[i].slotItemUI.Index = i;//ÿһ����Ʒ����һ����¼��index��index��������������ui���λ�þͿ������ݿ�һһ��Ӧ��
            
            slotHolders[i].SendDataToSlotItemUI();
            //Debug.Log("Container");
        }
    }
}
