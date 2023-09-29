using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SuccessTween : MonoBehaviour
{
    [SerializeField] private float punchForce = 1;
    [SerializeField] private float punchTime = 1;
    public void ShowSuccess()
    {
        transform.DOKill(true);
        transform.DOPunchScale(punchForce * Vector3.one, punchTime).SetUpdate(UpdateType.Normal, true);
    }

    private void OnDestroy()
    {
        transform.DOKill(true);
    }
}
