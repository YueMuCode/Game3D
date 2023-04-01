using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlotItemUI : MonoBehaviour
{
    public Image itemImage;
    public Text itemAmount;


    public InventoryData_SO inventoryData_SO { get; set; }
    public int Index { get; set; } = -1;

    public  ItemData_SO currentItem;//用于保存当前SetActiveItemUI的单个数据，在显示任务数据的时候，用到

    public void SetActiveItemUI(ItemData_SO item,int amount)
    {
        if (amount == 0)
        {
            inventoryData_SO.listOfItems[Index].itemData = null;
            inventoryData_SO.listOfItems[Index].amount = 0;
            itemImage.gameObject.SetActive(false);
            return;
        }
        if (amount < 0)
        {
            item = null;
        }

        if (item!=null)
        {
            currentItem = item;
            itemImage.sprite = item.itemIcon;
            itemAmount.text = amount.ToString("00");
            itemImage.gameObject.SetActive(true);
            //Debug.Log("刷新UI");
        }
        else
        {
            itemImage.gameObject.SetActive(false);
        }
    }

}
