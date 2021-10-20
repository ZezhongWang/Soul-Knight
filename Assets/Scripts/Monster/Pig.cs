using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Monster
{
    private Animator anim;
    private IntellFindPath intell;
    private float damage;

    void Start()
    {
        hp = 5f;
        damage = 2f;
        energy = 100f;
        coinNum = 2;
        magicStoneNum = 3;
        playerInRoom = true;
        strollTimeStamp = -5f;
        monsterState = MonsterState.Idle;
        anim = GetComponent<Animator>();
        intell = GetComponent<IntellFindPath>();
    }

    void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Idle:
                Idle();
                break;
            case MonsterState.Stroll:
                Stroll();
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
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<BeAttack>() != null)
        {
            collision.transform.GetComponent<BeAttack>().BeAttack(damage);
        }
    }
}
