using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    bool canPickUp;
    public ItemData_SO itemData_SO;//这个数据类型必须要定义到这里，因为有多个类型的可捡起物品，那么每个物品就要不同的物品数据
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           // Debug.Log("玩家准备捡起");
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
            InventotyManager.Instance.bagContainer.UpdateEverySlotItemUI();//捡起物品的时候也更新一下
            QuestManager.Instance.UpdateQuestProgress(itemData_SO.itemName, itemData_SO.itemAmount);
            //Debug.Log("开始捡起");
            //GameManager.Instance.playerStats.GetComponent<UseOrEquipItem>().EquipWeapon(itemData_SO);
        }

    }
}
