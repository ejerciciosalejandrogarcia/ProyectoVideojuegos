using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al AudioSource del AudioManager

    public void Play()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void Stop()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

}
