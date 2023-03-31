using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseOrEquipItem : MonoBehaviour
{
    public Transform parentOfWeapon;
    public RuntimeAnimatorController temp;
    private void Start()
    {
        temp = GetComponent<Animator>().runtimeAnimatorController;
    }
    public void EquipWeapon(ItemData_SO weapon)
    {
        

        if(weapon.weaponPrefab!=null)
        {
            Instantiate(weapon.weaponPrefab, parentOfWeapon);
            GetComponent<Animator>().runtimeAnimatorController = weapon.weaponOverrideAnimator;
        }
       
        //将人物的攻击属性加上武器的攻击属性
    }

    public void UnEquipWeapon(ItemData_SO weapon)
    {
        if(parentOfWeapon.transform.childCount>=7)//人物模型原本就有6个,一直往武器拖装备会出现很多个的情况，当然可以在装备武器的时候优化，即交换武器方格的时候也调用一次UNEquipment
        {
            for(int i=6;i<parentOfWeapon.transform.childCount;i++)
            {
                Destroy(parentOfWeapon.transform.GetChild(i).gameObject);
            }
           
        }
        GetComponent<Animator>().runtimeAnimatorController = temp;
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
