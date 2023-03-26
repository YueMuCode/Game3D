using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleT<T> : MonoBehaviour where T :SingleT<T>//Լ������
{
    private static T instance;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
            
        }
    }
    public static bool IsInitialized
    {
        get
        {
            return instance != null;//�ж��Ƿ��Ѿ������˵�����
        }
    }
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
