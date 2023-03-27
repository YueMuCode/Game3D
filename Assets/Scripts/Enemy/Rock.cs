using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Rigidbody rb;
    public float force;//撞击的力
    public GameObject hitTarget;//撞击的目标
    private Vector3 direction;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        HitToTarget();
    }

    public void HitToTarget()
    {
        direction = (hitTarget.transform.position - transform.position+Vector3.up).normalized;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().GetHit(FindObjectOfType<Golem>().GetComponent<Golem>().characterStats.maxDamage, FindObjectOfType<Golem>().transform);
            collision.transform.GetComponent<PlayerController>().velocity = direction * force;
            collision.transform.GetComponent<PlayerController>().anim.SetTrigger("Dizzy");
            Debug.Log("玩家受到了" + FindObjectOfType<Golem>().GetComponent<Golem>().characterStats.maxDamage + "点伤害");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().velocity = Vector3.zero;
        }
        
    }
}
