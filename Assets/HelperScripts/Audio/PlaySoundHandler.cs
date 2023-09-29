using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class PlaySoundHandler : MonoBehaviour
{
    private const float fadeOutTime = .2f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AbstractAudioHolder sound;
    public bool playOnce = false;
    private bool played = false;
    private float baseVolume = 1.0f;
    private bool isActive = true;
    private bool startStopping = false;

    public bool IsPlaying => audioSource.isPlaying;

    void Awake()
    {
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if( sound.IsValid)
        {
            baseVolume = sound.GetVolume();
            audioSource.volume = sound.GetVolume();
            audioSource.pitch = sound.GetPitch();
        }
    }

    public void PlaySound()
    {
        if (playOnce && played)
            return;

        startStopping = false;
        StopAllCoroutines();
        audioSource.volume = baseVolume;

        audioSource.clip = sound.GetAudioClip(); //if audio holder has mutilple clip, GetAudioClip return random clip from list
        audioSource.Play();
        played = true;
    }
    
    public void StopSound()
    {
        played = false;
        if(isActive && !startStopping)
        {
            StartCoroutine(SoundTransition());
        }
    }

    IEnumerator SoundTransition()
    {
        startStopping = true;
        float timer = 0;
        while (timer / fadeOutTime < 1)
        {
            timer += TimeManager.UnscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(baseVolume, 0f, timer / .5f);
            yield return new WaitForEndOfFrame();
        }
        audioSource.Stop();

        audioSource.volume = baseVolume;
          
    }

    private void OnDestroy()
    {
        isActive = false;
        StopAllCoroutines();
    }
}
