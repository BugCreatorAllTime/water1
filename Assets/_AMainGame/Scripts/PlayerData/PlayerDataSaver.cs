using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSaver : MonoBehaviour
{
    public static event System.Action OnBeforeSave = delegate { };

    [SerializeField]
    float mobileInterval = 300;
    [SerializeField]
    float standAloneInterval = 10.0f;

    float interval;

    public bool SaveThisFrame { get; set; } = false;

    public void Start()
    {
        StartCoroutine(SaveLoop());
    }

    public void LateUpdate()
    {
        if (SaveThisFrame)
        {
            ///
            Save(Entry.Instance.playerDataObject);

            ///
            SaveThisFrame = false;
        }
    }

    IEnumerator SaveLoop()
    {
        ///
#if UNITY_STANDALONE
        interval = standAloneInterval;
#else
        interval = mobileInterval;
#endif

        ///
        var playerDataObject = Entry.Instance.playerDataObject;

        ///
        while (true)
        {
            ///
            yield return new WaitForSecondsRealtime(interval);

            ///
            AddTimeSpentInGame(playerDataObject);
            Save(playerDataObject);
        }
    }

    void AddTimeSpentInGame(PlayerDataObject playerDataObject)
    {
        playerDataObject.Data.AddTimeSpentInGame(interval);
    }

    static void Save(PlayerDataObject playerDataObject)
    {
        ///
        OnBeforeSave();

        ///
        playerDataObject.Data.LastTimeSaved = System.DateTime.Now.Ticks;
        playerDataObject.SaveData();
    }

    public void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Save(Entry.Instance.playerDataObject);
        }
    }

    public void OnApplicationQuit()
    {
        Save(Entry.Instance.playerDataObject);
    }
}
