using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void Close()
    {
        foreach (Animator animator in GetComponentsInChildren<Animator>())
        {
            animator.Play("Close");
        }
        foreach (Collider2D coll in GetComponentsInChildren<Collider2D>())
        {
            coll.enabled = true;
        }
    }
    public void Open()
    {
        foreach (Animator animator in GetComponentsInChildren<Animator>())
        {
            animator.Play("Open");
        }
        foreach (Collider2D coll in GetComponentsInChildren<Collider2D>())
        {
            coll.enabled = false;
        }
    }
}
