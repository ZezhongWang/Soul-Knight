using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Attributes")]
    public float CD;
    public string weaponName;
    public GameObject bulletPrefab;

    private float timeStamp;

    void start()
    {
        timeStamp = Time.time;
    }
     
    public void Shoot()
    {
        if(Time.time - timeStamp >= CD)
        {
            timeStamp = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().Instantiation();
        }
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

    public void PickUp()    // 将武器捡起来
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void PutDown()   // 将武器扔下
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
