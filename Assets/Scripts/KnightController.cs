using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    public float speed;
    private Vector2 movement;
    private Animator anim;
    private Rigidbody2D rigid;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis(" Vertical");
        if (movement.x == 0 && movement.y == 0)
            anim.SetBool("isRun", false);
        else
            anim.SetBool("isRun", true);
    }

    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + movement * speed * Time.fixedDeltaTime);
    }
}
