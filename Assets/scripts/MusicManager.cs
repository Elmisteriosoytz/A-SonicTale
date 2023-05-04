using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip introduction;
    public AudioClip loop;
    public float introductionDuration;
    float durationTime;
    bool hasLoop = true;
    // Start is called before the first frame update
    public void Play()
    {
        durationTime = introductionDuration;
        source = GetComponent<AudioSource>();
        source.clip = introduction;
        source.Play();
        hasLoop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLoop == false)
           durationTime -= Time.deltaTime;

        if (durationTime <= 0 && hasLoop == false)
        {
            source.Stop();
            source.clip = loop;
            source.Play();
            hasLoop = true;
        }
    }
}
