using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformObject : MonoBehaviour
{
    [SerializeField]
    bool editorVisibility = true;
    [SerializeField]
    bool defaultVisibility = true;
    [SerializeField]
    bool iOSVisibility = true;
    [SerializeField]
    bool androidVisibility = true;

    [Space]
    [SerializeField]
    UnityEvent onVisible;
    [SerializeField]
    UnityEvent onNotVisibile;

    public void Awake()
    {
        ///
        bool visible = false;

        ///
#if UNITY_ANDROID
        gameObject.SetActive(androidVisibility);
        visible = androidVisibility;
#elif UNITY_IOS
        gameObject.SetActive(iOSVisibility);
        visible = iOSVisibility;
#elif UNITY_EDITOR
        gameObject.SetActive(editorVisibility || defaultVisibility);
        visible = editorVisibility || defaultVisibility;
#else
        gameObject.SetActive(defaultVisibility);
        visible = defaultVisibility;
#endif

        ///
        if (visible)
        {
            onVisible?.Invoke();
        }
        else
        {
            onNotVisibile?.Invoke();
        }
    }
}
