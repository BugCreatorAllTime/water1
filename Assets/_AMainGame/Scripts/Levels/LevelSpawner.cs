using System.Collections;
using System.Collections.Generic;
using FMod;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public static LevelSpawner InstanceOfLevelSpawner;
    
    public event System.Action OnSpawnedLevelEntry;

    private int lastSpawnedGameCount = -1;
    private int last_SpawnedLevel = -1;

    public LevelEntry CurrentLevelEntry { get; private set; }

    public bool IsRepeatedLevel { get; private set; }
    
    public void Start()
    {
        ///
        SpawnLevelEntry();

        ///
        Entry.Instance.gameStateManager.OnReset += GameStateManager_OnReset;
        Entry.Instance.gameStateManager.OnStateChanged += GameStateManager_OnStateChanged;
    }

    private void GameStateManager_OnReset()
    {
        RespawnCurrentLevel();
    }

    public void RespawnCurrentLevel()
    {
        SpawnLevelEntry();
    }

    private void GameStateManager_OnStateChanged()
    {
        if (Entry.Instance.gameStateManager.CurrentState == GameState.Beat)
        {
            ///
            var playerData = Entry.Instance.playerDataObject.Data;

            ///
            if (lastSpawnedGameCount != playerData.GameCount || last_SpawnedLevel != playerData.Level)
            {
                SpawnLevelEntry();
            }
        }
    }
    Scene_Switcher scene_Switcher=new Scene_Switcher();
    [ContextMenu("SpawnLevelEntry")]
    
    public void SpawnLevelEntry()
    {
        ///
        var playerData = Entry.Instance.playerDataObject.Data;
       
        ///
      
        lastSpawnedGameCount = playerData.GameCount;
        last_SpawnedLevel = playerData.Level;       
        PlayerPrefs.SetInt("playerData.Level", last_SpawnedLevel);
        PlayerPrefs.Save();

        ///
        bool isRepeatedLevel;
        var levelEntryPrototype = Entry.Instance.levelGenerator.GetLevelEntryForCurrentLevel(out isRepeatedLevel);
        IsRepeatedLevel = isRepeatedLevel;
        CurrentLevelEntry = levelEntryPrototype;    
        

        ///
        OnSpawnedLevelEntry?.Invoke();

        MyAnalytics.Firebase_Start_Level(CurrentLevelEntry.levelId);
    }
}
