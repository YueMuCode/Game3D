using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType { BAG,WEAPON,ARMOR,ACTION}//���slothoder�������Ʒ�������Ǳ����Ļ�����Ʒ�����������
public class SlotHolder : MonoBehaviour
{
    public SlotType InventoryType;
    public SlotItemUI slotItemUI;

    public void SendDataToSlotItemUI()
    {
        switch(InventoryType)
        {
            case SlotType.BAG:
                slotItemUI.inventoryData_SO = InventotyManager.Instance.inventoryData_SO;//��slotItemUI����ı����������ӵ����Ŵ����õ����ݿ���
               // Debug.Log("BagType");
                break;
            case SlotType.WEAPON:
                slotItemUI.inventoryData_SO = InventotyManager.Instance.equipmentData_SO;
                if(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData!=null)
                {
                    GameManager.Instance.playerStats.transform.GetComponent<UseOrEquipItem>().EquipWeapon(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData);
                }else
                {
                    GameManager.Instance.playerStats.transform.GetComponent<UseOrEquipItem>().UnEquipWeapon(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData);
                }
                break;
            case SlotType.ARMOR:

                break;
            case SlotType.ACTION:
                slotItemUI.inventoryData_SO = InventotyManager.Instance.actionData_SO;
                break;
        }
        if(slotItemUI.inventoryData_SO)
        { 
            var item = slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index];//�Ӵ����õ����ݿ����õ�UI�������Ӧλ�õ���Ʒ
            slotItemUI.SetActiveItemUI(item.itemData, item.amount);
            //Debug.Log("Senddata");
        }
    }


}
