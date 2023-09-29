using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AddScene), typeof(UnloadSceneCallback))]
[Obsolete("Not used anymore, should delete")]
public class FadeHandler : MonoBehaviour
{
    private AddScene addScene = null;
    private UnloadSceneCallback unloadScene = null;
    [SerializeField] private StringVariable sceneToLoad = null;
    [SerializeField] private StringVariable fadeSceneToUnload = null;
    private string[] activeScenes = new string[0];

    public UnityEvent OnDoneLoadingScenes; 
    private void Awake()
    {
        addScene = GetComponent<AddScene>();
        unloadScene = GetComponent<UnloadSceneCallback>();
        if (SceneManager.GetSceneByName(sceneToLoad.value).buildIndex > 0)
        {
            try
            {
                SceneManager.UnloadSceneAsync(sceneToLoad.value);
            }
            catch(System.Exception ex)
            {
                Debug.LogWarning("Could not unload " + sceneToLoad.value);
                Debug.LogWarning(ex.Message);
            }
        }
        List<string> activeScenesList = new List<string>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (!SceneManager.GetSceneAt(i).name.Equals(fadeSceneToUnload))
            {
                activeScenesList.Add(SceneManager.GetSceneAt(i).name);
            }
        }
        activeScenes = activeScenesList.ToArray();

    }

    public void HandleEvent(string eventName)
    {
        
        if (eventName.ToUpper().Equals("FADEHALFWAY"))
        {
            StartCoroutine(UnloadWithWait());
        }else if (eventName.ToUpper().Equals("FADEDONE"))
        {
            unloadScene.UnloadScene(fadeSceneToUnload);
        }
    }

    public IEnumerator UnloadWithWait()
    {
        foreach (string sceneName in activeScenes)
        {
            if (sceneName != null)
            {
                unloadScene.UnloadScene(sceneName);
            }
        }
        yield return new WaitForSeconds(0.1f);
        addScene.AddSceneCallback(sceneToLoad);

        OnDoneLoadingScenes?.Invoke();
    }
}
