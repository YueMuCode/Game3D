using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleT<T> : MonoBehaviour where T :SingleT<T>//约束类型
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
            return instance != null;//判断是否已经生成了单例。
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
