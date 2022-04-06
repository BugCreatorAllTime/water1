using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDPRPopupController : MonoBehaviour
{
    public void Awake()
    {
#if !UNITY_STANDALONE
        if (GDPRManager.Asked)
        {
            LoadMainScene();
        } 
#else
        bool tmp = GDPRManager.Asked;
        LoadMainScene();
#endif
    }

    public void LoadMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void SetConsent(bool isConsent)
    {
        GDPRManager.IsConsent = isConsent;
    }
}
