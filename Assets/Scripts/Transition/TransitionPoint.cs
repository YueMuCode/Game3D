using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        SameScene, DifferentScene
    }
    [Header("��������Ϣ")]
    public string sceneName;
    public TransitionType transitionType;

    public TransitionDestination.DestinationTag destinationTag;//����ڱ༭�����渳ֵ��

    private bool canTransition;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTransition = true;
            //��ʾUI            
            Debug.Log("�����˴�����");
            

        }

    }
    private void OnTriggerExit(Collider other)//��δ����ڴ��͵�ʱ����ȫ����ִ�У�ֻ�д������߳��Ż�ִ������cantransition�ڴ��ͺ��ܸ��£��ٰ���q�ͻᷢ���������ε����󼴲��ᴫ��
    {
        if (other.CompareTag("Player"))
        {
            canTransition = false;
            Debug.Log("û��ִ�е�");
        }
    }
    public void StartTransition()
    {
        if(Input.GetKeyDown(KeyCode.E)&&canTransition)
        {
           // Debug.Log("�㰴����Q");
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
