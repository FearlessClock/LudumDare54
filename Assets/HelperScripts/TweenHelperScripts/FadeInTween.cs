using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FadeInTween : MonoBehaviour
{
    [SerializeField] private float startPosition = 0;
    [SerializeField] private float time = 1;
    [SerializeField] private Ease ease = Ease.Flash;
    private void OnEnable()
    {
        ((RectTransform)transform).DOAnchorPosX(startPosition, 0).SetId("FadeInTween").SetUpdate(true);
        ((RectTransform)transform).DOAnchorPosX(0, time).SetEase(ease).SetId("FadeInTween").SetUpdate(true);
    }

    private void OnDestroy()
    {
        DOTween.Kill("FadeInTween");
    }
}
