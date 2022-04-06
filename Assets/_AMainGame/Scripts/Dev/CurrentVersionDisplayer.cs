using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentVersionDisplayer : MonoBehaviour
{
    [SerializeField]
    Text text;

    public void OnEnable()
    {
        text.text = Application.version;
    }
}
