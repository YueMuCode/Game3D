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
        InventotyManager.Instance.currentDragData.originalParent = GetComponentInParent<RectTransform>();


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
    }
}
