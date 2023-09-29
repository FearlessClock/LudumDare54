using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAlphaTween : MonoBehaviour
{
    [SerializeField] private CanvasGroup group = null;
    [SerializeField] private float startPosition = 0;
    [SerializeField] private float time = 1;
    [SerializeField] private Ease ease = Ease.Flash;
    private void OnEnable()
    {
        group.DOFade(startPosition, time).From().SetId("FadeInTween").SetUpdate(true).SetEase(ease);
    }

    private void OnDestroy()
    {
        group.DOKill();
    }
}
