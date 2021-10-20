using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodenBox : MonoBehaviour, BeAttack
{
    public float hp;

    void Start()
    {
        hp = 2f;
    }

    void Update()
    {
        
    }

    public void BeAttack(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
