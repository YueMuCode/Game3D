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


    [Header("PanelControl")]
    public GameObject bagPanelPrefabs;
    public GameObject CharacterPanelPrefabs;
    private bool bagisOpen = false;
    private bool StatsisOpen = false;
    private void Start()
    {
        bagContainer.UpdateEverySlotItemUI();//һ��ʼʱ�Ͱ����ݿ��ui�б�����һ��
        actionContainer.UpdateEverySlotItemUI();
        equipmentContainer.UpdateEverySlotItemUI();
    }

    public void AddItemToInventory(ItemData_SO newItem,int amountOfItem)
    {
        inventoryData_SO.isFound = false;
        if (newItem.stackable)//��������Ʒ�ǿ��Զѵ���
        {
            foreach(var item in inventoryData_SO.listOfItems)//ѭ���������������
            {
                if(item.itemData==newItem)
                {
                  //  Debug.Log("���ڲ�");
                    item.amount += amountOfItem;
                    inventoryData_SO.isFound = true;//�ܹ��ҵ�
                    break;//һ�μ���һ�������ҵ��Ͳ�����ѭ����
                }
            }
        }
        for(int i=0;i<inventoryData_SO.listOfItems.Count;i++)
        {
          //  Debug.Log("ִ�е���");
           // Debug.Log(inventoryData_SO.isFound);
            if(inventoryData_SO.listOfItems[i].itemData==null&&!inventoryData_SO.isFound)//������ӵ�����Ϊ�վ����
            {
                inventoryData_SO.listOfItems[i].itemData = newItem;
              //  Debug.Log("���ڲ�");
                inventoryData_SO.listOfItems[i].amount = amountOfItem;
                break;//�ҵ��ĵ�һ���հ׸������
            }
        }
    }




    #region ���������ʱ��ʱ���������Ƿ�λ��ĳ��SlotImage(��������Ʒ����������嶼����)��RectTransform��
    public bool CheckSlotRectTransformInInventory(Vector3 mousePosition)//��ⱳ���Ǿ��Ǳ�����container
    {
        for(int i=0;i<bagContainer.slotHolders.Length;i++)
        {
            RectTransform t = bagContainer.slotHolders[i].transform as RectTransform;//.GetComponent<RectTransform>();
            if(RectTransformUtility.RectangleContainsScreenPoint(t,mousePosition))
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckSlotRectTransformInAction(Vector3 mousePosition)//�����Ʒ��������Ʒ����container
    {
        for (int i = 0; i < actionContainer.slotHolders.Length; i++)
        {
            RectTransform t = actionContainer.slotHolders[i].transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(t, mousePosition))
            {
                return true;
            }
        }

        return false;
    }
    public bool CheckSlotRectTransformInEquipment(Vector3 mousePosition)//���������������������container
    {
        for (int i = 0; i < equipmentContainer.slotHolders.Length; i++)
        {
            RectTransform t = equipmentContainer.slotHolders[i].transform as RectTransform;//.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(t, mousePosition))
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    private void Update()
    {
        ClosePanel();
    }

    //�رջ��ߴ����
    public void ClosePanel()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            bagisOpen = !bagisOpen;
            bagPanelPrefabs.gameObject.SetActive(bagisOpen);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StatsisOpen = !StatsisOpen;
            CharacterPanelPrefabs.gameObject.SetActive(StatsisOpen);
        }
    }
}
