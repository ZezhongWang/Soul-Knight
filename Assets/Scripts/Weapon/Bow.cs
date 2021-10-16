using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public override void Shoot()
    {
        if (Time.time - timeStamp >= shootCD)
        {
            timeStamp = Time.time;
            InstantiateBullet();
            GetComponent<Animator>().SetTrigger("isAttack");
        }
    }
}
