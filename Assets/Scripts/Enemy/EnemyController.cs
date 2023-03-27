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
    protected EnemyStates enemyState;//敌人的当前状态
    public  Animator anim;
    public CharacterStats characterStats;

    [Header("敌人的基本参数：巡逻范围等")]
    public float sightRadius;//视野范围
    public float patrolRadius;//巡逻范围
    public GameObject attackTarget;//攻击目标
    protected float normalSpeed;
    public bool isGUARD;//是不是站桩怪
    protected Vector3 guardPos;//站桩的坐标
    public Quaternion guardRotation;
    protected Vector3 wayPoint;//随机的巡逻点
    public float lookAtTime;
    public float remainLookAtTime;
    public float lastAttackTime=0f;//下一次攻击的时间
    

   [Header("触发动画的参数")]
    public bool isChase;
    public bool isWalk;
    public bool isFollow;//是否被拉脱战
    public bool isCritical;//当前是否暴击（需要和攻击数据中的暴击率进行计算获取值）
    public bool isDead;

    [Header("其他参数")]
    bool playerIsDead = false;
    public bool skill;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        characterStats=GetComponent<CharacterStats>();
        guardPos = transform.position;//获取站桩的原始坐标
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
            lastAttackTime -= Time.deltaTime;//不断计时
        }
       
    }


    #region 敌人进入不同状态下的代码+敌人的动画
  public virtual bool isFoundPlayer()//用于检测是否发现玩家进而执行状态的相关操作
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
   

    void SwitchStates()//完成敌人的各种状态的切换
    {
        if(isDead)
        {
            enemyState = EnemyStates.DEAD;
        }

         else  if(isFoundPlayer())
        {
            enemyState = EnemyStates.CHASE; 
          //  Debug.Log("发现玩家！");
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
            agent.isStopped = false;
            agent.destination = attackTarget.transform.position;
            
        }
        if(TargetInAttackRange()||TargetInSkillRange())
        {
            isFollow = false;
            agent.isStopped = true;
            Debug.Log("停下！");
            agent.velocity = Vector3.zero;//防止速度过高的时候进行攻击的时候会滑动！
            if (lastAttackTime<=0)
            {
                
                lastAttackTime = characterStats.coolDown;//重置攻击的冷却时间
                isCritical = Random.value < characterStats.criticalChance;//获取是否暴击的值
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

    #region 敌人的攻击判断触发等等
    void Attack()
    {
        transform.LookAt (attackTarget.transform.position);//先面向敌人
       
        if(TargetInAttackRange())
        {
            
            anim.SetTrigger("Attack");//攻击不能用状态来实现
            skill = false;
            Debug.Log("执行一次攻击");
        }
       else  if (TargetInSkillRange())
        {
            anim.SetTrigger("Skill");
            skill = true;
            Debug.Log("进入了远程攻击的范围");
        }
    }



   public  bool TargetInAttackRange()//判断是否在攻击范围内
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
   public  bool TargetInSkillRange()//判断是否在远程攻击范围内
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

    #region 实现的接口
    void OnEnable()//激活就称为了观察者
    {
        
    }
    //void OnDisable()
    //{
    //    GameManager.Instance.RemoveObserver(this);
    //}
    public void EndNotify()//IEndGameObsetver观察者模式
    {
        //获胜
        //停止活动
        //播放胜利的动画
        //音乐？
        playerIsDead = true;
        isChase = false;
        isFollow = false;
        attackTarget = null;
        anim.SetBool("Win", true);
    }

    public void GetHit(float damage,Transform attacker)
    {
        //Debug.Log("受到伤害");
        characterStats.currentHealth -= damage;

        anim.SetTrigger("Hit");
        //if(anim.GetCurrentAnimatorStateInfo(2).IsName("Dizzy"))
        //{
        //    Debug.Log("正在播放眩晕动画");
        //    anim.SetBool("Dizzy", false);
        //}
        //else
        //{
           
        //}
        //anim.SetBool("Dizzy", false);
       
    }
    #endregion


}
