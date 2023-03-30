using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory Data")]
public class InventoryData_SO : ScriptableObject
{ 
    public List<ItemsInInventory> listOfItems = new List<ItemsInInventory>();//用list来存储多个物品。每个物品用类来存储
    public bool isFound;//背包里面的物品是否已经存在和可堆叠，是就叠加不是就重新找位置填充
}
[System.Serializable]
public class ItemsInInventory//类似结构体存储背包里面的单个物品的信息
{
    public ItemData_SO itemData;
    public int amount;
}