using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel1 : MonoBehaviour
{
    void Start()
    {
        Entry.Instance.gameStateManager.OnStateChanged += GameStateManager_OnStateChanged;
    }

    private void GameStateManager_OnStateChanged()
    {
        var gameStateMan = Entry.Instance.gameStateManager;
        if (gameStateMan.CurrentState == GameState.Over && gameStateMan.WonFlag)
        {
            Entry.Instance.gameStateManager.OnStateChanged -= GameStateManager_OnStateChanged;
            gameObject.SetActive(false);
        }
    }
}
