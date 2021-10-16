using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Attributes")]
    public float shootCD;
    public float energy;
    public string weaponName;
    public string role;                 // 谁持有的武器
    public GameObject bulletPrefab;     // 子弹物体

    protected float timeStamp;

    public void InstantiateWeapon(string role)
    {
        this.role = role;
    }

    public virtual void Shoot()
    {
        if(Time.time - timeStamp >= shootCD)
        {
            timeStamp = Time.time;
            InstantiateBullet();
        }
    }

    public virtual void InstantiateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.right, transform.rotation);
        bullet.GetComponent<Bullet>().Instantiation(role);
    }

    public void LookAt(Vector3 target)
    {
        transform.right = (target - transform.position).normalized;
        GetComponent<SpriteRenderer>().flipY = target.x < transform.position.x;
    }

    public void PutAway()   // 将武器收起来
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void TakeOut()   // 将武器拿出来
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void PickUp(string role)    // 将武器捡起来
    {
        GetComponent<Collider2D>().enabled = false;
        this.role = role;
    }

    public void PutDown()   // 将武器扔下
    {
        GetComponent<Collider2D>().enabled = true;
        role = "";
    }
}
