using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    static EndScreen instance;
    public static EndScreen Instance { get => instance; }

    [SerializeField] GameObject screen;
    [SerializeField] Image background;
    [SerializeField] Sprite backgroundWin;
    [SerializeField] Sprite backgroundLoose;
    [SerializeField] TextMeshProUGUI dataText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void CallEndScreen(bool isWin)
    {
        screen.SetActive(true);

        if (isWin)
        {
            background.sprite = backgroundWin;
            dataText.text = $"Time : XXXX";
        }
        else
        {
            background.sprite = backgroundLoose;
            dataText.text = $"Soul get : XXXX";
        }
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.W)) CallEndScreen(true);
        else if (Input.GetKeyDown(KeyCode.L)) CallEndScreen(false);*/
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
