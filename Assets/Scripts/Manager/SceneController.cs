using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : SingleT<SceneController>
{
    public GameObject player;
    public void TransitionToDestination(TransitionPoint transitionPoint)
    {
        switch (transitionPoint.transitionType)//��鴫�͵�����
        {
            case TransitionPoint.TransitionType.SameScene://�����ͬ�����ڴ���
                Debug.Log(1);
                TransitionSameScene(transitionPoint.destinationTag);
                break;
            case TransitionPoint.TransitionType.DifferentScene://����ǿ糡������
               
                break;
        }
    }


    void TransitionSameScene(TransitionDestination.DestinationTag destinationtag)//ͬ�����ڵĴ���
    {
         player = GameManager.Instance.playerStats.gameObject.GetComponent<PlayerController>().gameObject;//֮ǰ��playerstats����gamemanager�����Ϳ���ֱ��������Ҷ���Ҳ����ֱ��find��ע�������playerstats�������������

       
        player.GetComponent<CharacterController>().enabled = false;//�Ȱѿ����ƶ�������ر��ȣ�ԭ����ȷ����Ӧ���Ǹ�����λ�õ��߼��й�
        player.transform.SetPositionAndRotation(GetDestinationPosition(destinationtag).transform.position, GetDestinationPosition(destinationtag).transform.rotation);
        player.GetComponent<CharacterController>().enabled = true;
       // Debug.Log(GetDestinationPosition(destinationtag).transform.position);
     //   Debug.Log(player.transform.position);

        
    }

    public TransitionDestination GetDestinationPosition(TransitionDestination.DestinationTag destinationtag)
    {
        var allDestination = FindObjectsOfType<TransitionDestination>();
        foreach(var destination in allDestination)
        {
            if(destination.destinationTag == destinationtag)
            {
               // Debug.Log("��ȡ����ɹ�");
                return destination;
                
            }
        }


        return null;
    }
}
