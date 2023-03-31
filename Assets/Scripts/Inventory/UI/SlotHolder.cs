using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType { BAG,WEAPON,ARMOR,ACTION}//这个slothoder里面的物品的类型是背包的还是物品栏，方便后期
public class SlotHolder : MonoBehaviour
{
    public SlotType InventoryType;
    public SlotItemUI slotItemUI;

    public void SendDataToSlotItemUI()
    {
        switch(InventoryType)
        {
            case SlotType.BAG:
                slotItemUI.inventoryData_SO = InventotyManager.Instance.inventoryData_SO;//让slotItemUI里面的背包数据连接到我门创建好的数据库中
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
            var item = slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index];//从创建好的数据库中拿到UI表里面对应位置的物品
            slotItemUI.SetActiveItemUI(item.itemData, item.amount);
            //Debug.Log("Senddata");
        }
    }


}
