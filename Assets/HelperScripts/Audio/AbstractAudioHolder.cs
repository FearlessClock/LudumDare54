using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAudioHolder : ScriptableObject
{
    public abstract bool IsValid { get; }

    public abstract AudioClip GetAudioClip();
    public abstract float GetVolume();

    public abstract float GetPitch();
}
