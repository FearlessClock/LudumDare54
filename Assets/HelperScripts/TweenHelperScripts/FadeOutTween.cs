using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class FadeOutTween : MonoBehaviour
{
    [SerializeField] private float endPosition = 0;
    [SerializeField] private float time = 1;
    [SerializeField] private Ease ease = Ease.Flash;
    public UnityEvent OnComplete;
    public void FadeOut()
    {
        ((RectTransform)transform).DOAnchorPosX(endPosition, time).SetId("FadeOutTween").SetEase(ease).OnComplete(()=>OnComplete?.Invoke()).SetUpdate(true);
    }

    private void OnDestroy()
    {
        DOTween.Kill("FadeOutTween");
    }
}
