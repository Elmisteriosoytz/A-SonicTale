using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAudioSourcePlay : MonoBehaviour
{
    AudioSource source;
    public AudioClip sound;
    public AudioClip sound2;
    public AudioClip sound3;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void AudioSourcePlay()
    {
        source.PlayOneShot(sound);
    }

    public void SecondAudioSourcePlay()
    {
        source.PlayOneShot(sound2);
    }

    public void ThirdAudioSourcePlay()
    {
        source.PlayOneShot(sound3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
