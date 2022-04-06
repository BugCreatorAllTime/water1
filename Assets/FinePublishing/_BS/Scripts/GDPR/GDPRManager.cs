using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDPRManager : MonoBehaviour
{
    const string GDPRConsentKey = "GDPRConsent";

    static bool inited = false;
    static bool isConsent;
    static bool asked = false;

    public static bool IsConsent
    {
        get
        {
            ///
            TryInit();

            ///
            return isConsent;
        }

        set
        {
            if (isConsent != value || !asked)
            {
                ///
                isConsent = value;

                ///
                PlayerPrefs.SetInt(GDPRConsentKey, isConsent ? 1 : 0);

                ///
                asked = true;
            }
        }
    }

    public static bool Asked
    {
        get
        {
            ///
            TryInit();

            ///
            return asked;
        }
    }

    static void TryInit()
    {
        ///
        if (inited)
        {
            return;
        }

        ///
        asked = PlayerPrefs.HasKey(GDPRConsentKey);

        ///
        isConsent = PlayerPrefs.GetInt(GDPRConsentKey, 0) == 1;

        ///
        inited = true;
    }
}
