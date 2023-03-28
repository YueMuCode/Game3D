using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Data",menuName ="Character Stats/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("人物的基本属性")]
    public float maxHealth;
    public float currentHealth;
    public float maxDefend;
    public float currentDefend;
    [Header("击杀获得的经验值")]
    public int killExp;



    [Header("Level")]
    public int maxLevel;
    public int level;
    public int needExp;//需要的经验值
    public int currentExp;
    public float levelUpBuffer;


}
