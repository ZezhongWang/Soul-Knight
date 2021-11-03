using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Weapon
{
    private float angleBtwBullets;
    private float angleBtwGroups;
    private Transform player;
    private bool attacking;

    void Start()
    {
        angleBtwBullets = 5;
        angleBtwGroups = 30;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Shoot(ref float energy)
    {
        if (Time.time - timeStamp >= shootCD && energy >= useEnergy && !attacking)
        {
            if (Random.Range(0, 100) > 80)
            {
                StartCoroutine(StrongAttack());
            }
            else
            {
                timeStamp = Time.time;
                energy -= useEnergy;
                InstantiateBullet();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Weapons/" + sfxPath, GetComponent<Transform>().position);
            }
        }
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

    public IEnumerator StrongAttack()
    {
        //if (attacking == true) yield return null;
        //attacking = true;
        int rand = Random.Range(0, 100);
        if (rand < 50)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Weapons/" + sfxPath, GetComponent<Transform>().position);
            for (float angle = 0; angle < 360f; angle += 5f)
            {
                //Debug.Log("Generate one Barrage");
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.transform.right = (player.position - bullet.transform.position).normalized;
                bullet.transform.Rotate(Vector3.forward, angle, Space.Self);
                bullet.GetComponent<Bullet>().Instantiation(role);
            }
        }
        else
        {
            float angleOffset = 0;
            for (int i = 0; i < 20; i++)
            {
                for (float angle = 0; angle < 360f; angle += 60f)
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                    bullet.transform.right = (player.position - bullet.transform.position).normalized;
                    bullet.transform.Rotate(Vector3.forward, angle + angleOffset, Space.Self);
                    bullet.GetComponent<Bullet>().Instantiation(role);
                }
                yield return new WaitForSeconds(0.1f);
                angleOffset += 2f;
            }
        }
        //attacking = false;
    }

    public override void LookAt(Vector3 target)
    {
        GetComponent<SpriteRenderer>().flipY = target.x < transform.position.x;
    }
}
