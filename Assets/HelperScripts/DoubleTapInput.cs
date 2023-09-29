using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoubleTapInput : MonoBehaviour
{
    [SerializeField] private float doubleTapWaitTime = 0.2f;
    public UnityEvent OnDoubleTap;
    private float timer = 0;
    private bool firstTouch = false;

    private bool isActive = true;
    public void ToggleDoubleTap(bool state)
    {
        isActive = state;
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        if (firstTouch)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                firstTouch = false;
            }
        }
        if (InputManager.InputExistsDown())
        {
            if (firstTouch && timer > 0)
            {
                firstTouch = false;
                timer = -1;
                OnDoubleTap?.Invoke();
                return;
            }
            firstTouch = true;
            timer = doubleTapWaitTime;
        }
    }
}
