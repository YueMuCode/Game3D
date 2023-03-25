using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New AttackData",menuName ="Attack Stats/Data")]
public class AttackData_SO :ScriptableObject
{
    [Header("¹¥»÷µÄÊôÐÔ")]
    public float attackRange;
    public float skillRange;
    public float coolDown;
    public int minDamage;
    public int maxDamage;
    public float criticalMultiplier;
    public float criticalChance;
}
