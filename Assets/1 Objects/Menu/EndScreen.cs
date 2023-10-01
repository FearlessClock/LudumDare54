using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] GameObject screen;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI dataText;
    public void CallEndScreen(bool isWin)
    {
        screen.SetActive(true);

        if (isWin)
        {
            titleText.text = "Victory !!";
            dataText.text = $"Time : XXXX";
        }
        else
        {
            titleText.text = "Defeat...";
            dataText.text = $"Soul get : XXXX";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) CallEndScreen(true);
        else if (Input.GetKeyDown(KeyCode.L)) CallEndScreen(false);
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
