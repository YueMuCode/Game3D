using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventotyManager : SingleT<InventotyManager>
{
    public InventoryData_SO inventoryData_SO;




    [Header("Container")]
    public Container bagContainer;

    private void Start()
    {
        bagContainer.UpdateEverySlotItemUI();//一开始时就把数据库和ui列表连接一下
    }

    public void AddItemToInventory(ItemData_SO newItem,int amountOfItem)
    {
        if(newItem.stackable)//如果这个物品是可以堆叠的
        {
            foreach(var item in inventoryData_SO.listOfItems)//循环背包里面的数据
            {
                if(item.itemData==newItem)
                {
                    item.amount += amountOfItem;
                    inventoryData_SO.isFound = true;//能够找到
                    break;//一次捡起一个，被找到就不用再循环了
                }
            }
        }
        for(int i=0;i<inventoryData_SO.listOfItems.Count;i++)
        {
            Debug.Log("执行到了");
            if(inventoryData_SO.listOfItems[i].itemData==null&&!inventoryData_SO.isFound)//这个格子的数据为空就添加
            {
                inventoryData_SO.listOfItems[i].itemData = newItem;
                Debug.Log("内部");
                inventoryData_SO.listOfItems[i].amount = amountOfItem;
                break;//找到的第一个空白格子添加
            }
        }
    }
}
