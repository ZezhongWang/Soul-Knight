using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOrdinary : Room
{
    void Start()
    {
        OpenDoor();
    }

    public override void OnTriggerEnter2D(Collider2D coll)
    {
    }

}
