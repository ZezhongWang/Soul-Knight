using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Open");
    }

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        Invoke("ReturnToMainMenu", 2f);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
