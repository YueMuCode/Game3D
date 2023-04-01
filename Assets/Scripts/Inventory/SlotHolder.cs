using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum SlotType { BAG,WEAPON,ARMOR,ACTION}//���slothoder�������Ʒ�������Ǳ����Ļ�����Ʒ�����������
public class SlotHolder : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public SlotType InventoryType;
    public SlotItemUI slotItemUI;

   
    public void SendDataToSlotItemUI()
    {
        switch(InventoryType)
        {
            case SlotType.BAG:
                slotItemUI.inventoryData_SO = InventotyManager.Instance.inventoryData_SO;//��slotItemUI����ı����������ӵ����Ŵ����õ����ݿ���
               // Debug.Log("BagType");
                break;
            case SlotType.WEAPON:
                slotItemUI.inventoryData_SO = InventotyManager.Instance.equipmentData_SO;
                if(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData!=null)
                {
                    GameManager.Instance.playerStats.transform.GetComponent<UseOrEquipItem>().EquipWeapon(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData);
                }else
                {
                    GameManager.Instance.playerStats.transform.GetComponent<UseOrEquipItem>().UnEquipWeapon(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData);
                }
                break;
            case SlotType.ARMOR:

                break;
            case SlotType.ACTION:
                slotItemUI.inventoryData_SO = InventotyManager.Instance.actionData_SO;
                break;
        }
        if(slotItemUI.inventoryData_SO)
        { 
            var item = slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index];//�Ӵ����õ����ݿ����õ�UI�������Ӧλ�õ���Ʒ
            slotItemUI.SetActiveItemUI(item.itemData, item.amount);
           // Debug.Log("Senddata");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       if(eventData.clickCount%2==0)//�ж����˫��
        {
            UseItem();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)//����������ڵ�ǰ�����ϵ�ʱ��
    {
        if(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData!=null)
        {
            InventotyManager.Instance.itemToolTip.SetItemText(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData);
            InventotyManager.Instance.itemToolTip.gameObject.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InventotyManager.Instance.itemToolTip.gameObject.SetActive(false) ;
    }
    private void OnDisable()
    {
        InventotyManager.Instance.itemToolTip.gameObject.SetActive(false);//�����رգ�����Ҳ�͸��Źرգ���ô��ʾ����ϢҲҪ���Źر�
    }

    public void UseItem()
    {
        if(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].amount>0&& slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData.itemType==ItemType.Useable)//��Ʒ�����ݿ��е�����������0.�����ǿ���ʹ�õ�
        {
            GameManager.Instance.playerStats.GetComponent<UseOrEquipItem>().UseItem(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData);
            slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].amount -= 1;
            QuestManager.Instance.UpdateQuestProgress(slotItemUI.inventoryData_SO.listOfItems[slotItemUI.Index].itemData.itemName, -1);//����Ӧ����Ʒʹ�ù����-1��
        }
        //�����Ҫˢ���µ����ݸ�ui
        SendDataToSlotItemUI();
    }

  
}
