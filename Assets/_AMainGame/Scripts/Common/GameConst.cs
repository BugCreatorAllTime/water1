using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConst
{
    ///
    public const int ColorCount = 12;

    // Tag
    public const string TagColorChanger = "ColorChanger";
    public const string TagFinishLine = "FinishLine";
    public const string TagPlayer = "Player";
    public const string TagObstacle = "Obstacle";
    public const string TagRoadBall = "RoadBall";

    // Layer
    // public const string LayerStickman = "Stickman";   

    // IAP
    public const string IAPItem_RemoveAds = "liquidsort_removeads_0";
    public const string IAPItem_CoinPack1 = "liquidsort_coinpack_1";
    public const string IAPItem_CoinPack2 = "liquidsort_coinpack_2";
    public const string IAPItem_CoinPack3 = "liquidsort_coinpack_3";

    ///
    public const int LevelsToUnlockNewBackground = 5;
    public const int PlayerSkinCount = 10;

    ///
    public const string NormalLevelResourcePathFormat = "Levels/Level_{0:000}";
    public const string BonusLevelResourcePathFormat = "Levels/Level_{0:000}_b";

    ///
    public const int MaxLayerCount = 5;
}
