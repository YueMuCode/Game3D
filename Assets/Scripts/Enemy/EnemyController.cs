using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyStates enemyState;//���˵ĵ�ǰ״̬
    private Animator anim;
    [Header("���˵Ļ���������Ѳ�߷�Χ��")]
    public float sightRadius;//��Ұ��Χ
    public float patrolRadius;//Ѳ�߷�Χ
    private GameObject attackTarget;//����Ŀ��
    private float normalSpeed;
    public bool isGUARD;//�ǲ���վ׮��
    private Vector3 guardPos;//վ׮������
    private Vector3 wayPoint;//�����Ѳ�ߵ�
    public float lookAtTime;
    public float remainLookAtTime;
    [Header("���������Ĳ���")]
    public bool isChase;
    public bool isWalk;
    public bool isFollow;//�Ƿ�����ս
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        guardPos = transform.position;//��ȡվ׮��ԭʼ����
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


    #region ���˽��벻ͬ״̬�µĴ���+���˵Ķ���
    bool isFoundPlayer()//���ڼ���Ƿ�����ҽ���ִ��״̬����ز���
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRadius);//���ԲȦ��Χ�ڵĵ�collider���������Ҿͽ��з��ֲ���
        foreach (Collider target in colliders)
        {
            if (target.transform.CompareTag("Player"))
            {
                attackTarget = target.transform.gameObject;//����ȡ����Ҷ���ֵ��attackTarget����
                return true;
            }

        }
        attackTarget = null;
        return false;
    }
    void InitEnemyState()//��ʼ�������ԭʼ״̬
    {
        if(isGUARD)
        {
            enemyState = EnemyStates.GUARD;//վ׮
        }else
        {
            enemyState = EnemyStates.PATROL;//Ѳ��
            GetNewWayPoint();
        }
    }
    void SwitchAnimation()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Chase", isChase);
        anim.SetBool("Follow", isFollow);
    }
   

    void SwitchStates()//��ɵ��˵ĸ���״̬���л�
    {
        if(isFoundPlayer())
        {
            enemyState = EnemyStates.CHASE; 
            Debug.Log("������ң�");
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
       
        if (Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance) //�ж�����λ�ú�Ѳ�ߵ�Ŀ��λ��
        {

            isWalk = false;
            
            if (remainLookAtTime > 0)//�ù���ͣ����һ����ʱ��Ȼ��ʼ����Ѳ��
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
        isWalk = false;//����Ѳ��״̬
        isChase = true;//����׷��
        agent.speed = normalSpeed;
        if(!isFoundPlayer())
        {
            //TODO�ص���һ��״̬������վ׮Ҳ������Ѳ��
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
            isFollow = false;//�����ս
            agent.destination = transform.position;//������Ҿ�ͣ��ԭ��
        }
        else
        {
            isFollow = true;//��ʼ�������
            agent.destination = attackTarget.transform.position;
        }
    }
    void DEAD()
    {

    }

    void GetNewWayPoint()//��ȡ�����Ѳ�ߵ�
    {
        remainLookAtTime = lookAtTime;
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);
        Vector3 randomPoint = new Vector3(randomX + guardPos.x, this.transform.position.y, randomZ + guardPos.z);
        NavMeshHit hit;
        //�ж����ѡ��ĵ��Ƿ��Ǻ決walkable�ĵ㣬�����򷵻�ԭ����λ�ã�Ȼ�������ҵ㡣
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
