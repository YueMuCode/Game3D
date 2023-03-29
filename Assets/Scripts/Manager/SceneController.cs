using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : SingleT<SceneController>
{
    public GameObject player;
    public GameObject playerPrefabs;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void TransitionToDestination(TransitionPoint transitionPoint)
    {
        switch (transitionPoint.transitionType)//��鴫�͵�����
        {
            case TransitionPoint.TransitionType.SameScene://�����ͬ�����ڴ���
                Debug.Log(1);
                TransitionSameScene(transitionPoint.destinationTag);
                break;
            case TransitionPoint.TransitionType.DifferentScene://����ǿ糡������
                Debug.Log(2);
               StartCoroutine( TransitionDifferentScene(transitionPoint.sceneName, transitionPoint.destinationTag));
                Debug.Log(2);
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
    IEnumerator TransitionDifferentScene(string sceneName, TransitionDestination.DestinationTag destinationtag)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);//���س���
        if(FindObjectOfType<PlayerController>())//���������
        {
            Destroy(FindObjectOfType<PlayerController>().gameObject);
        }
            yield return Instantiate(playerPrefabs, GetDestinationPosition(destinationtag).transform.position, GetDestinationPosition(destinationtag).transform.rotation);//�����곡������������ҵ�Ԥ����
        yield break;
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
