using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePoolController : SingleT<SlimePoolController>
{
    public bool isDead;
    public float time = 3f;
    GameObject temp;
    void Start()
    {
       SlimeObjectPool.Instance.GetSlime();
       
    }

    // Update is called once per frame
    void Update()
    {
       if(isDead)
        {
            time -= Time.deltaTime;
            if(time<0)
            {
                SlimeObjectPool.Instance.GetSlime();
                Debug.Log(2);
                isDead = false;
            }
        }

    }
}
