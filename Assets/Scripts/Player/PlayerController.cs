using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour,IDamageTable
{
    
    public CharacterController character;    
    public Transform cam;
    public Transform groundCheck;
    public LayerMask layerMask;
    [Header("移动速度及镜头旋转的速度等参数")]
    public float speed = 5f;
    public float maxSpeed = 10f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    [Header("重力参数和地面的检测参数")]
    public float gravity = -9.8f;
    public Vector3 velocity = Vector3.zero;
    public float checkRadius = .01f;
    public float jumoHeight = 3f;
    private bool isGround;
    [Header("人物的动画变量")]
    public  Animator anim;
    float animSpeed;
    public bool isInputBlocked = true;
    public bool isMove;
    [Header("人物的基本属性")]
    public CharacterStats characterStats;
    public bool isDead;
    public bool isCritical;//是否暴击
    public bool isAttack;//是否正在攻击
    //更新UI用的委托
    public event Action<int> UpdatePlayerHealth;//用于更新血条
    void InitPlayerStats()
    {
        characterStats.maxHealth = 100;
        characterStats.currentHealth = characterStats.maxHealth;
        characterStats.currentExp = 0;
        characterStats.needExp = 10;
        characterStats.level = 1;
        characterStats.maxDefend = 1;
        characterStats.currentDefend = characterStats.maxDefend;
        cam = Camera.main.transform;
       
    }    

    void Start()
    {
        anim = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        InitPlayerStats();
  
        GameManager.Instance.RigisterPlayer(characterStats);//将人物的数据注册给manager
    }
    void Update()
    {
        isDead = characterStats.currentHealth <= 0;
        updateAttackStats();
        PlayerMove();
        PlayerAnimation();
        if (isDead)
        {
            GameManager.Instance.NotifyObserver();//开始广播
        }
        isLevelUp();
        UpdatePlayerHealth?.Invoke(1);//因为healthbar的初始化不能够直接在start中，是代码的执行顺序问题这个bug详细记录，去看3.28日的笔记

    }
    void PlayerMove()//这个函数完成了人物的跑路功能，以及将重力的效果添加给人物，使人物在移动的时候能够下落
    {
        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);
        isGround = Physics.CheckSphere(groundCheck.position, checkRadius, layerMask);
        float horizontal=0f;
        float vertical=0f;
        if (isGround && velocity.y < 0)
        {
            velocity.y = 0;
        }
        if (isGround && Input.GetButtonDown("Jump"))
        {
            //PlayerAnimation()
        }
        if (isInputBlocked)
        {
             horizontal = Input.GetAxisRaw("Horizontal");
             vertical = Input.GetAxisRaw("Vertical");
        }
       
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            character.Move(moveDir.normalized * speed * Time.deltaTime);//,因为下方的函数使用了movdToWards,就是朝中面向的方向平移，那么这里可用可不用

            isMove = true;
        }else
        {
            isMove = false;
        }
        

    }
    void updateAttackStats()
    {
        anim.SetFloat("AttackTime", Mathf.Repeat(anim.GetCurrentAnimatorStateInfo(2).normalizedTime, 1f));
        anim.ResetTrigger("Attack");
    }
    void PlayerAnimation()//目前这个函数触发跑路、跳跃动画，触发跳跃功能
    {

        // Vector3 horizontalVelocity = new Vector3(character.velocity.x, 0, character.velocity.z);
        float deSpeed = 0f;
        if (isMove&& !Input.GetKeyDown(KeyCode.LeftShift))
        {
           
            deSpeed = speed;

        }
        if (Input.GetKey(KeyCode.LeftShift)&&isMove)
        {
            deSpeed = maxSpeed;
            Debug.Log("按下了shift");
        }
        animSpeed = Mathf.MoveTowards(animSpeed, deSpeed,Time.deltaTime*20);;
        anim.SetFloat("Speed", animSpeed);//
        isGround = Physics.CheckSphere(groundCheck.position, checkRadius, layerMask);
        if (isGround && Input.GetButtonDown("Jump")&&isInputBlocked)
        {
            velocity.y = Mathf.Sqrt(jumoHeight * -2 * gravity);
            anim.SetTrigger("Jump");
        }
        if(isGround)
        {

            anim.SetBool("Ground", isGround);
            
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Land")||anim.GetCurrentAnimatorStateInfo(1).IsName("Hit")||anim.GetCurrentAnimatorStateInfo(2).IsName("Attack")
            || anim.GetCurrentAnimatorStateInfo(2).IsName("CriticalAttack")|| anim.GetCurrentAnimatorStateInfo(1).IsName("Dizzy"))
        {           
            isInputBlocked = false;
            isAttack = true;
        }
        else
        {
            isInputBlocked = true;
            isAttack = false;
        }
        anim.SetBool("Dead", isDead);
        if(Input.GetKeyDown(KeyCode.Mouse0)&&!InteractWithUI())
        {
            //  Debug.Log("按下了鼠标左键");
            anim.SetTrigger("Attack");
            isCritical = UnityEngine.Random.value <= characterStats.criticalChance;
            UpdatePlayerHealth?.Invoke(1);
        }
        anim.SetBool("Critical", isCritical);
    }
    void isLevelUp()
    {
        int temp;
        if(characterStats.level<characterStats.maxLevel&&characterStats.currentExp>=characterStats.needExp)
        {
            Debug.Log("升级！");
            temp = characterStats.needExp;
            characterStats.level += 1;
            characterStats.needExp += characterStats.level * 20;

            //进行防御、攻击的升级等等
            characterStats.maxHealth += 50;
            characterStats.currentDefend += 1;
            characterStats.currentHealth = characterStats.maxHealth;
            characterStats.currentExp -=temp;
            

            UpdatePlayerHealth?.Invoke(1);
        }

    }
    #region 实现的接口
    public void GetHit(float damage,Transform attacker)
    {
        float realDamage = Mathf.Max(1, damage - damage * (characterStats.currentDefend / 10));



        characterStats.currentHealth -= realDamage;   
        if(attacker.GetComponent<EnemyController>().isCritical&& !attacker.GetComponent<EnemyController>().skill)//远程攻击不能暴击
        {
            anim.SetTrigger("Hit");
        }
        else
        {

        }
        UpdatePlayerHealth?.Invoke(1);
    }
    #endregion

    //攻击的判定
    #region 伤害的判断因为
    public void StartAttackCheckLeftHand()
    {
        if(FindObjectOfType<LHandHitPoint>())
        {
            FindObjectOfType<LHandHitPoint>().GetComponent<BoxCollider>().enabled = true;
          
        }
       
    }
    public void EndAttackCheckLeftHand()
    {
       
        if (FindObjectOfType<LHandHitPoint>())
        {
            FindObjectOfType<LHandHitPoint>().GetComponent<BoxCollider>().enabled = false;
           
        }
    }

    public void StartAttackCheckRightHand()
    {
        if (FindObjectOfType<RHandHitPoint>())
        {
            FindObjectOfType<RHandHitPoint>().GetComponent<BoxCollider>().enabled = true;

        }
    }
    public void EndAttackCheckRightHand()
    {

        if (FindObjectOfType<RHandHitPoint>())
        {
            FindObjectOfType<RHandHitPoint>().GetComponent<BoxCollider>().enabled = false;

        }
    }


    public void StartAttackCheckWeaponRightHand()
    {
        if (FindObjectOfType<WeaponHitPoint>())
        {
            FindObjectOfType<WeaponHitPoint>().GetComponent<BoxCollider>().enabled = true;

        }

    }
    public void EndAttackCheckWeaponRightHand()
    {

        if (FindObjectOfType<WeaponHitPoint>())
        {
            FindObjectOfType<WeaponHitPoint>().GetComponent<BoxCollider>().enabled = false;

        }
    }
    #endregion



    bool InteractWithUI()//是否正在和UI互动，防止点击UI的时候进行攻击
    {
        if(EventSystem.current!=null&&EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
  
}
