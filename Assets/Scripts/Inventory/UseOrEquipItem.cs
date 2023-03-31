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
       
        //将人物的攻击属性加上武器的攻击属性
    }

    public void UnEquipWeapon(ItemData_SO weapon)
    {
        if(parentOfWeapon.transform.childCount==7)//人物模型原本就有6个
        {
            Destroy(parentOfWeapon.transform.GetChild(6).gameObject);
        }
        //更新人物的攻击数据
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
