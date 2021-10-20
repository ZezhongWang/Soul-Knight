using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Weapon
{
    private float angleBtwBullets;
    private float angleBtwGroups;
    private Transform player;

    void Start()
    {
        angleBtwBullets = 5;
        angleBtwGroups = 30;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void InstantiateBullet()
    {
        for (float i = -angleBtwGroups; i <= angleBtwGroups; i += angleBtwGroups)
        {
            for (float j = -angleBtwBullets; j <= angleBtwBullets; j += angleBtwBullets)
            {
                float angle = i + j;
                GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.right, transform.rotation);
                bullet.transform.right = (player.position - bullet.transform.position).normalized;
                bullet.transform.Rotate(Vector3.forward, angle, Space.Self);
                bullet.GetComponent<Bullet>().Instantiation(role);
            }            
        }

    }
    public override void LookAt(Vector3 target)
    {
        GetComponent<SpriteRenderer>().flipY = target.x < transform.position.x;
    }
}
