using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������ǹ
/// ��������3
/// ��ʽ������2ö�ٶȽ������ӵ�
/// </summary>
public class TrueGatlin : Weapon
{
    public int bulletNum;       // ���η����ӵ���Ŀ
    public float dartleCD;      // ����CD

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
