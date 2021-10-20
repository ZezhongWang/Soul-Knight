﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// 名称：手枪哥布林
/// 武器：手枪(Gun)
/// 血量：10
/// 攻击力：3
/// </summary>
public class Goblin_Gun : Monster
{
    private Animator anim;
    private IntellFindPath intell;

    void Start()
    {
        hp = 10f;
        energy = 100f;
        coinNum = 2;
        magicStoneNum = 3;
        playerInRoom = true;
        strollTimeStamp = -5f;
        monsterState = MonsterState.Idle;
        anim = GetComponent<Animator>();
        intell = GetComponent<IntellFindPath>();
        weapon = weaponObj ? weaponObj.GetComponent<Weapon>() : null;
        weapon.InstantiateWeapon(transform.tag);
    }

    void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Idle:
                Idle();
                break;
            case MonsterState.Track:
                Track();
                break;
            case MonsterState.Stroll:
                Stroll();
                break;
            case MonsterState.Attack:
                Attack();
                break;
            case MonsterState.Elude:
                Elude();
                break;
            case MonsterState.Die:
                Die();
                break;
        }
    }

    public override void Idle()
    {
        if(playerInRoom)
        {
            monsterState = MonsterState.Stroll;
            anim.SetBool("isRun", true);
        }
    }

    public override void Stroll()
    {
        if (RaycastDetection())
        {
            monsterState = MonsterState.Track;
        }
        if (Time.time - strollTimeStamp >= strollCD)
        {
            strollTimeStamp = Time.time;
            var target = transform.position + Random.insideUnitSphere * strollRadius;
            intell.UpdatePath(target);
        }
        LookAt(intell.nextPosition);
        intell.moveTo();

        if (intell.reachPathEnd)
            anim.SetBool("isRun", false);
        else
            anim.SetBool("isRun", true);
    }

    public override void Track()
    {
        if (!RaycastDetection())
        {
            monsterState = MonsterState.Stroll;
        }
        if (Vector3.Distance(transform.position, target.position) <= attackRadius)
        {
            monsterState = MonsterState.Attack;
            anim.SetBool("isRun", false);
        }
        if (Time.time - trackTimeStamp >= trackCD)
        {
            trackTimeStamp = Time.time;
            intell.UpdatePath(target.position);
        }
        LookAt(intell.nextPosition);
        intell.moveTo();
    }

    public override void Attack()
    {
        float dis = Vector3.Distance(transform.position, target.position);
        if (dis > attackRadius)
        {
            monsterState = MonsterState.Track;
            anim.SetBool("isRun", true);
        }
        if (Time.time - attackTimeStamp >= attackCD)
        {
            attackTimeStamp = Time.time;
            if (weapon != null)
                weapon.Shoot(ref energy);
        }
        LookAt(target.position);
    }
}
