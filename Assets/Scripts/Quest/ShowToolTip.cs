using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShowToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SlotItemUI currentItemUI;
    void Awake()
    {
        currentItemUI = GetComponent<SlotItemUI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        QuestUIController.Instance.tooltip.gameObject.SetActive(true);
        QuestUIController.Instance.tooltip.SetItemText(currentItemUI.currentItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        QuestUIController.Instance.tooltip.gameObject.SetActive(false);
    }
}
