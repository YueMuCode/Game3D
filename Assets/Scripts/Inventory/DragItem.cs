using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;//����ק�Ľӿ�

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
    public void OnBeginDrag(PointerEventData eventData)//��ʼ��ק
    {
        InventotyManager.Instance.currentDragData = new InventotyManager.DragTempData();//ÿ����ק����������һ���µĶ���洢
        InventotyManager.Instance.currentDragData.originalSlotHolderData = GetComponentInParent<SlotHolder>();
        InventotyManager.Instance.currentDragData.originalParent = transform.parent.GetComponent<RectTransform>();// ((RectTransform)transform.parent; GetComponentInParent<RectTransform>() //
       // Debug.Log(InventotyManager.Instance.currentDragData.originalParent);

        //��¼�����Ʒ���ʼ����Ϣ
        transform.SetParent(InventotyManager.Instance.dragCanvas.transform, true);

    }
    public void OnDrag(PointerEventData eventData)//��ק�Ĺ���
    {

        transform.position = eventData.position;//-new Vector2(800,500);//��Ʒui����������ƶ�
       // Debug.Log(transform.position);
       // Debug.Log(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)//��ק������
    {
        //����ui��������Ҫ���н���
        if(EventSystem.current.IsPointerOverGameObject())//��������ָ���Ƿ���UI��Ʒ���Ϸ�
        {
            if(InventotyManager.Instance.CheckSlotRectTransformInAction(eventData.position)||
               InventotyManager.Instance.CheckSlotRectTransformInEquipment(eventData.position)||
               InventotyManager.Instance.CheckSlotRectTransformInInventory(eventData.position))//�����ط����з���
            {
                if(eventData.pointerEnter.GetComponent<SlotHolder>())
                {
                    targetSlotHolder = eventData.pointerEnter.GetComponent<SlotHolder>();
                   // Debug.Log(1);
                }else
                {
                    targetSlotHolder = eventData.pointerEnter.GetComponentInParent<SlotHolder>();//����������ǣ������������Ʒ����ô���ܴ������Ʒ�ĸ����õ�����
                }
                if(targetSlotHolder!=currentSlotHolder)//��ק���ĵط�����ԭ���ĵط��Ǿͽ��н���
                {
                    switch(targetSlotHolder.InventoryType)
                    {
                        case SlotType.BAG://�Ǳ����Ļ�����ֱ�ӽ���Ŀ���������ק����Ʒ
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
        
        else//�������
        {
            //�������������Ʒ������������
        }
        //���û������
        transform.SetParent(InventotyManager.Instance.currentDragData.originalParent);//��ק�Ĺ�����Ϊ����ui��ʾ�����ϲ㣬�������ĸ��������ڻ�����
        RectTransform t = transform as RectTransform;
        t.offsetMax = -Vector2.one * 10;//��Ϊ��������ק�Ĺ����У���û�и��¹������UI�߼�������ʵ��������ֻ�ǰ��������ŵ���ķ����Ϸ�һ�£�Ȼ�����������ص�ԭ�أ��������������ݾͽ����ˣ����������ݾ�ˢ�£���ʾ��UI�ͱ���
        t.offsetMin = Vector2.one * 10;
    }

    public  void SwapItem()
    {
        var targetItem = targetSlotHolder.slotItemUI.inventoryData_SO.listOfItems[targetSlotHolder.slotItemUI.Index];
        var tempItem = currentSlotHolder.slotItemUI.inventoryData_SO.listOfItems[currentSlotHolder.slotItemUI.Index]; 
        if(targetItem==tempItem&&targetItem.itemData.stackable)//�����ͬ���͵���Ʒ���ҿ��Զѵ����Ͱ�ԭ������Ʒ���
        {
            targetItem.amount += tempItem.amount;
            tempItem.amount = 0;//
            tempItem.itemData = null;
        }
        else//����ͬ�ͽ�������
        {
            currentSlotHolder.slotItemUI.inventoryData_SO.listOfItems[currentSlotHolder.slotItemUI.Index] = targetItem;
            targetSlotHolder.slotItemUI.inventoryData_SO.listOfItems[targetSlotHolder.slotItemUI.Index] = tempItem;
           // Debug.Log(1);
        }
    }



}
