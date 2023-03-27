using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterStats))]
public class EnemyController : MonoBehaviour,IEndGameObserver,IDamageTable
{
    protected NavMeshAgent agent;
    protected EnemyStates enemyState;//���˵ĵ�ǰ״̬
    public  Animator anim;
    public CharacterStats characterStats;

    [Header("���˵Ļ���������Ѳ�߷�Χ��")]
    public float sightRadius;//��Ұ��Χ
    public float patrolRadius;//Ѳ�߷�Χ
    public GameObject attackTarget;//����Ŀ��
    protected float normalSpeed;
    public bool isGUARD;//�ǲ���վ׮��
    protected Vector3 guardPos;//վ׮������
    public Quaternion guardRotation;
    protected Vector3 wayPoint;//�����Ѳ�ߵ�
    public float lookAtTime;
    public float remainLookAtTime;
    public float lastAttackTime=0f;//��һ�ι�����ʱ��
    

   [Header("���������Ĳ���")]
    public bool isChase;
    public bool isWalk;
    public bool isFollow;//�Ƿ�����ս
    public bool isCritical;//��ǰ�Ƿ񱩻�����Ҫ�͹��������еı����ʽ��м����ȡֵ��
    public bool isDead;

    [Header("��������")]
    bool playerIsDead = false;
    public bool skill;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        characterStats=GetComponent<CharacterStats>();
        guardPos = transform.position;//��ȡվ׮��ԭʼ����
        guardRotation = transform.rotation;
    }

    void Start()
    {
        normalSpeed = agent.speed;
        InitEnemyState();
    }

    void Update()
    {
        isDead = characterStats.currentHealth <= 0;
        if(!playerIsDead)
        {
            SwitchStates();
            SwitchAnimation();
            lastAttackTime -= Time.deltaTime;//���ϼ�ʱ
        }
       
    }


    #region ���˽��벻ͬ״̬�µĴ���+���˵Ķ���
  public virtual bool isFoundPlayer()//���ڼ���Ƿ�����ҽ���ִ��״̬����ز���
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
        characterStats.currentHealth = characterStats.maxHealth;
        GameManager.Instance.AddObserver(this);
    }
    void SwitchAnimation()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Chase", isChase);
        anim.SetBool("Follow", isFollow);
        anim.SetBool("Critical", isCritical);
        anim.SetBool("Dead", isDead);

    }
   

    void SwitchStates()//��ɵ��˵ĸ���״̬���л�
    {
        if(isDead)
        {
            enemyState = EnemyStates.DEAD;
        }

         else  if(isFoundPlayer())
        {
            enemyState = EnemyStates.CHASE; 
          //  Debug.Log("������ң�");
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
        isChase = false;
        if (transform.position != guardPos)
        {
            isWalk = true;
            agent.isStopped = false;
            agent.destination = guardPos;
            if (Vector3.SqrMagnitude(guardPos - transform.position) <= agent.stoppingDistance)
            {
                isWalk = false;
                transform.rotation = Quaternion.Lerp(transform.rotation, guardRotation, .01f);
            }
        }
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
            agent.isStopped = false;
            agent.destination = attackTarget.transform.position;
            
        }
        if(TargetInAttackRange()||TargetInSkillRange())
        {
            isFollow = false;
            agent.isStopped = true;
            Debug.Log("ͣ�£�");
            agent.velocity = Vector3.zero;//��ֹ�ٶȹ��ߵ�ʱ����й�����ʱ��Ử����
            if (lastAttackTime<=0)
            {
                
                lastAttackTime = characterStats.coolDown;//���ù�������ȴʱ��
                isCritical = Random.value < characterStats.criticalChance;//��ȡ�Ƿ񱩻���ֵ
                Attack();
            }
        }





    }
    void DEAD()
    {
        GetComponent<Collider>().enabled = false;
        agent.radius = 0;
        Destroy(this.gameObject, 2f);
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

    #region ���˵Ĺ����жϴ����ȵ�
    void Attack()
    {
        transform.LookAt (attackTarget.transform.position);//���������
       
        if(TargetInAttackRange())
        {
            
            anim.SetTrigger("Attack");//����������״̬��ʵ��
            skill = false;
            Debug.Log("ִ��һ�ι���");
        }
       else  if (TargetInSkillRange())
        {
            anim.SetTrigger("Skill");
            skill = true;
            Debug.Log("������Զ�̹����ķ�Χ");
        }
    }



   public  bool TargetInAttackRange()//�ж��Ƿ��ڹ�����Χ��
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, this.transform.position) <= characterStats.attackRange;//.attackRange
        }
        else
        {
            return false;
        }
    }
   public  bool TargetInSkillRange()//�ж��Ƿ���Զ�̹�����Χ��
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, this.transform.position) <= characterStats.skillRange;
        }
        else
        {
            return false;
        }
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    #region ʵ�ֵĽӿ�
    void OnEnable()//����ͳ�Ϊ�˹۲���
    {
        
    }
    //void OnDisable()
    //{
    //    GameManager.Instance.RemoveObserver(this);
    //}
    public void EndNotify()//IEndGameObsetver�۲���ģʽ
    {
        //��ʤ
        //ֹͣ�
        //����ʤ���Ķ���
        //���֣�
        playerIsDead = true;
        isChase = false;
        isFollow = false;
        attackTarget = null;
        anim.SetBool("Win", true);
    }

    public void GetHit(float damage,Transform attacker)
    {
        //Debug.Log("�ܵ��˺�");
        characterStats.currentHealth -= damage;

        anim.SetTrigger("Hit");
        //if(anim.GetCurrentAnimatorStateInfo(2).IsName("Dizzy"))
        //{
        //    Debug.Log("���ڲ���ѣ�ζ���");
        //    anim.SetBool("Dizzy", false);
        //}
        //else
        //{
           
        //}
        //anim.SetBool("Dizzy", false);
       
    }
    #endregion


}
