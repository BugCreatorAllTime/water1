using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UnifiedText : MonoBehaviour
{
    [SerializeField]
    private Text uguiText;
    [SerializeField]
    private TMP_Text tmp_Text;

    public string Text
    {
        get
        {
            if (uguiText != null)
            {
                return uguiText.text;
            }
            else if (tmp_Text != null)
            {
                return tmp_Text.text;
            }
            else
            {
                throw new System.Exception();
            }
        }

        set
        {
            SetText(value);
        }
    }

#if UNITY_EDITOR
    public void Reset()
    {
        uguiText = GetComponent<Text>();
        tmp_Text = GetComponent<TMP_Text>();
    }
#endif

    public void SetText(string text)
    {
        if (uguiText != null)
        {
            uguiText.text = text;
        }
        else if (tmp_Text != null)
        {
            tmp_Text.text = text;
        }
        else
        {
            throw new System.Exception();
        }
    }
}
