using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : SingleT<SaveManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.S))
        {
            SavePlayerData();
            
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPlayerData();
        }
        
    }




   public  void SavePlayerData()
    {     
        SaveCurrentData(GameManager.Instance.playerStats.name, GameManager.Instance.playerStats.characterData_so);       
    }

   public  void LoadPlayerData()
    {

        LoadData(GameManager.Instance.playerStats.name, GameManager.Instance.playerStats.characterData_so);
                
    }



    void SaveCurrentData(string keyName, Object data)//��data���һ���Ѿ�ʵ�л��Ķ�����ʵ����CharacterData_SO���洴��ŵ���ҵ����ݣ�����ҵ�������key����ʶ���浽Ӳ�����棨����һ�����������Ӧ�����ڶ�ջ����
    {
        var jsonData = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(keyName, jsonData);
        PlayerPrefs.Save();
    }
    void LoadData( string keyName, Object data)
    {
        if(PlayerPrefs.HasKey(keyName))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(keyName), data);//��Ӳ����������ݶ�ȡ���˵�ǰ�����ָ���Ķ�������data���棬data����һ������
        }
    }
}
