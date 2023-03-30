using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventotyManager : SingleT<InventotyManager>
{

    public class DragTempData
    {
        public SlotHolder originalSlotHolderData;
        public RectTransform originalParent;
    }




    public InventoryData_SO inventoryData_SO;
    public InventoryData_SO actionData_SO;
    public InventoryData_SO equipmentData_SO;



    [Header("Container")]
    public Container bagContainer;
    public Container actionContainer;
    public Container equipmentContainer;


    [Header("DragTempCanvas")]
    public Canvas dragCanvas;

    public DragTempData currentDragData;
    private void Start()
    {
        bagContainer.UpdateEverySlotItemUI();//һ��ʼʱ�Ͱ����ݿ��ui�б�����һ��
        actionContainer.UpdateEverySlotItemUI();
        equipmentContainer.UpdateEverySlotItemUI();
    }

    public void AddItemToInventory(ItemData_SO newItem,int amountOfItem)
    {
        if(newItem.stackable)//��������Ʒ�ǿ��Զѵ���
        {
            foreach(var item in inventoryData_SO.listOfItems)//ѭ���������������
            {
                if(item.itemData==newItem)
                {
                    item.amount += amountOfItem;
                    inventoryData_SO.isFound = true;//�ܹ��ҵ�
                    break;//һ�μ���һ�������ҵ��Ͳ�����ѭ����
                }
            }
        }
        for(int i=0;i<inventoryData_SO.listOfItems.Count;i++)
        {
           // Debug.Log("ִ�е���");
            if(inventoryData_SO.listOfItems[i].itemData==null&&!inventoryData_SO.isFound)//������ӵ�����Ϊ�վ����
            {
                inventoryData_SO.listOfItems[i].itemData = newItem;
               // Debug.Log("�ڲ�");
                inventoryData_SO.listOfItems[i].amount = amountOfItem;
                break;//�ҵ��ĵ�һ���հ׸������
            }
        }
    }
}
