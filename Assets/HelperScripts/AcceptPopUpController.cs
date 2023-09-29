using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AcceptPopUpController : MonoBehaviour
{
    private bool hasResponded = false;
    public bool response = false;
    [SerializeField] private TextMeshProUGUI textmesh = null;
    [SerializeField] private TextMeshProUGUI currentLevel = null;
    [SerializeField] private Image icon = null;
    public bool DestroyOnReponse = true;
    [SerializeField] private BoolVariable isPopUpOpen = null;

    private void OnEnable()
    {
        isPopUpOpen.SetValue(true);
    }

    private void OnDisable()
    {
        isPopUpOpen.SetValue(false);
    }

    public void SetIcon(Sprite icon)
    {
        this.icon.color = Color.white;
        this.icon.sprite = icon;
    }

    public void SetText(string text)
    {
        textmesh.SetText(text);
    }
    
    public void SetLevel(string text)
    {
        currentLevel.SetText(text);
    }

    public void Accept()
    {
        hasResponded = true;
        response = true;
        if (DestroyOnReponse)
        {
            Destroy(this.gameObject, 0.1f);
        }
    }

    public void Decline()
    {
        hasResponded = true;
        response = false;
        if (DestroyOnReponse)
        {
            Destroy(this.gameObject, 0.1f);
        }
    }

    public IEnumerator DoYouAccept()
    {
        hasResponded = false;
        yield return new WaitUntil(() => hasResponded == true);
    }

    public void Reset()
    {
        hasResponded = false;
        response = false;
    }
}
