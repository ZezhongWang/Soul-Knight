using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GoblinWitcher : Monster
{
    public bool playerInRoom;
    public float strollRadius;
    public float strollCD;
    public float trackRadius;
    public float trackCD;
    public float attackRadius;
    public float attackCD;
    public LayerMask layerMask;
    
    private Animator anim;
    private IntellFindPath intell;
    private RaycastHit2D hit;
    private float strollTimeStamp;
    private float trackTimeStamp;
    private float attackTimeStamp;

    void Start()
    {
        hp = 5f;
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
        switch(monsterState)
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
        if(RaycastDetection())
        {
            monsterState = MonsterState.Track;
        }
        if(Time.time - strollTimeStamp >= strollCD)
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
        if(!RaycastDetection())
        {
            monsterState = MonsterState.Stroll;
        }
        if(Vector3.Distance(transform.position, target.position) <= attackRadius)
        {
            monsterState = MonsterState.Attack;
            anim.SetBool("isRun", false);
        }
        if(Time.time - trackTimeStamp >= trackCD)
        {
            trackTimeStamp = Time.time;
            intell.UpdatePath(target.position);
        }
        LookAt(intell.nextPosition);
        intell.moveTo();
    }

    public override void Attack()
    {
        if(Vector3.Distance(transform.position, target.position) > attackRadius)
        {
            monsterState = MonsterState.Track;
            anim.SetBool("isRun", true);
        }
        if(Time.time - attackTimeStamp >= attackCD)
        {
            attackTimeStamp = Time.time;
            if (weapon != null)
                weapon.Shoot();
        }
        LookAt(target.position);
    }

    public bool RaycastDetection()
    {
        hit = Physics2D.Raycast(transform.position + Vector3.up, (target.position - (transform.position + Vector3.up)).normalized, trackRadius, layerMask);

        if (hit.transform != null && hit.transform == target)
        {
            Debug.DrawLine(transform.position + Vector3.up, hit.transform.position, Color.red);
            return true;
        }
        else
        {
            return false;
        }
    }
}
