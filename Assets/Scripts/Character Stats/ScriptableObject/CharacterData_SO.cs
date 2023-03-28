using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Data",menuName ="Character Stats/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("����Ļ�������")]
    public float maxHealth;
    public float currentHealth;
    public float maxDefend;
    public float currentDefend;
    [Header("��ɱ��õľ���ֵ")]
    public int killExp;



    [Header("Level")]
    public int maxLevel;
    public int level;
    public int needExp;//��Ҫ�ľ���ֵ
    public int currentExp;
    public float levelUpBuffer;


}
