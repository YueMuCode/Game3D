using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO characterData_so;//获取 tempplateCharacterData_so生成的值，防止共用同一套数据
    public CharacterData_SO tempplateCharacterData_so;//拖过unity界面拖动获取数据库
    public AttackData_SO attackData_SO;

    public AttackData_SO templateAttackData_SO;
    private void Awake()
    {
        if(tempplateCharacterData_so!=null&&characterData_so==null)
        {
            characterData_so = Instantiate(tempplateCharacterData_so);
        }    
        if(templateAttackData_SO!=null&&attackData_SO==null)
        {
            attackData_SO = Instantiate(templateAttackData_SO);
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

    #region 升级系统所需要的数据
    public int maxLevel
    {
        get
        {
            if (characterData_so)
            {
                return characterData_so.maxLevel;
            }
            return 0;
        }
        set
        {
            characterData_so.maxLevel = value;
        }
    }
    public int level
    {
        get
        {
            if(characterData_so)
            {
                return characterData_so.level;
            }
            return 0;
        }
        set
        {
            characterData_so.level = value;
        }
    }
    public int killExp
    {
        get
        {
            if(characterData_so)
            {
                return characterData_so.killExp;
            }
            return 0;
        }
        set
        {
            characterData_so.killExp = value;
        }
    }
    public int needExp
    {
        get
        {
            if(characterData_so)
            {
                return characterData_so.needExp;
            }
            return 0;
        }
        set
        {
            characterData_so.needExp = value;
        }
    }
    public int currentExp
    {
        get
        {
            if(characterData_so)
            {
                return characterData_so.currentExp;
            }
            return 0;
        }
        set
        {
            characterData_so.currentExp = value;
        }
    }
    public float levelUpBuffer
    {
        get
        {
            if(characterData_so)
            {
                return characterData_so.levelUpBuffer;
            }
            return 0;
        }
        set
        {
            characterData_so.levelUpBuffer = value;
        }
    }
    #endregion
}
