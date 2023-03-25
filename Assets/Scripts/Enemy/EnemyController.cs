using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyStates enemyState;//敌人的当前状态
    private Animator anim;
    [Header("敌人的基本参数：巡逻范围等")]
    public float sightRadius;//视野范围
    public float patrolRadius;//巡逻范围
    private GameObject attackTarget;//攻击目标
    private float normalSpeed;
    public bool isGUARD;//是不是站桩怪
    private Vector3 guardPos;//站桩的坐标
    private Vector3 wayPoint;//随机的巡逻点
    public float lookAtTime;
    public float remainLookAtTime;
    [Header("触发动画的参数")]
    public bool isChase;
    public bool isWalk;
    public bool isFollow;//是否被拉脱战
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        guardPos = transform.position;//获取站桩的原始坐标
    }

    void Start()
    {
        normalSpeed = agent.speed;
        InitEnemyState();
    }

    void Update()
    {
        SwitchStates();
        SwitchAnimation();
    }


    #region 敌人进入不同状态下的代码+敌人的动画
    bool isFoundPlayer()//用于检测是否发现玩家进而执行状态的相关操作
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRadius);//检测圆圈范围内的的collider，如果是玩家就进行发现操作
        foreach (Collider target in colliders)
        {
            if (target.transform.CompareTag("Player"))
            {
                attackTarget = target.transform.gameObject;//将获取的玩家对象赋值给attackTarget变量
                return true;
            }

        }
        attackTarget = null;
        return false;
    }
    void InitEnemyState()//初始化怪物的原始状态
    {
        if(isGUARD)
        {
            enemyState = EnemyStates.GUARD;//站桩
        }else
        {
            enemyState = EnemyStates.PATROL;//巡逻
            GetNewWayPoint();
        }
    }
    void SwitchAnimation()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Chase", isChase);
        anim.SetBool("Follow", isFollow);
    }
   

    void SwitchStates()//完成敌人的各种状态的切换
    {
        if(isFoundPlayer())
        {
            enemyState = EnemyStates.CHASE; 
            Debug.Log("发现玩家！");
        }
        switch(enemyState)
        {
            case EnemyStates.GUARD:
                GUARD();
                break;
            case EnemyStates.PATROL:
                PATROL();
                break;
            case EnemyStates.CHASE:
                CHASE();
                break;
            case EnemyStates.DEAD:
                DEAD();
                break;
        }
    }


    void GUARD()
    {

    }
    void PATROL()
    {
        isChase = false;
        isFollow = false;
        agent.speed = normalSpeed * 0.5f;
       
        if (Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance) //判断自身位置和巡逻的目标位置
        {

            isWalk = false;
            
            if (remainLookAtTime > 0)//让怪物停下来一丢丢时间然后开始继续巡逻
            {
                remainLookAtTime -= Time.deltaTime;
            }
            else
            {
                GetNewWayPoint();
            }



        }
        else
        {
            isWalk = true;
            agent.destination = wayPoint;
        }
    }
    void CHASE()
    {
        isWalk = false;//结束巡逻状态
        isChase = true;//进入追击
        agent.speed = normalSpeed;
        if(!isFoundPlayer())
        {
            //TODO回到上一个状态可能是站桩也可能是巡逻
            if (remainLookAtTime > 0)
            {
                agent.destination = this.transform.position;
                remainLookAtTime -= Time.deltaTime;
            }
            else if (isGUARD)
            {
                enemyState = EnemyStates.GUARD;
            }else
            {
                enemyState = EnemyStates.PATROL;
            }
            isFollow = false;//玩家脱战
            agent.destination = transform.position;//脱离玩家就停在原地
        }
        else
        {
            isFollow = true;//开始跟随玩家
            agent.destination = attackTarget.transform.position;
        }
    }
    void DEAD()
    {

    }

    void GetNewWayPoint()//获取随机的巡逻点
    {
        remainLookAtTime = lookAtTime;
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);
        Vector3 randomPoint = new Vector3(randomX + guardPos.x, this.transform.position.y, randomZ + guardPos.z);
        NavMeshHit hit;
        //判断这个选择的点是否是烘焙walkable的点，不是则返回原来的位置，然后重新找点。
        wayPoint = NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, 1) ? hit.position : this.transform.position;
    }
    #endregion



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}
