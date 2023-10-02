using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class EndScreen : MonoBehaviour
{
    static EndScreen instance;
    public static EndScreen Instance { get => instance; }

    [SerializeField] GameObject screen;
    [SerializeField] Image background;
    [SerializeField] Sprite backgroundWin;
    [SerializeField] Sprite backgroundLoose;
    [SerializeField] TextMeshProUGUI dataText;
    private float secondTimer;
    [SerializeField] IntVariable score;

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
        Time.timeScale = 0;
        screen.SetActive(true);

        if (isWin)
        {
            SoundTransmitter.Instance.Play("Victory");

            background.sprite = backgroundWin;
            dataText.text = $"Time : {(int)secondTimer / 60}min {(int)secondTimer % 60}sec";
        }
        else
        {
            SoundTransmitter.Instance.Play("Defeat");
            background.sprite = backgroundLoose;
            dataText.text = $"Soul won : " + score.value;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) CallEndScreen(true);
        else if (Input.GetKeyDown(KeyCode.L)) CallEndScreen(false);
        secondTimer += Time.deltaTime;
    }

    public void Menu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(1);
    }
}
