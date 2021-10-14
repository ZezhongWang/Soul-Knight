using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState { Idle, Track, Stroll, Attack, Die}

public class Monster : Creature
{
    public MonsterState monsterState;
    public GameObject weaponObj;
    public Transform target;

    [HideInInspector]
    public Weapon weapon;

    public override void LookAt(Vector2 target)
    {
        base.LookAt(target);
        if(weapon != null)
        {
            weapon.LookAt(target);
        }
    }


    public virtual void Idle()
    {

    }
    
    public virtual void Track()
    {

    }

    public virtual void Stroll()
    {

    }

    public virtual void Attack()
    {

    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

}
