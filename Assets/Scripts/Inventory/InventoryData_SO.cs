using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory Data")]
public class InventoryData_SO : ScriptableObject
{ 
    public List<ItemsInInventory> listOfItems = new List<ItemsInInventory>();//��list���洢�����Ʒ��ÿ����Ʒ�������洢
    public bool isFound;//�����������Ʒ�Ƿ��Ѿ����ںͿɶѵ����Ǿ͵��Ӳ��Ǿ�������λ�����
}
[System.Serializable]
public class ItemsInInventory//���ƽṹ��洢��������ĵ�����Ʒ����Ϣ
{
    public ItemData_SO itemData;
    public int amount;
}