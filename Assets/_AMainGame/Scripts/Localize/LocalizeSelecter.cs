using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LocalizeData;

public class LocalizeSelecter : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;

    void Start()
    {
        InitListener();
        dropdown.value = (int)(LocalizeManager.GetLanguage()) - 1;
    }

    void InitListener()
    {
        dropdown.onValueChanged.AddListener(OnValueChange);
    }

    void OnValueChange(int id)
    {
        Debug.LogError("On Value Changed " + id);
        LocalizeLanguage language = (LocalizeLanguage)(id + 1);

        LocalizeManager.ChangeLanguage(language);
    }
}
