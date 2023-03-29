using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        SameScene, DifferentScene
    }
    [Header("传送门信息")]
    public string sceneName;
    public TransitionType transitionType;

    public TransitionDestination.DestinationTag destinationTag;//这个在编辑器界面赋值了

    private bool canTransition;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTransition = true;
            //提示UI            
            Debug.Log("进入了传送门");
            

        }

    }
    private void OnTriggerExit(Collider other)//这段代码在传送的时候完全不能执行，只有传送完走出才会执行所以cantransition在传送后不能更新，再按下q就会发生传送两次的现象即不会传送
    {
        if (other.CompareTag("Player"))
        {
            canTransition = false;
            Debug.Log("没有执行到");
        }
    }
    public void StartTransition()
    {
        if(Input.GetKeyDown(KeyCode.E)&&canTransition)
        {
           // Debug.Log("你按下了Q");
            SceneController.Instance.TransitionToDestination(this);
            canTransition = false;
            //Debug.Log(canTransition);
        }
    }
    private void Update()
    {
        StartTransition();
    }

}
