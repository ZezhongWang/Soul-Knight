using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconBeats : MonoBehaviour, IEasyListener
{
    private float timer = 0;
    public float scaleSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeat(EasyEvent currentAudioEvent)
    {
        StartCoroutine(AnimateIcon());
    }

    private IEnumerator AnimateIcon()
    {
        timer = 0;
        yield return null;

        timer = Time.deltaTime;
        this.transform.localScale = Vector3.one;

        timer += Time.deltaTime; // A timer for the cube animation

        var cubeTrans = this.transform;
        Vector3 newScale = this.transform.localScale;
        float cubeSize = this.transform.localScale.x;

        while (timer > 0)
        {
            cubeSize += scaleSpeed / 100;
            newScale = new Vector3(cubeSize, cubeSize, cubeSize);
            cubeTrans.localScale = newScale;
            yield return null;
        }
    }
}
