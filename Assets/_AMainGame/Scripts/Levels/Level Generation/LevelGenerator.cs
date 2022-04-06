using EasyExcelGenerated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private List<int> colorCountByLevels = new List<int>();

    [Header("Test")]
    [SerializeField]
    private bool useTest;
    [SerializeField]
    private bool useTestLevelEntry;
    [SerializeField]
    private int testLevelCount;
    [SerializeField]
    private LevelEntry testLevelEntry;

    public LevelEntry GetLevelEntryForCurrentLevel(out bool isRepeatedLevel)
    {
        var level = Entry.Instance.playerDataObject.Data.Level;
        return GetLevelEntry(level, true, out isRepeatedLevel);
    }

    private LevelEntry GetLevelEntry(int level, bool tryLastLoadedLevel, out bool isRepeatedLevel)
    {
        ///
#if UNITY_EDITOR
        if (useTest)
        {
            isRepeatedLevel = true;
            if (!useTestLevelEntry)
            {
                return new LevelEntry(level, testLevelCount);
            }
            else
            {
                return testLevelEntry;
            }
        }
#endif

        int colorCount = GetColorCount(level);
        isRepeatedLevel = false;
        return new LevelEntry(level, GetColorCount(level));
    }

    private int GetColorCount(int level)
    {
        if (level >= colorCountByLevels.Count)
        {
            return colorCountByLevels[colorCountByLevels.Count - 1];
        }
        else
        {
            return colorCountByLevels[level];
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Editor_UpdateColorCountByLevel")]
    private void Editor_UpdateColorCountByLevel()
    {
        ///
        UnityEditor.Undo.RecordObject(this, "UpdateColorCountByLevel");

        ///
        var data = Resources.Load<LevelDefitions_WaterDefition_Sheet>("EasyExcelGeneratedAsset/LevelDefitions_WaterDefition_Sheet");

        ///
        colorCountByLevels = new List<int>();

        ///
        for (int i = 0; i < data.GetDataCount(); i++)
        {
            var levelData = data.GetData(i) as WaterDefition;
            colorCountByLevels.Add(levelData.colorCount);
        }
    }
#endif
}
