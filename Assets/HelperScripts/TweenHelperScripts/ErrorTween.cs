using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ErrorTween : MonoBehaviour
{
    [SerializeField] private float errorTime = 1;
    [SerializeField] private float shakeAmount = 1;
    private bool isPunched = false;
    public void ErrorShake()
    {
        if (!isPunched)
        {
            isPunched = true;
            DOTween.Punch(() => transform.rotation.eulerAngles, x => transform.rotation = Quaternion.Euler(x), Vector3.forward * shakeAmount, errorTime, vibrato: 4).SetId("ErrorTween").OnComplete(() => isPunched = false);
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill("ErrorTween");
    }
}
