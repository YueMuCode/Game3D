using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragPanel : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    RectTransform rectTransform;
    Canvas canvas;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = InventotyManager.Instance.GetComponent<Canvas>();//ע��awake�����Թ������������������Ҫ�ȹر�
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;//�������������λ��
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.SetSiblingIndex(2);//���ò㼶��ϵ��ȷ��������������ʾ���ϲ㣬Ҫע��canvas�����������棬�����������Ⱦ���ϲ�

    }
}
