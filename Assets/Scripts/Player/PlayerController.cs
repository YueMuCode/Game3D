using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour,IDamageTable
{
    [Header("�Ų������������˵���Ч")]
    public AudioClip footClip;
    public AudioClip attackClip;
   // public AudioClip getHitClip;
    public AudioClip levelUpClip;
    public AudioClip dizzyClip;
    public AudioClip rollClip;
    public AudioClip jumpClip;
    public AudioClip landClip;

    
    public CharacterController character;    
    public Transform cam;
    public Transform groundCheck;
    public LayerMask layerMask;
    [Header("�ƶ��ٶȼ���ͷ��ת���ٶȵȲ���")]
    public float speed = 5f;
    public float maxSpeed = 10f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Vector3 rollDir;//�����ķ���
    public float attackDir;//�����ķ���
    public float rollSpeed;//�������ٶȣ�
    [Header("���������͵���ļ�����")]
    public float gravity = -9.8f;
    public Vector3 velocity = Vector3.zero;
    public float checkRadius = .01f;
    public float jumoHeight = 3f;
    private bool isGround;
    [Header("����Ķ�������")]
    public  Animator anim;
    float animSpeed;
    public bool isInputBlocked = true;
    public bool isMove;
    [Header("����Ļ�������")]
    public CharacterStats characterStats;
    public bool isDead;
    public bool isCritical;//�Ƿ񱩻�
    public bool isAttack;//�Ƿ����ڹ���
    //����UI�õ�ί��
    public event Action<int> UpdatePlayerHealth;//���ڸ���Ѫ��
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
        GameManager.Instance.RigisterPlayer(characterStats);//�����������ע���manager
    }
    void Update()
    {
        isDead = characterStats.currentHealth <= 0;
        attackDir = cam.eulerAngles.y;
        if (!isDead)
        {
            updateAttackStats();
            PlayerMove();
            
        }
        PlayerAnimation();
        if (isDead)
        {
            GameManager.Instance.NotifyObserver();//��ʼ�㲥
            
        }
        isLevelUp();
        UpdatePlayerHealth?.Invoke(1);//��Ϊhealthbar�ĳ�ʼ�����ܹ�ֱ����start�У��Ǵ����ִ��˳���������bug��ϸ��¼��ȥ��3.28�յıʼ�
        
    }
    void PlayerMove()//�������������������·���ܣ��Լ���������Ч����Ӹ����ʹ�������ƶ���ʱ���ܹ�����
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
          
        
        //if (isGround && Input.GetButtonDown("Jump"))
        //{
        //    //PlayerAnimation()��ʵ��
        //}
        if (isInputBlocked)
        {
             horizontal = Input.GetAxisRaw("Horizontal");
             vertical = Input.GetAxisRaw("Vertical");
        }
       
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;//��ȡĿ�귽��
            //float angle = ;        
            transform.rotation = Quaternion.Euler(0f, Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime), 0f);//����������ҳ�ָ���ķ�����ת��
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rollDir = moveDir;    
            character.Move(moveDir.normalized * speed * Time.deltaTime);//��������ƶ�����Ϊ�·��ĺ���ʹ����movdToWards+����,���ǳ�������ķ���ƽ�ƣ���ô������ÿɲ���

            isMove = true;
        }else
        {
            isMove = false;
        }
    }
    void updateAttackStats()//ʵ�ֽ�ɫ��������
    {
        anim.SetFloat("AttackTime", Mathf.Repeat(anim.GetCurrentAnimatorStateInfo(2).normalizedTime, 1f));
        anim.ResetTrigger("Attack");
    }
    void PlayerAnimation()//Ŀǰ�������������·����Ծ������������Ծ����
    {
        //ʵ����������ߺͱ���
        float deSpeed = 0f;
        if (isMove&& !Input.GetKeyDown(KeyCode.LeftShift))
        {
           
            deSpeed = speed;

        }
        if (Input.GetKey(KeyCode.LeftShift)&&isMove)
        {
            deSpeed = maxSpeed;
            //Debug.Log("������shift");
        }
        animSpeed = Mathf.MoveTowards(animSpeed, deSpeed,Time.deltaTime*20);
        anim.SetFloat("Speed", animSpeed);//
        //ʵ������ķ���
        if(Input.GetKeyDown(KeyCode.LeftControl)&&isInputBlocked)
        {
            rollSpeed = 200f;
            anim.SetTrigger("Roll");
            character.Move( rollDir * rollSpeed*Time.deltaTime);
            AudioManager.Instance.PlayClip(landClip);
        }
        isGround = Physics.CheckSphere(groundCheck.position, checkRadius, layerMask);
        if (isGround && Input.GetButtonDown("Jump")&&isInputBlocked)
        {
            velocity.y = Mathf.Sqrt(jumoHeight * -2 * gravity);
            character.Move(velocity * Time.deltaTime);
            AudioManager.Instance.PlayClip(jumpClip);
            anim.SetTrigger("Jump");
        }
        if(isGround)
        {

            anim.SetBool("Ground", isGround);
            //AudioManager.Instance.PlayClip(landClip);


        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Land")
            ||anim.GetCurrentAnimatorStateInfo(1).IsName("Hit")
            ||anim.GetCurrentAnimatorStateInfo(2).IsName("Attack")
            || anim.GetCurrentAnimatorStateInfo(2).IsName("Attack2")
            || anim.GetCurrentAnimatorStateInfo(2).IsName("Attack3")
            || anim.GetCurrentAnimatorStateInfo(2).IsName("CriticalAttack")
            || anim.GetCurrentAnimatorStateInfo(1).IsName("Dizzy")
            ||anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
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
        if(Input.GetKeyDown(KeyCode.Mouse0)&&!InteractWithUI()&&!isDead)
        {
            //  Debug.Log("������������");
            transform.rotation = Quaternion.Euler(0f, Mathf.SmoothDampAngle(transform.eulerAngles.y, attackDir, ref turnSmoothVelocity, 0f), 0f);
           // Debug.Log(attackDir);
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
            //Debug.Log("������");

            temp = characterStats.needExp;
            characterStats.level += 1;
            AudioManager.Instance.PlayClip(levelUpClip);//������������Ч
            characterStats.needExp += characterStats.level * 20;

            //���з����������������ȵ�
            characterStats.maxHealth += 50;
            characterStats.currentDefend += 1;
            characterStats.currentHealth = characterStats.maxHealth;
            characterStats.currentExp -=temp;
            

            UpdatePlayerHealth?.Invoke(1);
        }

    }
    #region ʵ�ֵĽӿ�
    public void GetHit(float damage,Transform attacker)
    {
        float realDamage = Mathf.Max(1, damage - damage * (characterStats.currentDefend / 10));
        //  AudioManager.Instance.PlayClip(getHitClip);
        AudioManager.Instance.PlayClip(dizzyClip);

        characterStats.currentHealth -= realDamage;   
        if(attacker.GetComponent<EnemyController>().isCritical&& !attacker.GetComponent<EnemyController>().skill)//Զ�̹������ܱ���
        {
            anim.SetTrigger("Hit");
            AudioManager.Instance.PlayClip(dizzyClip);
        }
        else
        {

        }
        UpdatePlayerHealth?.Invoke(1);
    }
    #endregion

    //�������ж�
    #region �˺����ж���Ϊ
    public void StartAttackCheckLeftHand()
    {
        if(FindObjectOfType<LHandHitPoint>())
        {
            FindObjectOfType<LHandHitPoint>().GetComponent<BoxCollider>().enabled = true;
           
        }
        AudioManager.Instance.PlayClip(attackClip);

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
        AudioManager.Instance.PlayClip(attackClip);
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



    bool InteractWithUI()//�Ƿ����ں�UI��������ֹ���UI��ʱ����й���
    {
        if(EventSystem.current!=null&&EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
     
    //�Ų�
   
    void RunFootLeft()
    {
        AudioManager.Instance.PlayClip(footClip);
    }
    void RunFootRight()
    {
        AudioManager.Instance.PlayClip(footClip);
    }
    void Land()
    {
        AudioManager.Instance.PlayClip(landClip);
    }
}
