using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    bool canPickUp;
    public ItemData_SO itemData_SO;//����������ͱ���Ҫ���嵽�����Ϊ�ж�����͵Ŀɼ�����Ʒ����ôÿ����Ʒ��Ҫ��ͬ����Ʒ����
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           // Debug.Log("���׼������");
            canPickUp = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&&canPickUp)
        {
            Destroy(gameObject);
            InventotyManager.Instance.AddItemToInventory(itemData_SO,itemData_SO.itemAmount);
            InventotyManager.Instance.bagContainer.UpdateEverySlotItemUI();//������Ʒ��ʱ��Ҳ����һ��
            QuestManager.Instance.UpdateQuestProgress(itemData_SO.itemName, itemData_SO.itemAmount);
            //Debug.Log("��ʼ����");
            //GameManager.Instance.playerStats.GetComponent<UseOrEquipItem>().EquipWeapon(itemData_SO);
        }

    }
}
