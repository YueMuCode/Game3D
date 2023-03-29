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



    void SaveCurrentData(string keyName, Object data)//将data理解一个已经实列化的对象，其实就是CharacterData_SO里面创造号的玩家的数据，将玩家的数据用key来标识保存到硬盘里面（本来一个对象的数据应该是在堆栈区）
    {
        var jsonData = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(keyName, jsonData);
        PlayerPrefs.Save();
    }
    void LoadData( string keyName, Object data)
    {
        if(PlayerPrefs.HasKey(keyName))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(keyName), data);//将硬盘里面的数据读取到了当前程序的指定的对象里面data里面，data理解成一个对象
        }
    }
}
