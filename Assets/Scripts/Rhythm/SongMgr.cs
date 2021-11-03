using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongMgr : MonoBehaviour
{
    public string bgm;

    private bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !isPlaying)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/BG/" + bgm);
            isPlaying = true;
        }
    }
}
