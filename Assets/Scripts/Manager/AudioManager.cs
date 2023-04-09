using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager :SingleT<AudioManager>
{
    public AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

   public void PlayClip(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }
}
