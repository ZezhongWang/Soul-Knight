using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMonster : Room
{
    public int round;               // 波次
    public GameObject[] hatch;      // 孵化怪物的地方
    public GameObject[] monsterSource;
    public List<Monster> monsters;
    public GameObject treasureHatch;
    public GameObject treasureBox;

    void Start()
    {
        OpenDoor();
        isExplored = false;
        round = 3;
    }

    void Update()
    {
        if(!isExplored)
        {
            if (playerInRoom)
            {
                if (monsters.Count == 0 && round > 0)
                {
                    hatchNewMonster();
                    round--;
                }
            }
            if (round == 0 && monsters.Count == 0)
            {
                isExplored = true;
                if(treasureBox)
                    Instantiate(treasureBox, treasureHatch.transform.position, Quaternion.identity);
                OpenDoor();
            }
        }
    }

    public void hatchNewMonster()
    {
        for(int i = 0; i < hatch.Length; i++)
        {
            GameObject obj = Instantiate(monsterSource[Random.Range(0, monsterSource.Length)], hatch[i].transform.position, Quaternion.identity);
            Monster monster = obj.GetComponent<Monster>();
            monster.InstantiateSelf(this);
            monsters.Add(monster);
        }
    }

    public override void MonsterDie(Monster monster)
    {
        monsters.Remove(monster);
    }


}
