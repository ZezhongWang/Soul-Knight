using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState { Idle, Track, Stroll, Attack, Elude, Die}

/// <summary>
/// 怪物基类
/// </summary>
public class Monster : Creature, BeAttack
{
    public MonsterState monsterState;
    public bool playerInRoom;
    public float strollRadius;
    public float strollCD;
    public float trackRadius;
    public float trackCD;
    public float attackRadius;
    public float attackCD;
    public GameObject weaponObj;                // Monster拥有的武器物体
    public Transform target;                    // 攻击目标
    public LayerMask layerMask;                 // 目标的LayerMask

    protected Weapon weapon;
    protected RaycastHit2D hit;
    protected float strollTimeStamp;
    protected float trackTimeStamp;
    protected float attackTimeStamp;

    public void BeAttack(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            monsterState = MonsterState.Die;
            GetComponent<Animator>().SetBool("isDead", true);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public override void LookAt(Vector2 target)
    {
        base.LookAt(target);
        if(weapon != null)
        {
            weapon.LookAt(target);
        }
    }


    public virtual void Idle() { }

    public virtual void Track() { }

    public virtual void Stroll() { }

    public virtual void Attack() { }

    // 躲避
    public virtual void Elude() { }

    public virtual void Die() { }

    public bool RaycastDetection()
    {
        hit = Physics2D.Raycast(transform.position, (target.position - transform.position).normalized, trackRadius, layerMask);

        if (hit.transform != null && hit.transform == target)
        {
            Debug.DrawLine(transform.position, hit.transform.position, Color.red);
            return true;
        }
        else
        {
            return false;
        }
    }
}
