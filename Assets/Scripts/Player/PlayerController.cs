using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IDamageTable
{
    public CharacterController character;    
    public Transform cam;
    public Transform groundCheck;
    public LayerMask layerMask;
    [Header("�ƶ��ٶȼ���ͷ��ת���ٶȵȲ���")]
    public float speed = 5f;
    public float maxSpeed = 10f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
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
    void InitPlayerStats()
    {
        characterStats.maxHealth = 100;
        characterStats.currentHealth = characterStats.maxHealth;
        characterStats.maxDefend = 3;
        characterStats.currentDefend = characterStats.maxDefend;
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
        isCritical = Random.value <= characterStats.criticalChance;
        PlayerMove();
        PlayerAnimation();
        if (isDead)
        {
            GameManager.Instance.NotifyObserver();//��ʼ�㲥
        }
       
        
    }
    void PlayerMove()//�������������������·���ܣ��Լ���������Ч�����Ӹ����ʹ�������ƶ���ʱ���ܹ�����
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
            character.Move(moveDir.normalized * speed * Time.deltaTime);//,��Ϊ�·��ĺ���ʹ����movdToWards,���ǳ�������ķ���ƽ�ƣ���ô������ÿɲ���

            isMove = true;
        }else
        {
            isMove = false;
        }
        

    }
    void PlayerAnimation()//Ŀǰ�������������·����Ծ������������Ծ����
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
            Debug.Log("������shift");
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
            || anim.GetCurrentAnimatorStateInfo(2).IsName("CriticalAttack"))
        {           
            isInputBlocked = false;
        }
        else
        {
            isInputBlocked = true;
        }
        anim.SetBool("Dead", isDead);
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
          //  Debug.Log("������������");
            anim.SetTrigger("Attack");
        }
        anim.SetBool("Critical", isCritical);
    }

    #region ʵ�ֵĽӿ�
    public void GetHit(float damage,Transform attacker)
    {
        characterStats.currentHealth -= damage;   
        if(attacker.GetComponent<EnemyController>().isCritical)
        {
            anim.SetTrigger("Hit");
        }
    }
    #endregion
}