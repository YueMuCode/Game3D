using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum SlotType { BAG,WEAPON,ARMOR,ACTION}//这个slothoder里面的物品的类型是背包的还是物品栏，方便后期
public class SlotHolder : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
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
           // Debug.Log("Senddata");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       if(eventData.clickCount%2==0)//判断鼠标双击
        {
            UseItem();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)//当鼠标悬浮在当前格子上的时候
    {
        if(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData!=null)
        {
            InventotyManager.Instance.itemToolTip.SetItemText(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData);
            InventotyManager.Instance.itemToolTip.gameObject.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InventotyManager.Instance.itemToolTip.gameObject.SetActive(false) ;
    }
    private void OnDisable()
    {
        InventotyManager.Instance.itemToolTip.gameObject.SetActive(false);//背包关闭，方格也就跟着关闭，那么显示的信息也要跟着关闭
    }

    public void UseItem()
    {
        if(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].amount>0&& slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData.itemType==ItemType.Useable)//物品在数据库中的总数量大于0.类型是可以使用的
        {
            GameManager.Instance.playerStats.GetComponent<UseOrEquipItem>().UseItem(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData);
            slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].amount -= 1;
            QuestManager.Instance.UpdateQuestProgress(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData.itemName, -1);//将对应的物品使用过后会-1；
        }
        //用完后要刷新新的数据给ui
        SendDataToSlotItemUI();
    }

  
}
