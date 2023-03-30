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

    public void SetActiveItemUI(ItemData_SO item,int amount)
    {
        //if(amount==0)
        //{
        //    inventoryData_SO.listOfItems[Index].itemData = null;
        //    inventoryData_SO.listOfItems[Index].amount = 0;
        //    itemImage.gameObject.SetActive(false);
        //    return;
        //}
        //if(amount<0)
        //{
        //    item = null;
        //}
        if(item!=null)
        {
            itemImage.sprite = item.itemIcon;
            itemAmount.text = amount.ToString("00");
            itemImage.gameObject.SetActive(true);
            Debug.Log("Ë¢ÐÂUI");
        }
        else
        {
            itemImage.gameObject.SetActive(false);
        }
    }

}
