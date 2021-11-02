using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹基类
/// </summary>
public class Bullet : MonoBehaviour
{
    public float force;             //  子弹射出的力
    protected string role;          //  谁射出的子弹
    public float damage;            //  对敌伤害
    public float destroyTime = 0.05f;
    public GameObject hitEffect;    //  命中后的特效

   protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag != role)
        {
            if(collision.GetComponent<BeAttack>() != null)
            {
                collision.GetComponent<BeAttack>().BeAttack(damage);
                DestroyBullet();
            }
            if (collision.transform.tag == "Wall")
                DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
        if(hitEffect != null)
        {
            GameObject hit = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hit, destroyTime);
        }
    }

    public virtual void Instantiation(string role)
    {
        this.role = role;
        GetComponent<Rigidbody2D>().AddForce(transform.right * force, ForceMode2D.Impulse);
    }
}
