using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectPlayer : MonoBehaviour
{
    private AudioSource source;
    public UnityEvent OnSoundEffectDone = null;

    private float timer = 0;
    private bool hasPlayed = false;

    public bool IsPlaying => source.isPlaying;

    private void Awake() {
        source = GetComponent<AudioSource>();
        source.ignoreListenerPause = true;
        source.ignoreListenerVolume = true;
    }

    public void PlayEffect(AbstractAudioHolder clip){
        if (source.isActiveAndEnabled)
        {
            if (Time.timeScale == 0)
            {
                float timeScale = Time.timeScale;
                Time.timeScale = 1;
                if (clip.IsValid)
                {
                    AudioClip audioClip = clip.GetAudioClip();
                    source.PlayOneShot(audioClip, clip.GetVolume());
                    hasPlayed = true;
                    timer = audioClip.length;
                }
                Time.timeScale = timeScale;
            }
            else
            {
                if (clip.IsValid)
                {
                    AudioClip audioClip = clip.GetAudioClip();
                    source.PlayOneShot(clip.GetAudioClip(), clip.GetVolume());
                    hasPlayed = true;
                    timer = audioClip.length;
                }
            }
        }
    }

    public void StopSound()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
    }

    private void Update()
    {
        if (hasPlayed)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                hasPlayed = false;
                OnSoundEffectDone?.Invoke();
            }
        }
    }
}
