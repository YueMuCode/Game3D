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
         
            slotHolders[i].slotItemUI.Index = i;//每一个物品都由一个记录的index将index传给他，这样，ui表的位置就可以数据库一一对应；
            
            slotHolders[i].SendDataToSlotItemUI();
            //Debug.Log("Container");
        }
    }
}
