using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FH.Core.Architecture.WritableData;

[CreateAssetMenu(fileName = "PlayerData", menuName = "BS2/PlayerData")]
public class PlayerDataObject : WritableScriptableObject<PlayerData>
{
    [Header("Test")]
    [SerializeField]
    private int test_weaponId = 0;
    [SerializeField]
    private int test_weaponLevel;

    protected override void OnDataLoaded(PlayerData data)
    {
        base.OnDataLoaded(data);
        
        ///
        data.CorrectData();
        data.SetCurrentLevelToMaxLevelReached();
        
        ///
        data.UpdateVersions();
    }

#if UNITY_EDITOR

#endif

}
