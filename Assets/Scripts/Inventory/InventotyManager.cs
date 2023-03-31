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
        bagContainer.UpdateEverySlotItemUI();//一开始时就把数据库和ui列表连接一下
        actionContainer.UpdateEverySlotItemUI();
        equipmentContainer.UpdateEverySlotItemUI();
    }

    public void AddItemToInventory(ItemData_SO newItem,int amountOfItem)
    {
        inventoryData_SO.isFound = false;
        if (newItem.stackable)//如果这个物品是可以堆叠的
        {
            foreach(var item in inventoryData_SO.listOfItems)//循环背包里面的数据
            {
                if(item.itemData==newItem)
                {
                  //  Debug.Log("上内部");
                    item.amount += amountOfItem;
                    inventoryData_SO.isFound = true;//能够找到
                    break;//一次捡起一个，被找到就不用再循环了
                }
            }
        }
        for(int i=0;i<inventoryData_SO.listOfItems.Count;i++)
        {
          //  Debug.Log("执行到了");
           // Debug.Log(inventoryData_SO.isFound);
            if(inventoryData_SO.listOfItems[i].itemData==null&&!inventoryData_SO.isFound)//这个格子的数据为空就添加
            {
                inventoryData_SO.listOfItems[i].itemData = newItem;
              //  Debug.Log("下内部");
                inventoryData_SO.listOfItems[i].amount = amountOfItem;
                break;//找到的第一个空白格子添加
            }
        }
    }




    #region 检查鼠标放下时此时鼠标的坐标是否位于某个SlotImage(背包、物品栏、人物面板都包括)的RectTransform内
    public bool CheckSlotRectTransformInInventory(Vector3 mousePosition)//检测背包那就是背包的container
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

    public bool CheckSlotRectTransformInAction(Vector3 mousePosition)//检测物品栏就是物品栏的container
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
    public bool CheckSlotRectTransformInEquipment(Vector3 mousePosition)//检测人物面板就是人物面板的container
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

    //关闭或者打开面板
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
