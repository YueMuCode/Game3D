using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Useable, Weapon, Armor }
[CreateAssetMenu(fileName = "ItemData", menuName = "Item")]
public class ItemData_SO : ScriptableObject
{
    public ItemType itemType;//������Լ���������Ʒ��������ʲô
    public string itemName;//��Ʒ����
    public Sprite itemIcon;//��Ʒ�ڱ���������ʾ��UIͼƬ
    public int itemAmount;//ӛ���Ʒ�Ĕ���
    [TextArea]
    public string description = "";//��Ʒ������
    public bool stackable;//�Ƿ���Զѵ�

    [Header("Weapon")]
    public GameObject weaponPrefab;//��Ʒ��Ԥ����
    public AttackData_SO weaponAttackData;//��������Ĺ�������
}

