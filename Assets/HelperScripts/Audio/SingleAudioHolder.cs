using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Audio/Single Audio Holder")]
public class SingleAudioHolder : AbstractAudioHolder
{
    public AudioClip clip = null;
    public float volume = 1;
    public bool randomPitch;
    [ShowIf(nameof(randomPitch)), Range(0, 2)]
    public float minPitch = 1;
    [ShowIf(nameof(randomPitch)), Range(0, 2)]
    public float maxPitch = 1;

    public override AudioClip GetAudioClip()
    {
        return clip;
    }

    public override float GetVolume()
    {
        return volume;
    }

    public override float GetPitch()
    {
        if (randomPitch)
        {
            return Random.Range(minPitch, maxPitch);
        }
        else
            return 1f;
    }

    public override bool IsValid
    {
        get { return clip != null;  }
    }
}
