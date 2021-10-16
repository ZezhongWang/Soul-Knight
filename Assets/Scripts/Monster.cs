using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState { Idle, Track, Stroll, Attack, Die}

/// <summary>
/// 怪物基类
/// </summary>
public class Monster : Creature, BeAttack
{
    public MonsterState monsterState;           
    public GameObject weaponObj;                // Monster拥有的武器物体
    public Transform target;                    // 攻击目标

    protected Weapon weapon;

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

    public virtual void Die() { }
}
