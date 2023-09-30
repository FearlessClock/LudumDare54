using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
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


    void Awake()
    {
        titleStartingPosition = title.transform.position;
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

    IEnumerator ShowUI()
    {
        Debug.Log(titleStartingPosition);
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

    void Update()
    {
        Debug.Log(title.transform.position);
    }
}