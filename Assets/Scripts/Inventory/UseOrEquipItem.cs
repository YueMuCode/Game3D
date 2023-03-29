using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseOrEquipItem : MonoBehaviour
{
    public Transform parentOfWeapon;
   public void EquipWeapon(ItemData_SO weapon)
    {
        Instantiate(weapon.weaponPrefab, parentOfWeapon);
        //将人物的攻击属性加上武器的攻击属性
    }
}
