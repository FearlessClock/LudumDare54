using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayGenderedSoundHandler : MonoBehaviour
{
    private const float fadeOutTime = .2f;
    private AudioSource audioSource;
    [SerializeField] private AbstractAudioHolder maleSound;
    [SerializeField] private AbstractAudioHolder femaleSound;
    private int gender = 0;
    private float baseVolume = 1.0f;

    public bool IsPlaying => audioSource.isPlaying;
    private AbstractAudioHolder Sound => gender == 0 ? maleSound : femaleSound;


    public void Init(int gender)
    {
        this.gender = gender;
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (Sound.IsValid)
        {
            baseVolume = Sound.GetVolume();
            audioSource.volume = Sound.GetVolume();
            audioSource.pitch = Sound.GetPitch();
        }
    }

    public void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.volume = baseVolume;

            audioSource.clip = Sound.GetAudioClip(); //if audio holder has mutilple clip, GetAudioClip return random clip from list
            audioSource.Play();
        }
    }

    public void FadeOut()
    {
        audioSource.DOFade(0, 0.4f);
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
