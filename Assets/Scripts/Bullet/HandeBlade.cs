using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandeBlade : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != role)
        {
            if (collision.GetComponent<BeAttack>() != null)
            {
                collision.GetComponent<BeAttack>().BeAttack(damage);
            }
        }
    }

    public override void Instantiation(string role)
    {
        this.role = role;
        Destroy(gameObject, 0.2f);
    }
}
