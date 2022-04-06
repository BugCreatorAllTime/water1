using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LocalizeData;

public class LocalizeManager : MonoBehaviour
{
    public static LocalizeManager InstanceOfLanguages;
    private const string LANGUAGE_KEY = "LANGUAGE_KEY";

    static LocalizeManager m_Instance;
    static Action OnLanguageChangeListener;

    [System.Serializable]
    public class LocalizeRes
    {
        public LocalizeLanguage language;
        public TextAsset textAsset;
    }

    [SerializeField] List<LocalizeRes> localizeRes;
    [SerializeField] protected LocalizeData localizeData;

    [Header("Debug")]
    [SerializeField] TextAsset debugAsset;

    #region init, load, setup localize
    void Awake()
    {
        if (m_Instance == null)
        {
            Debug.LogError("Setup localize");
            m_Instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            ReLoadData();
            return;
        }
        else
        {
            GameObject.DestroyImmediate(gameObject);
        }
    }

    private void Start()
    {
        var language = Application.systemLanguage;
        Debug.LogError("CannonMaster: System Language = " + language.ToString());
    }

    public static void ReLoadData()
    {
        if (Instance != null)
        {
            Instance.DoLoadData();
        }
    }

    public static void SetLanguage(LocalizeLanguage pLanguage)
    {
        PlayerPrefs.SetInt(LANGUAGE_KEY, (int)pLanguage);
        PlayerPrefs.Save();

        ReLoadData();
    }

    public static LocalizeLanguage GetLanguage()
    {
        var result = (LocalizeLanguage)PlayerPrefs.GetInt(LANGUAGE_KEY, (int)LocalizeLanguage.L_00_Unknow);
        if (result == LocalizeLanguage.L_00_Unknow)
        {
            result = SetupLanguageAtFirstTime();
        }
        
        return result;
    }

    public static LocalizeLanguage SetupLanguageAtFirstTime()
    {
        Debug.LogError("CannonMaster: first time login. System Language = " + Application.systemLanguage.ToString());
        LocalizeLanguage language = LocalizeLanguage.L_01_English;
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Vietnamese:
                language = LocalizeLanguage.L_02_Vi;
                break;

            //case SystemLanguage.:
            //    language = LocalizeLanguage.L_03_Hi;
            //    break;

            case SystemLanguage.Japanese:
                language = LocalizeLanguage.L_04_Ja;
                break;

            case SystemLanguage.French:
                language = LocalizeLanguage.L_05_Fr;
                break;

            case SystemLanguage.Thai:
                language = LocalizeLanguage.L_06_Th;
                break;

            case SystemLanguage.Russian:
                language = LocalizeLanguage.L_07_Ru;
                break;

            case SystemLanguage.Spanish:
                language = LocalizeLanguage.L_08_Es;
                break;
                
            case SystemLanguage.German:
                language = LocalizeLanguage.L_09_De;
                break;

            case SystemLanguage.Korean:
                language = LocalizeLanguage.L_10_Kr;
                break;

            //case SystemLanguage.:
            //    language = LocalizeLanguage.L_11_Pk;
            //    break;

            case SystemLanguage.Chinese:
                language = LocalizeLanguage.L_11_CN;
                break;

            default:
                language = LocalizeLanguage.L_01_English;
                break;
        }
        SetLanguage(language);

        return language;
    }

    void DoLoadData()
    {
        LocalizeLanguage lang = GetLanguage();
        TextAsset textAsset = AssetOfLanguage(lang);
        string jsonData = (textAsset != null) ? textAsset.text : "";

        localizeData = JsonUtility.FromJson<LocalizeData>(jsonData);
    }

    TextAsset AssetOfLanguage(LocalizeLanguage language)
    {
        foreach (LocalizeRes res in localizeRes)
        {
            if (res.language == language)
            {
                return res.textAsset;
            }
        }
        return null;
    }
    #endregion

    public static void AddLanguageChangeListener(Action pListener)
    {
        OnLanguageChangeListener -= pListener;
        OnLanguageChangeListener += pListener;
    }

    public static void RemoveLanguageChangeListener(Action pListener)
    {
        OnLanguageChangeListener -= pListener;
    }


    public static string GetTextOfId(LocalizeId localizeId)
    {
        return Instance.DoGetTextOfId(localizeId);
    }

    string DoGetTextOfId(LocalizeId localizeId)
    {
        return localizeData.TextOfId(localizeId);
    }


    public static void ChangeLanguage(LocalizeLanguage language)
    {
        SetLanguage(language);
        Debug.LogError("Change to language " + language.ToString());
        OnLanguageChangeListener?.Invoke();
    }



    #region debug
    [ContextMenu("Log json data")]
    void LogJsonData()
    {
        Debug.LogError(JsonUtility.ToJson(localizeData));
    }

    [ContextMenu("Convert json data to Object")]
    void ConvertJsonDataToObject()
    {
        localizeData = JsonUtility.FromJson<LocalizeData>(debugAsset.text);
    }
    #endregion



    static LocalizeManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<LocalizeManager>();
            }
            return m_Instance;
        }
    }
}
