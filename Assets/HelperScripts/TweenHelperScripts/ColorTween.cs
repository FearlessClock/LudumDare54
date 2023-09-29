using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ColorTween : MonoBehaviour
{
    [SerializeField] private Color color1 = Color.white;
    [SerializeField] private Color color2 = Color.gray;
    [SerializeField] private float time = 1;
    [SerializeField] private Image image = null;
    public void SetColor1()
    {
        image.DOColor(color1, time);
    }

    public void SetColor2()
    {
        image.DOColor(color2, time);
    }

    private void OnDestroy()
    {
        image.DOKill();
    }
}
