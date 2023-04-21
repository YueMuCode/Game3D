using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;//有拖拽的接口

[RequireComponent(typeof(SlotItemUI))]
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    SlotItemUI currentSlotItemUI;
    SlotHolder currentSlotHolder;
    SlotHolder targetSlotHolder;

    private void Awake()
    {
        currentSlotItemUI = GetComponent<SlotItemUI>();
        currentSlotHolder = GetComponentInParent<SlotHolder>();
    }
    public void OnBeginDrag(PointerEventData eventData)//开始拖拽
    {
        InventotyManager.Instance.currentDragData = new InventotyManager.DragTempData();//每次拖拽都重新生成一个新的对象存储
        InventotyManager.Instance.currentDragData.originalSlotHolderData = GetComponentInParent<SlotHolder>();
        InventotyManager.Instance.currentDragData.originalParent = transform.parent.GetComponent<RectTransform>();// ((RectTransform)transform.parent; GetComponentInParent<RectTransform>() //
       // Debug.Log(InventotyManager.Instance.currentDragData.originalParent);

        //记录这个物品的最开始的信息
        transform.SetParent(InventotyManager.Instance.dragCanvas.transform, true);

    }
    public void OnDrag(PointerEventData eventData)//拖拽的过程
    {

        transform.position = eventData.position;//-new Vector2(800,500);//物品ui跟随着鼠标移动
       // Debug.Log(transform.position);
       // Debug.Log(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)//拖拽结束后
    {
        //放下ui，且数据要进行交换
        if(EventSystem.current.IsPointerOverGameObject())//现在鼠标的指针是否在UI物品的上方
        {
            if(InventotyManager.Instance.CheckSlotRectTransformInAction(eventData.position)||
               InventotyManager.Instance.CheckSlotRectTransformInEquipment(eventData.position)||
               InventotyManager.Instance.CheckSlotRectTransformInInventory(eventData.position))//三个地方都有方格
            {
                if(eventData.pointerEnter.GetComponent<SlotHolder>())
                {
                    targetSlotHolder = eventData.pointerEnter.GetComponent<SlotHolder>();
                   // Debug.Log(1);
                }else
                {
                    targetSlotHolder = eventData.pointerEnter.GetComponentInParent<SlotHolder>();//这种情况就是，这个格子有物品，那么这能从这个物品的父级拿到格子
                }
                if(targetSlotHolder!=currentSlotHolder)//拖拽到的地方不是原来的地方那就进行交换
                {
                    switch(targetSlotHolder.InventoryType)
                    {
                        case SlotType.BAG://是背包的话，就直接交换目标和正在拖拽的物品
                            SwapItem();
                            break;
                        case SlotType.ACTION:
                            if(currentSlotHolder.slotItemUI.inventoryData_SO.listOfItems[currentSlotHolder.slotItemUI.Index].itemData.itemType==ItemType.Useable)
                            {
                                SwapItem();
                            }
                            break;
                        case SlotType.WEAPON:
                            if(currentSlotHolder.slotItemUI.inventoryData_SO.listOfItems[currentSlotHolder.slotItemUI.Index].itemData.itemType == ItemType.Weapon)
                            {
                                SwapItem();
                                
                            }
                            break;
                        case SlotType.ARMOR:
                            if (currentSlotHolder.slotItemUI.inventoryData_SO.listOfItems[currentSlotHolder.slotItemUI.Index].itemData.itemType == ItemType.Armor)
                            {
                                SwapItem();
                            }
                            break;
                           
                    }
                }
                targetSlotHolder.SendDataToSlotItemUI();
                currentSlotHolder.SendDataToSlotItemUI();

            }
        }
        
        else//如果不在
        {
            //将背包里面的物品丢弃到世界中
        }
        //如果没被丢弃
        transform.SetParent(InventotyManager.Instance.currentDragData.originalParent);//拖拽的过程中为了让ui显示在最上层，换了他的父级，现在换回来
        RectTransform t = transform as RectTransform;
        t.offsetMax = -Vector2.one * 10;//因为方格在拖拽的过程中，并没有更新过方格的UI逻辑，所以实际上我们只是把这个方格放到别的方格上放一下，然后让这个方格回到原地，两个方法的数据就交换了，交换了数据就刷新，显示的UI就变了
        t.offsetMin = Vector2.one * 10;
    }

    public  void SwapItem()
    {
        var targetItem = targetSlotHolder.slotItemUI.inventoryData_SO.listOfItems[targetSlotHolder.slotItemUI.Index];
        var tempItem = currentSlotHolder.slotItemUI.inventoryData_SO.listOfItems[currentSlotHolder.slotItemUI.Index]; 
        if(targetItem==tempItem&&targetItem.itemData.stackable)//如果是同类型的物品并且可以堆叠，就把原来的物品清空
        {
            targetItem.amount += tempItem.amount;
            tempItem.amount = 0;//
            tempItem.itemData = null;
        }
        else//不相同就交互数据
        {
            currentSlotHolder.slotItemUI.inventoryData_SO.listOfItems[currentSlotHolder.slotItemUI.Index] = targetItem;
            targetSlotHolder.slotItemUI.inventoryData_SO.listOfItems[targetSlotHolder.slotItemUI.Index] = tempItem;
           // Debug.Log(1);
        }
    }



}
