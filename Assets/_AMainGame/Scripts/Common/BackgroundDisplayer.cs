using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundDisplayer : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage;

    public void Start()
    {
        PlayerData.OnThemeChanged += PlayerData_OnThemeChanged;
    }

    private void PlayerData_OnThemeChanged()
    {
        backgroundImage.sprite = Entry.Instance.themeManager.GetCurrentTheme().sprite;
    }

    public void OnEnable()
    {
        backgroundImage.sprite = Entry.Instance.themeManager.GetCurrentTheme().sprite;
    }
}
