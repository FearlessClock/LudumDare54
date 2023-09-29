using HelperScripts.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{

    public void LoadSceneCallback(StringVariable name)
    {
        SceneChangeManager.instance.LoadScene(name.value);
    }

    public void LoadSceneWithFadeCallback(StringVariable name)
    {
        SceneChangeManager.instance.LoadSceneWithFade(name);
    }
}
