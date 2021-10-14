using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float force;
    
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    public void Instantiation()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * force, ForceMode2D.Impulse);
    }
}
