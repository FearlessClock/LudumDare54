using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnAwake : MonoBehaviour
{
    [SerializeField] private SingleAudioHolder sound;
    private AudioSource audioSource;
    [SerializeField] private bool onEnable = false;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        audioSource.clip = sound.GetAudioClip();
        audioSource.volume = sound.volume;
        if (sound.randomPitch)
            audioSource.pitch = Random.Range(sound.minPitch, sound.maxPitch);
        audioSource.Play();
    }

    private void OnEnable()
    {
        if(onEnable)
            audioSource.Play();
    }
}
