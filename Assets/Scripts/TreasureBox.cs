using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject weaponObj;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        weaponObj.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.tag == "Player")
        {
            anim.Play("Open");
            weaponObj.SetActive(true);
        }
    }
}
