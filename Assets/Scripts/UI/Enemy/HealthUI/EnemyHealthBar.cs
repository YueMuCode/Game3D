using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    public GameObject healthBarUIPrafab;//��ȡUIԤ����
    public Transform cam;//��ȡ������ķ���
    Transform UITransform;//�õ����ɳ�����Ԥ�����canvastransformͨ������õ�slider������
    public Transform barPoint;
    public bool alwaysVisible=false;  //�Ƿ���һֱ��ʾ��
    public float visibleTime; //����ʱ�� 
    private float timeLeft;  //ʣ��Ŀ���ʾʱ��
    EnemyController enemyStats;
    private void Start()
    {
       
        cam= Camera.main.transform;
        enemyStats = GetComponent<EnemyController>();
        enemyStats.UpdateHealthBarOnAttack += UpdateHealth;
        InitInstantiateUI();
    }
    void InitInstantiateUI()
    {
        GameObject canvas = GameObject.Find("EnemyHealthBarCanvas");
        if(canvas)
        {
            UITransform = Instantiate(healthBarUIPrafab, canvas.transform).transform;//�ڶ���������ʾҪ����Ԥ�����������Ǹ�������棬�������������EnemyHealthBarCanvas�����Ϊ��������
            UITransform.gameObject.SetActive(alwaysVisible);
        }
    }

    private void Update()
    {
        
        if (enemyStats.isDead&&UITransform)
        {
            Destroy(UITransform.gameObject);
        }
        if(UITransform)
        {
            UITransform.forward = -cam.forward;
           //UpdateHealth(1);
        }
        
    }
    void UpdateHealth(int a)
    {
        UITransform.gameObject.SetActive(true);
        timeLeft = visibleTime;
        UITransform.GetComponent<Slider>().maxValue = enemyStats.characterStats.maxHealth;
        UITransform.GetComponent<Slider>().value = enemyStats.characterStats.currentHealth;
    }

    private void LateUpdate()
    {
        if(UITransform)
        {
            UITransform.position = barPoint.position;
            if (timeLeft <= 0 && !alwaysVisible)
            {
                UITransform.gameObject.SetActive(false);

            }
            else
            {
                timeLeft -= Time.deltaTime;
            }
        }
        

    }
}