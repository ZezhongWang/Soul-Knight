using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject obj;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        obj.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.tag == "Player")
        {
            anim.Play("Open");
            obj.SetActive(true);
        }
    }
}
