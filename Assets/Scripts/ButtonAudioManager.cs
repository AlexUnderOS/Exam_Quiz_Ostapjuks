using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(int index)
    {
        if (audioSource == null)
            throw new ArgumentNullException(nameof(audioSource), "AudioSource is null");

        if (clips == null)
            throw new ArgumentNullException(nameof(clips), "AudioClip array is null");

        if (index < 0 || index >= clips.Length)
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");

        if (clips[index] == null)
            throw new NullReferenceException("AudioClip at the specified index is null");

        audioSource.clip = clips[index];
        audioSource.Play();
    }
}
