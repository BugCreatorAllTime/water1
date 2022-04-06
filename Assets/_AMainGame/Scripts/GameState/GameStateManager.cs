using System.Collections;
using System.Collections.Generic;
using FMod;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public event System.Action OnStateChanged;
    public event System.Action OnBeforeBeat;
    public event System.Action OnReset;
    public event System.Action OnUndo;

    private GameState current_State = GameState.Prepare;

    public bool WonFlag { get; private set; }

    public bool RemovedObstacleFlag { get; set; }

    public float LastLevelProgress { get; set; }

    public GameState CurrentState
    {
        get => current_State;
        private set
        {
            if (current_State != value)
            {
                ///
                var savedValue = current_State;

                ///
                current_State = value;
                try
                {
                    OnStateChanged?.Invoke();
                }
                catch (System.Exception e)
                {
                    ///
                    current_State = savedValue;

                    ///
                    throw e;
                }
            }
        }
    }

    public int GameCountThisSession { get; private set; }

    public void SwitchToPrepare()
    {
        if (CurrentState != GameState.Prepare)
        {
            CurrentState = GameState.Prepare;
        }
    }

    public void SwitchToBeat()
    {
        if (CurrentState != GameState.Beat)
        {
            ///
            RemovedObstacleFlag = false;

            ///
            OnBeforeBeat?.Invoke();

            ///
            CurrentState = GameState.Beat;
        }
    }

    public void SwitchToOver(bool won)
    {
        SwitchToOver(won, false);
    }

    public void SwitchToOver(bool won, bool canSkip)
    {
        ///
        var playerData = Entry.Instance.playerDataObject.Data;

        ///
        if (CurrentState != GameState.Beat)
        {
            return;
        }

        ///
        WonFlag = won;

        ///
        playerData.IncreaseGameCount();
        GameCountThisSession++;

        ///
        bool isReplay = playerData.Level < playerData.MaxLevelReached;

        ///
        if (won)
        {
            ///
            if (!Entry.Instance.levelSpawner.IsRepeatedLevel)
            {
                playerData.LevelUp();
            }

            ///
            playerData.SetCurrentLevelToMaxLevelReached();
        }

        ///
        CurrentState = GameState.Over;
    }

    public void ResetCurrentGame()
    {
        ///
        //FMod.InterstitialAds.ShowQuickForced(null);
        Ads_Controller.Show_InterstitialAds();
        MyAnalytics.Firebase_ResetLevel(Entry.Instance.playerDataObject.Data.Level);

        ///
        if (CurrentState != GameState.Over)
        {
            OnReset?.Invoke();
        }
    }

    public void UndoLastStep()
    {
        ///
        OnUndo?.Invoke();
    }
}
