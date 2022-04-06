using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject level_0_Tutorial;
    [SerializeField]
    private GameObject level_1_Tutorial;

    public void Awake()
    {
        var currentLevel = Entry.Instance.playerDataObject.Data.Level;
        if (currentLevel > 1)
        {
            level_0_Tutorial.gameObject.SetActive(false);
            level_1_Tutorial.gameObject.SetActive(false);
        }
        else
        {
            ///
            level_0_Tutorial.gameObject.SetActive(currentLevel == 0);
            level_1_Tutorial.gameObject.SetActive(currentLevel == 1);

            ///
            if (currentLevel == 0)
            {
                Entry.Instance.gameStateManager.OnBeforeBeat += GameStateManager_OnBeforeBeat;
            }
        }
    }

    private void GameStateManager_OnBeforeBeat()
    {
        var currentLevel = Entry.Instance.playerDataObject.Data.Level;
        if (currentLevel == 1)
        {
            level_1_Tutorial.gameObject.SetActive(true);
            Entry.Instance.gameStateManager.OnBeforeBeat -= GameStateManager_OnBeforeBeat;
        }
    }
}
