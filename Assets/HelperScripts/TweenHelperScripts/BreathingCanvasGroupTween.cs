using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BreathingCanvasGroupTween : MonoBehaviour
{
    private Sequence breathingSequence = null;

    private CanvasGroup group = null;
    [SerializeField] private float breathingMin = 0.4f;
    [SerializeField] private float breathingDuration = 1;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
        breathingSequence = DOTween.Sequence();
        breathingSequence.Append(group.DOFade(breathingMin, breathingDuration/2));
        breathingSequence.Append(group.DOFade(1, breathingDuration/2)).SetLoops(-1).SetEase(Ease.Linear);
        breathingSequence.Play();
    }

    private void OnDestroy()
    {
        breathingSequence.Kill();
    }
}
