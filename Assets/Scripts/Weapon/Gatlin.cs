using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器：手枪
/// 攻击力：3
/// 方式：发射2枚速度较慢的子弹
/// </summary>
public class Gatlin : Weapon
{
    public float dartleCD;      // 连射CD

    void Start()
    {

    }

    public override void Shoot()
    {
        if (Time.time - timeStamp >= shootCD)
        {
            timeStamp = Time.time;
            InstantiateBullet();
            Invoke("InstantiateBullet", dartleCD);
        }
    }
}
