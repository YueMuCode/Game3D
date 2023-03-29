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
        switch (transitionPoint.transitionType)//检查传送的类型
        {
            case TransitionPoint.TransitionType.SameScene://如果是同场景内传送
                Debug.Log(1);
                TransitionSameScene(transitionPoint.destinationTag);
                break;
            case TransitionPoint.TransitionType.DifferentScene://如果是跨场景传送
                Debug.Log(2);
               StartCoroutine( TransitionDifferentScene(transitionPoint.sceneName, transitionPoint.destinationTag));
                Debug.Log(2);
                break;
        }
    }


    void TransitionSameScene(TransitionDestination.DestinationTag destinationtag)//同场景内的传送
    {
         player = GameManager.Instance.playerStats.gameObject.GetComponent<PlayerController>().gameObject;//之前把playerstats给了gamemanager这样就可以直接用组件找对象。也可以直接find（注意这里的playerstats本质上是组件）

       
        player.GetComponent<CharacterController>().enabled = false;//先把控制移动的组件关闭先，原因不能确定，应该是跟更新位置的逻辑有关
        player.transform.SetPositionAndRotation(GetDestinationPosition(destinationtag).transform.position, GetDestinationPosition(destinationtag).transform.rotation);
        player.GetComponent<CharacterController>().enabled = true;
       // Debug.Log(GetDestinationPosition(destinationtag).transform.position);
     //   Debug.Log(player.transform.position);

        
    }
    IEnumerator TransitionDifferentScene(string sceneName, TransitionDestination.DestinationTag destinationtag)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);//加载场景
        if(FindObjectOfType<PlayerController>())//方便调试用
        {
            Destroy(FindObjectOfType<PlayerController>().gameObject);
        }
            yield return Instantiate(playerPrefabs, GetDestinationPosition(destinationtag).transform.position, GetDestinationPosition(destinationtag).transform.rotation);//加载完场景后再生成玩家的预制体
        yield break;
    }
    public TransitionDestination GetDestinationPosition(TransitionDestination.DestinationTag destinationtag)
    {
        var allDestination = FindObjectsOfType<TransitionDestination>();
        foreach(var destination in allDestination)
        {
            if(destination.destinationTag == destinationtag)
            {
               // Debug.Log("获取坐标成功");
                return destination;
                
            }
        }


        return null;
    }
}
