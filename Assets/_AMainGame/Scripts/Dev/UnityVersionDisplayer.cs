using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityVersionDisplayer : MonoBehaviour
{
    [SerializeField]
    Text text;

    public void OnEnable()
    {
        text.text = Application.unityVersion;
    }
}
