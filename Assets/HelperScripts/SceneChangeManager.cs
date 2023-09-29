using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    private struct SceneLoadingInfo
    {
        public AsyncOperation op;
        public string sceneName;
    }
    public static SceneChangeManager instance = null;

    [SerializeField] private StringVariable[] scenesToNotUnload = null;
    public Action OnSceneLoaded = null;
    [SerializeField] private StringVariable fadeScene = null;
    [SerializeField] private BoolVariable doneLoadingFade = null;

    private string currentPrimaryScene = "";

    private List<SceneLoadingInfo> scenesLoading = new List<SceneLoadingInfo>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        currentPrimaryScene = SceneManager.GetActiveScene().name;
    }

    public void LoadSceneWithFade(StringVariable sceneName)
    {
        LoadSceneWithFade(sceneName.value);
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(LoadWithFade(sceneName));
    }

    private IEnumerator LoadWithFade(string sceneName)
    {
        AddScene(fadeScene, false);
        yield return new WaitUntil(() => doneLoadingFade.value == true);
        doneLoadingFade.SetValue(false);
        UnloadSurroundingScenes();
        AddScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        UnloadSurroundingScenes();
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scenesLoading.Add(new SceneLoadingInfo() { op = op, sceneName = sceneName });
    }

    private void UnloadSurroundingScenes()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            bool skip = false;
            for (int j = 0; j < scenesToNotUnload.Length; j++)
            {
                if (SceneManager.GetSceneAt(i).name == scenesToNotUnload[j].value)
                {
                    skip = true;
                    break;
                }
            }
            if (skip)
            {
                continue;
            }
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
        }
    }

    private void AddSceneLoaded(SceneLoadingInfo obj)
    {
        OnSceneLoaded?.Invoke();
        Scene scene = SceneManager.GetSceneByName(obj.sceneName);
        if(scene.IsValid())
        {
            bool res = SceneManager.SetActiveScene(scene);
        }
    }

    public void ReloadCurrentScene()
    {
        LoadSceneWithFade(SceneManager.GetActiveScene().name);
    }

    public void AddScene(string sceneName, bool isPrimaryScene = true)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        string sceneToKeepAsPrimary = "";
        if (isPrimaryScene)
        {
            currentPrimaryScene = sceneName;
            sceneToKeepAsPrimary = sceneName;
        }
        else
        {
            sceneToKeepAsPrimary = currentPrimaryScene;
        }
        scenesLoading.Add(new SceneLoadingInfo() { op = op, sceneName= sceneToKeepAsPrimary });
    }

    public void AddScene(StringVariable sceneName, bool isPrimaryScene = true)
    {
        AddScene(sceneName.value, isPrimaryScene);
    }

    public void RemoveScene(StringVariable pauseMenuScene)
    {
        SceneManager.UnloadSceneAsync(pauseMenuScene.value);
    }

    private void Update()
    {
        for (int i = scenesLoading.Count-1; i >= 0; i--)
        {   
            if (scenesLoading[i].op.isDone)
            {
                AddSceneLoaded(scenesLoading[i]);
                scenesLoading.RemoveAt(i);
            }
        }
    }
}
