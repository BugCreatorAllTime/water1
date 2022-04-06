using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDisplayer : ValueDisplayerUnified<int>
{
    [SerializeField]
    private string levelPrefix = "LEVEL";
    [SerializeField]
    private int levelNumberModification = 0;

    protected override string GetString(int value)
    {
        return levelPrefix + value;
    }

    protected override int GetCurrentValue()
    {
        return Entry.Instance.playerDataObject.Data.Level + 1 + levelNumberModification;
    }
}
