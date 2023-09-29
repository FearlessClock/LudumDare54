using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InitializationController : MonoBehaviour
{
    [Button("Get All Loaders", DirtyOnClick = true), PropertyOrder(-10)]
    private void LoadLoaders()
    {
        Loader[] allLoaders = FindObjectsOfType<Loader>();
        for (int i = 0; i < allLoaders.Length; i++)
        {
            bool isAlreadyLoaded = false;
            for (int j = 0; j < loaders.Count; j++)
            {
                if (loaders[j] != null && loaders[j].Equals(allLoaders[i]))
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
    [SerializeField] private List<Loader> loaders = new List<Loader>();

    public UnityEvent OnDoneLoading = null;

    [SerializeField] private Slider progressSlider = null;

    private void Start()
    {
        if(progressSlider != null)
        {
            progressSlider.maxValue = loaders.Count - 1;
            progressSlider.value = 0;
        }
        LoadComponents();
    }

    private void LoadComponents()
    {
        for (int i = 0; i < loaders.Count; i++)
        {
            bool taskRes = loaders[i].Load();
            if (!taskRes)
            {
                Debug.LogError("Something went wrong when loading the information " + loaders[i].name);
            }
            if (progressSlider != null)
            {
                progressSlider.DOKill();
                progressSlider.DOValue(i, 0.1f).SetEase(Ease.OutSine);
            }
        }
        OnDoneLoading?.Invoke();
    }
}

public abstract class Loader : MonoBehaviour
{
    [SerializeField]
    protected bool loadOnStart = false;
    [SerializeField]
    protected bool loadOnAwake = false;
    private void Start()
    {
        if (loadOnStart)
        {
            Load();
        }
    }
    private void Awake()
    {
        if (loadOnAwake)
        {
            Load();
        }
    }

    virtual public bool Load() { Debug.Log("LOADER NOT OVERLOADED"); return true; }
}
