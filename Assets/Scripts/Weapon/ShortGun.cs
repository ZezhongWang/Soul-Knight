using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器：散弹枪
/// 攻击力：3
/// 方式：散射
/// </summary>
public class ShortGun : Weapon
{
    public float bulletNum;      // 散射子弹数目
    public float angle;      // 散射角度

    public override void InstantiateBullet()
    {
        for (int i = 0; i < bulletNum; i++)
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.Set(eulerAngles.x, eulerAngles.y, eulerAngles.z - angle / 2 + i * angle / (bulletNum - 1));
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.right, Quaternion.Euler(eulerAngles));
            bullet.GetComponent<Bullet>().Instantiation(role);
        }
    }
}
