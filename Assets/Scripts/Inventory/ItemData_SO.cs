using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Useable, Weapon, Armor }
[CreateAssetMenu(fileName = "ItemData", menuName = "Item")]
public class ItemData_SO : ScriptableObject
{
    public ItemType itemType;//这个可以捡起来的物品的类型是什么
    public string itemName;//物品名称
    public Sprite itemIcon;//物品在背包里面显示的UI图片
    public int itemAmount;//記錄物品的數量
    [TextArea]
    public string description = "";//物品的描述
    public bool stackable;//是否可以堆叠

    [Header("Weapon")]
    public GameObject weaponPrefab;//物品的预制体
    public AttackData_SO weaponAttackData;//这把武器的攻击数据
}

