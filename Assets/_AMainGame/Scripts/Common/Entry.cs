using FH.Core.Gameplay.HelperComponent;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class Entry : MonoBehaviour
{
    public event Action OnCalculatedOfflineDuration;

    private static Entry instance;
    public static Entry Instance
    {
        get
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                instance = FindObjectOfType<Entry>();
            }
#endif
            ///
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    public PlayerDataObject playerDataObject;
    public PlayerDataSaver playerDataSaver;

    [Space]
    public GameStateManager gameStateManager;
    public GameBalance gameBalance;
    public ScreenInfo screenInfo;
    public MainCameraVibrator mainCameraVibrator;
    public ColorManager colorManager;
    public GameObject noInternetPopup;

    [Space]
    public LevelGenerator levelGenerator;
    public LevelSpawner levelSpawner;
    public UndoManager undoManager;

    [Space]
    public TubeSkinManager tubeSkinManager;
    public ThemeManager themeManager;

    [Space]
    [SerializeField]
    private UnityEvent onAppResume;

    [Space]
    public List<InactiveUpdatable> updatableObjects = new List<InactiveUpdatable>();

    public GeneralPool GeneralPool { get; private set; }

    public TimeSpan LastOfflineTimeSpan { get; private set; } = TimeSpan.MinValue;

    public bool IsFirstLaunch { get; private set; }

    public abstract class InactiveUpdatable : MonoBehaviour
    {
        public abstract void InactiveUpdate();
    }

    public void Awake()
    {
        ///
        Instance = this;

        ///
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            Debug.Log("Entry - Editor causes this Awake");
            return;
        }
#endif

        ///
        GeneralPool = new GeneralPool();

        ///
        TrySaveInstallTime();

        ///
        CalculateLastOfflineTime();
    }

    public void Update()
    {
        foreach (var item in updatableObjects)
        {
            if (!item.isActiveAndEnabled)
            {
                item.InactiveUpdate();
            }
        }
    }

    void TrySaveInstallTime()
    {
        if (playerDataObject.Data.TrySetNowAsInstallTime())
        {
            ///
            IsFirstLaunch = true;

            ///
            playerDataObject.SaveData();
        }
        else
        {
            IsFirstLaunch = false;
        }
    }

    public void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            ///
            CalculateLastOfflineTime();

            ///
            onAppResume?.Invoke();
        }
    }

    void CalculateLastOfflineTime()
    {
        var playerData = playerDataObject.Data;

        ///
        System.TimeSpan elapsedTime;
        var now = DateTime.Now;

        ///
        if (playerData.LastTimeSaved > 0)
        {
            elapsedTime = now - new DateTime(playerData.LastTimeSaved);
        }
        else
        {
            elapsedTime = TimeSpan.FromSeconds(0);
        }

        ///
        playerData.LastTimeSaved = now.Ticks;

        ///
        LastOfflineTimeSpan = elapsedTime;

        ///
        playerDataSaver.SaveThisFrame = true;

        ///
        OnCalculatedOfflineDuration?.Invoke();
    }
}
