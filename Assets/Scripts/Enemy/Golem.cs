using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : EnemyController
{
    public float kickForce = 10f;

    public GameObject rockPrefab;
    public Transform throwHandPos;

    public void ThrowEvent()
    {
        if(attackTarget!=null)
        {
           GameObject rock= Instantiate(rockPrefab, throwHandPos.position, Quaternion.identity);//手部产生预制体，旋转是石头的原本旋转。
            rock.GetComponent<Rock>().hitTarget = attackTarget;//将攻击目标传给石头的撞击目标

        }
         

    }
}
