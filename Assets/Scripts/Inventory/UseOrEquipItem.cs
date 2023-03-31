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
       
        //������Ĺ������Լ��������Ĺ�������
    }

    public void UnEquipWeapon(ItemData_SO weapon)
    {
        if(parentOfWeapon.transform.childCount>=7)//����ģ��ԭ������6��,һֱ��������װ������ֺܶ�����������Ȼ������װ��������ʱ���Ż������������������ʱ��Ҳ����һ��UNEquipment
        {
            for(int i=6;i<parentOfWeapon.transform.childCount;i++)
            {
                Destroy(parentOfWeapon.transform.GetChild(i).gameObject);
            }
           
        }
        GetComponent<Animator>().runtimeAnimatorController = temp;
        //��������Ĺ�������
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
