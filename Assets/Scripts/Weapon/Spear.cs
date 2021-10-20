using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{
    public override void Shoot(ref float energy)
    {
        if (Time.time - timeStamp >= shootCD && energy >= useEnergy)
        {
            timeStamp = Time.time;
            energy -= useEnergy;
            InstantiateBullet();
            GetComponent<Animator>().SetTrigger("isAttack");
        }
    }
}
