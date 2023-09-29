using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPrefabPlayer : MonoBehaviour
{
    [SerializeField] private SoundEffectPlayer sourcePrefab = null;

    public void PlaySound(AbstractAudioHolder holder)
    {
        if(sourcePrefab != null)
        {
            SoundEffectPlayer source = Instantiate<SoundEffectPlayer>(sourcePrefab);
            source.PlayEffect(holder);
        }
    }
}
