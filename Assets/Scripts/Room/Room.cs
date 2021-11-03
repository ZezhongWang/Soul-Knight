using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Door[] doors;
    public bool isExplored;
    public bool playerInRoom;

    public void OpenDoor()
    {
        foreach(Door door in doors)
            door.Open();
    }

    public void CloseDoor()
    {
        foreach (Door door in doors)
            door.Close();
    }

    public virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.transform.tag == "Player")
        {
            playerInRoom = true;
            if(!isExplored)
            {
                Invoke("CloseDoor", 0.5f);
                KnightController.Instance.room = this;
            }
        }
    }

    public virtual void MonsterDie(Monster monster) { }


    public virtual Transform GetNearestMonster()
    {
        return null;
    }
}
