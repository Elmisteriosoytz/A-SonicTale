using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAudioSourcePlay : MonoBehaviour
{
    AudioSource source;
    public AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void AudioSourcePlay()
    {
        source.PlayOneShot(sound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
