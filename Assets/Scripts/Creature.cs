using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    protected float hp;

    public virtual void LookAt(Vector2 target)
    {
        GetComponent<SpriteRenderer>().flipX = target.x < transform.position.x;
    }
}
