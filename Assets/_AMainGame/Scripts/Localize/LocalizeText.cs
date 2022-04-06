using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LocalizeData;

public class LocalizeText : MonoBehaviour
{
    [SerializeField] LocalizeId m_localizeId;
    [SerializeField] Text m_text;


    void Start()
    {
        SetupText();
        InitListener();

        SetupLocalize();
    }

    void InitListener()
    {
        LocalizeManager.AddLanguageChangeListener(OnLanguageChanged);
    }

    void SetupLocalize()
    {
        if (m_text != null)
        {
            m_text.text = LocalizeManager.GetTextOfId(m_localizeId);
            Debug.LogError("Setup localize for " + name + " _ " + m_localizeId.ToString() + " __ " + m_text.text);
        }
    }

    void OnLanguageChanged()
    {
        if (gameObject != null)
        {
            SetupLocalize();
        }
    }

    private void OnDestroy()
    {
        LocalizeManager.RemoveLanguageChangeListener(OnLanguageChanged);
    }


    [ContextMenu("Setup Text")]
    void SetupText()
    {
        m_text = GetComponent<Text>();
    }
}
