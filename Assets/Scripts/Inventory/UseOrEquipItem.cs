using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseOrEquipItem : MonoBehaviour
{
    public Transform parentOfWeapon;
   public void EquipWeapon(ItemData_SO weapon)
    {
        

        if(weapon.weaponPrefab!=null)
        {
            Instantiate(weapon.weaponPrefab, parentOfWeapon);
        }
       
        //������Ĺ������Լ��������Ĺ�������
    }

    public void UnEquipWeapon(ItemData_SO weapon)
    {
        if(parentOfWeapon.transform.childCount==7)//����ģ��ԭ������6��
        {
            Destroy(parentOfWeapon.transform.GetChild(6).gameObject);
        }
        //��������Ĺ�������
    }

    public void UseItem(ItemData_SO item)
    {
        if(GameManager.Instance.playerStats.currentHealth+item.useableItemData.health>GameManager.Instance.playerStats.maxHealth)
        {
            GameManager.Instance.playerStats.currentHealth = GameManager.Instance.playerStats.maxHealth;
        }else
        {
            GameManager.Instance.playerStats.currentHealth += item.useableItemData.health;
        }

    }
}
