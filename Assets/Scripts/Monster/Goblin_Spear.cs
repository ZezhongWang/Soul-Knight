using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Spear : Monster
{
    private Animator anim;
    private IntellFindPath intell;

    void Start()
    {
        hp = 10f;
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
        if (playerInRoom)
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
                weapon.Shoot();
        }
        LookAt(target.position);
    }
}
