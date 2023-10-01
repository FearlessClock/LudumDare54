using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] GameObject sceneHolder;

    [Header("Title Position")]
    [SerializeField] GameObject title;
    [SerializeField] Vector3 titleOffset;
    Vector3 titleStartingPosition;

    [Header("title Appearance")]
    [SerializeField] float delayBeforeButton;
    [SerializeField] float apperanceDurationTitle;

    [Header("Buttons Position")]
    [SerializeField] GameObject mainMenuButtonHolder;
    [SerializeField] Vector3 buttonOffset;
    List<GameObject> mainButtonList = new List<GameObject>();
    List<Vector3> mainButtonStartingPosition = new List<Vector3>();

    [Header("Button Appearance")]
    [SerializeField] float delayBtwButton;
    [SerializeField] float apperanceDurationButton;

    [Header("Music")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider master;
    [SerializeField] Slider music;
    [SerializeField] Slider sfx;


    void Awake()
    {

        titleStartingPosition = title.transform.localPosition;
        title.transform.position += titleOffset;
        
        for (int i = 0; i < mainMenuButtonHolder.transform.childCount; i++)
        {
            GameObject button = mainMenuButtonHolder.transform.GetChild(i).gameObject;
            mainButtonList.Add(button);
            mainButtonStartingPosition.Add(button.transform.localPosition);
            button.transform.position += buttonOffset;
        }

        StartCoroutine(ShowUI());
    }

    private void Start()
    {
        GetSavedVolume();
    }

    IEnumerator ShowUI()
    {
        Sequence sequenceTitle = DOTween.Sequence();
        sequenceTitle.Append(title.transform.DOLocalMove(titleStartingPosition, apperanceDurationTitle));
        sequenceTitle.Play();

        yield return new WaitForSeconds(delayBeforeButton);

        for (int i = 0; i < mainButtonList.Count; i++)
        {
            Sequence sequenceButton = DOTween.Sequence();
            sequenceButton.Append(mainButtonList[i].transform.DOLocalMove(mainButtonStartingPosition[i], apperanceDurationButton));
            sequenceButton.Play();
            yield return new WaitForSeconds(delayBtwButton);
        }
       
    }

    void ChangeMenuScene(int id)
    {
        Sequence sequenceTitle = DOTween.Sequence();

        switch (id)
        {
            case 0:
                sequenceTitle.Append(sceneHolder.transform.DOLocalMove(new Vector3(0, 0, 0), 1));
                break;
            case 1:
                sequenceTitle.Append(sceneHolder.transform.DOLocalMove(new Vector3(1920, 0, 0), 1));
                break;
            default:
                break;
        }
        sequenceTitle.Play();
    }

    #region Buttons

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsButton()
    {
        ChangeMenuScene(1);
    }

    public void BackToMainButton()
    {
        ChangeMenuScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    #endregion

    #region Volume

    void GetSavedVolume()
    {
        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            master.value = -20;
            music.value = -20;
            sfx.value = -20;
        }
        else
        {
            master.value = PlayerPrefs.GetFloat("MasterVolume");
            music.value = PlayerPrefs.GetFloat("MusicVolume");
            sfx.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    public void ChangeVolumeMaster(float value)
    {
        audioMixer.SetFloat("MasterVolume", value);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void ChangeVolumeMusic(float value)
    {
        audioMixer.SetFloat("MusicVolume", value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void ChangeVolumeSFX(float value)
    {
        audioMixer.SetFloat("SFXVolume", value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
    #endregion
}