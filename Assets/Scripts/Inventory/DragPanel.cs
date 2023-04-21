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
        canvas = InventotyManager.Instance.GetComponent<Canvas>();//注意awake，所以挂载这个代码的组件必须要先关闭
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;//调整物体的中心位置
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.SetSiblingIndex(2);//设置层级关系，确保鼠标点击的面板显示在上层，要注意canvas的子物体里面，排在下面的渲染在上层

    }
}
