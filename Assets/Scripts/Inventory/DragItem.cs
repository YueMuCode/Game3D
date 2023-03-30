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
        InventotyManager.Instance.currentDragData.originalParent = GetComponentInParent<RectTransform>();


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
    }
}
