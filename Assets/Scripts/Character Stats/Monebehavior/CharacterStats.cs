using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private CharacterData_SO characterData_so;//获取 tempplateCharacterData_so生成的值，防止共用同一套数据
    public CharacterData_SO tempplateCharacterData_so;//拖过unity界面拖动获取数据库
    public AttackData_SO attackData_SO;

    private void Awake()
    {
        if(tempplateCharacterData_so!=null)
        {
            characterData_so = Instantiate(tempplateCharacterData_so);
        }    
    }

    #region 获取数据库中的人物属性数据
    public float maxHealth
    {
        get
        {
            if(characterData_so)
            {
                return characterData_so.maxHealth;
            }else
            {
                return 0;
            }
        }
        set
        {
            characterData_so.maxHealth = value;
        }
    }
    public float currentHealth
    {
        get
        {
            if(characterData_so)
            {
                return characterData_so.currentHealth;
            }else
            {
                return 0;
            }
        }
        set
        {
            characterData_so.currentHealth = value;
        }
    }
    public float maxDefend
    {
        get
        {
            if(characterData_so)
            {
                return characterData_so.maxDefend;
            }else
            {
                return 0;
            }
        }
        set
        {
            characterData_so.maxDefend = value;
        }
    }
    public float currentDefend
    {
        get
        {
            if (characterData_so)
            {
                return characterData_so.currentDefend;
            }else
            {
                return 0;
            }

        }
        set
        {
            characterData_so.currentDefend = value;
        }
    }
    #endregion

    #region 获取数据库中的攻击属性的数据
    public float attackRange
    {
        get
        {
            if(attackData_SO)
            {
                return attackData_SO.attackRange;
            }
            return 0;
        }
        set
        {
            attackData_SO.attackRange = value;
        }
    }
    public float skillRange
    {
        get
        {
            if(attackData_SO)
            {
                return attackData_SO.skillRange;
            }
            return 0;
        }
        set
        {
            attackData_SO.skillRange = value;
        }
    }
    public float coolDown
    {
        get
        {
            if(attackData_SO)
            {
                return attackData_SO.coolDown;
            }
            return 0;
        }
        set
        {
            attackData_SO.coolDown = value;
        }
    }
    public int minDamage
    {
        get
        {
            if(attackData_SO)
            {
                return attackData_SO.minDamage;
            }
            return 0;
        }
        set
        {
            attackData_SO.minDamage = value;
        }
    }
    public int maxDamage
    {
        get
        {
            if(attackData_SO)
            {
                return attackData_SO.maxDamage;
            }
            return 0;
        }
        set
        {
            attackData_SO.maxDamage = value;
        }
    }
    public float criticalMultiplier
    {
        get
        {
            if(attackData_SO)
            {
                return attackData_SO.criticalMultiplier;
            }
            return 0;
        }
        set
        {
            attackData_SO.criticalMultiplier=value;
        }
    }
    public float criticalChance
    {
        get
        {
            if(attackData_SO)
            {
                return attackData_SO.criticalChance;
            }
            return 0;
        }
        set
        {
            attackData_SO.criticalChance = value;
        }
    }
    #endregion
}
