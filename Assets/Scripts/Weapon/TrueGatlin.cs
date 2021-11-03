using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器：手枪
/// 攻击力：3
/// 方式：发射2枚速度较慢的子弹
/// </summary>
public class TrueGatlin : Weapon
{
    public int bulletNum;       // 单次发射子弹数目
    public float dartleCD;      // 连射CD

    public override void Shoot(ref float energy)
    {
        if (Time.time - timeStamp >= shootCD && energy >= useEnergy)
        {
            timeStamp = Time.time;
            energy -= useEnergy;
            StartCoroutine(ContinuousShoot());
        }
    }

    private IEnumerator ContinuousShoot()
    {
        for (int i = 0; i < bulletNum; i++)
        {
            InstantiateBullet();
            FMODUnity.RuntimeManager.PlayOneShot("event:/Weapons/" + sfxPath, GetComponent<Transform>().position);
            yield return new WaitForSeconds(dartleCD);
        }
    }
}
