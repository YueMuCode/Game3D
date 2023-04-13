using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    public GameObject healthBarUIPrafab;//获取UI预制体
    public Transform cam;//获取摄像机的方向
    public Transform UITransform;//拿到生成出来的预制体的canvastransform通过这个拿到slider子物体
    public Transform barPoint;
    public bool alwaysVisible=false;  //是否是一直显示的
    public float visibleTime; //可视时间 
    private float timeLeft;  //剩余的可显示时间
    EnemyController enemyStats;
    public bool isDead;
    private void Start()
    {
       
        cam= Camera.main.transform;
        enemyStats = GetComponent<EnemyController>();
        enemyStats.UpdateHealthBarOnAttack += UpdateHealth;
        InitInstantiateUI();
    }
    private void OnEnable()
    {
        //nitInstantiateUI();
    }
    void InitInstantiateUI()
    {
        GameObject canvas = GameObject.Find("EnemyHealthBarCanvas");
        if(canvas)
        {
            UITransform = Instantiate(healthBarUIPrafab, canvas.transform).transform;//第二个参数表示要将此预制体生成在那个组件里面，这里就是生成在EnemyHealthBarCanvas里面成为其子物体
            UITransform.gameObject.SetActive(alwaysVisible);
        }
    }

    private void Update()
    {
      //  Debug.Log(isDead);
        if(UITransform==null)
        {
          //  Debug.Log(1);
        }
        
        if (enemyStats.isDead&&UITransform)
        {
            Destroy(UITransform.gameObject);
            Debug.Log(111);
        }
        if(UITransform)
        {
            UITransform.forward = -cam.forward;
           //UpdateHealth(1);
        }
        
    }
    void UpdateHealth(int a)
    {
        if(UITransform!=null)
        {
            UITransform.gameObject.SetActive(true);
            timeLeft = visibleTime;
            UITransform.GetComponent<Slider>().maxValue = enemyStats.characterStats.maxHealth;
            UITransform.GetComponent<Slider>().value = enemyStats.characterStats.currentHealth;
        }
        
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
