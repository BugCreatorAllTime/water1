using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD;
using System.Net.NetworkInformation;

[CreateAssetMenu(fileName = "GameBalance", menuName = "BS2/GameBalance")]
public class GameBalance : ScriptableObject
{
    public int minGameCountToGetSkipLevel = 3;

    [SerializeField]
    private PlayerDataObject playerDataObject;

    [Space]
    public int minLevelToHaveObstacles = 5;
    public int freeDifficultySwitchCount = 4;
    public float easyIntervalFactor = 1 / 3.0f;

    [Space]
    [SerializeField]
    private List<int> skinUnlockLevelCounts = new List<int>();

    private List<int> skinUnlockLevels = null;

    public int GetTotalSkinAvailableCount(int playerLevel)
    {
        ///
        TryBuildSkinUnlockLevels();

        ///
        int skinCount = 0;
        for (int i = 0; i < skinUnlockLevels.Count; i++)
        {
            ///
            if (skinUnlockLevels[i] <= playerLevel)
            {
                skinCount = i + 1;
            }
            else
            {
                return skinCount;
            }

            ///
            if (skinCount >= GameConst.PlayerSkinCount)
            {
                return GameConst.PlayerSkinCount;
            }
        }

        ///
        return skinCount;
    }

    /// <summary>
    /// Total available minus already unlocked
    /// </summary>
    /// <param name="playerLevel"></param>
    /// <returns></returns>
    public int GetUnlockableSkinCount(int playerLevel)
    {
        ///
        int totalSkinAvailable = GetTotalSkinAvailableCount(playerLevel);
        var playerData = Entry.Instance.playerDataObject.Data;

        ///
        var unlockableCount = totalSkinAvailable - playerData.UnlockedTubeSkinCount;

        ///
        return unlockableCount < 0 ? 0 : unlockableCount;
    }

    public int GetNextLevelToUnlockNewSkin(int playerLevel)
    {
        ///
        int totalSkinAvailable = GetTotalSkinAvailableCount(playerLevel);

        ///
        if (totalSkinAvailable >= GameConst.PlayerSkinCount)
        {
            return -1;
        }

        ///
        return skinUnlockLevels[totalSkinAvailable];
    }

    private void TryBuildSkinUnlockLevels()
    {
        ///
        if (skinUnlockLevels != null && skinUnlockLevels.Count > 0)
        {
            return;
        }

        ///
        skinUnlockLevels = new List<int>();

        ///
        int currentLevel = 0;
        for (int i = 0; i < skinUnlockLevelCounts.Count; i++)
        {
            currentLevel += skinUnlockLevelCounts[i];
            skinUnlockLevels.Add(currentLevel);
        }
    }

    [System.Obsolete]
    public double GetSkinCost(int unlockedSkinCount)
    {
        throw new System.NotImplementedException();
    }
}
