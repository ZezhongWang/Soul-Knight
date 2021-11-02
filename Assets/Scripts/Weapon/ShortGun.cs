using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ɢ��ǹ
/// ��������3
/// ��ʽ��ɢ��
/// </summary>
public class ShortGun : Weapon
{
    public float bulletNum;      // ɢ���ӵ���Ŀ
    public float angle;      // ɢ��Ƕ�

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
