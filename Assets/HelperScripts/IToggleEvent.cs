using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IToggleEvent
{
    void ListenToggleOff(Action callback);
    void ListenToggleOn(Action callback);
    void SilenceToggleOff(Action callback);
    void SilenceToggleOn(Action callback);
}
