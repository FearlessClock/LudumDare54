using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScene : MonoBehaviour
{
    public void AddSceneCallback(StringVariable name)
    {
        SceneChangeManager.instance.AddScene(name, false);
    }

    public void RemoveScene(StringVariable name)
    {
        SceneManager.UnloadSceneAsync(name.value);
    }
}
