using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBoss : Room
{
    public int round;               // 波次
    public GameObject[] hatch;      // 孵化怪物的地方
    public GameObject bossHatch;
    public GameObject[] monsterSource;
    public List<Monster> monsters;
    public GameObject treasureHatch;
    public GameObject treasureBox;
    public GameObject bossSource;
    private Boss bossObj;
    private bool bossFlag;

    void Start()
    {
        OpenDoor();
        isExplored = false;
        bossFlag = false;
    }

    void Update()
    {
        if (!isExplored)
        {
            if (playerInRoom && !bossFlag)
            {
                hatchBoss();
                bossFlag = true;
            }
            if (playerInRoom && bossObj == null && bossFlag)
            {
                isExplored = true;
                if (treasureBox)
                    Instantiate(treasureBox, treasureHatch.transform.position, Quaternion.identity);
                OpenDoor();
            }
        }
    }

    public void hatchNewMonster()
    {
        for (int i = 0; i < hatch.Length; i++)
        {
            GameObject obj = Instantiate(monsterSource[Random.Range(0, monsterSource.Length)], hatch[i].transform.position, Quaternion.identity);
            Monster monster = obj.GetComponent<Monster>();
            monster.InstantiateSelf(this);
            monsters.Add(monster);
        }
    }

    public void hatchBoss()
    {
        GameObject obj = Instantiate(bossSource, bossHatch.transform.position, Quaternion.identity);
        bossObj = obj.GetComponent<Boss>();
        bossObj.InstantiateSelf(this);
    }

    public override void MonsterDie(Monster monster)
    {
        monsters.Remove(monster);
    }


    public override Transform GetNearestMonster()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        foreach(Monster monster in monsters)
        {
            float dist = Vector3.Distance(KnightController.Instance.transform.position, monster.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                tMin = monster.transform;
            }
        }
        if (bossObj != null)
        {
            float bossDist = Vector3.Distance(KnightController.Instance.transform.position, bossObj.transform.position);
            if (bossDist < minDist)
            {
                tMin = bossObj.transform;
            }
        }
        
        return tMin;
    }
}
