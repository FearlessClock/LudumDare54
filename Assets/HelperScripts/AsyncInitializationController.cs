using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AsyncInitializationController : MonoBehaviour
{
    [Button("Get All Loaders", DirtyOnClick = true), PropertyOrder(-10)]
    private void LoadLoaders()
    {
        AsyncLoader[] allLoaders = FindObjectsOfType<AsyncLoader>();
        for (int i = 0; i < allLoaders.Length; i++)
        {
            bool isAlreadyLoaded = false;
            for (int j = 0; j < loaders.Count; j++)
            {
                if (loaders[j].Equals(allLoaders[i]))
                {
                    isAlreadyLoaded = true;
                    break;
                }
            }
            if (!isAlreadyLoaded)
            {
                loaders.Add(allLoaders[i]);
            }
        }
    }
    [SerializeField] private List<AsyncLoader> loaders = new List<AsyncLoader>();

    public UnityEvent OnDoneLoading = null;

    [SerializeField] private Slider progressSlider = null;

    private void Start()
    {
        if(progressSlider != null)
        {
            progressSlider.maxValue = loaders.Count - 1;
            progressSlider.value = 0;
        }
        StartCoroutine(StartAsync());
    }

    private IEnumerator StartAsync()
    {
        for (int i = 0; i < loaders.Count; i++)
        {
            yield return loaders[i].Load();
            if (progressSlider != null)
            {
                progressSlider.DOKill();
                progressSlider.DOValue(i, 0.1f).SetEase(Ease.OutSine);
            }
        }
        OnDoneLoading?.Invoke();
    }

    private void OnDestroy()
    {
        progressSlider.DOKill();
    }
}

public abstract class AsyncLoader: MonoBehaviour
{
    [SerializeField]
    protected bool loadOnStart = false;
    [SerializeField]
    protected bool loadOnAwake = false;
    private IEnumerator Start()
    {
        if (loadOnStart)
        {
            yield return null;
            yield return Load();
        }
        else if (loadOnAwake)
        {
            yield return Load();
        }
    }

    virtual public IEnumerator Load() { yield return null; Debug.Log("LOADER NOT OVERLOADED"); }
}
