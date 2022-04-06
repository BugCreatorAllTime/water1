using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public event System.Action OnGotNewColors;

    [SerializeField]
    private Color obstacleColor;

    [Space]
    [SerializeField]
    private List<ColorInfo> colors;

    [Header("Test")]
    [SerializeField]
    private bool useTest;
    [SerializeField]
    private int testPlayerColorId;
    [SerializeField]
    private int testRoadBallColorId;

    [System.Serializable]
    public struct ColorInfo
    {
        public Color normalColor;
        public Color obstacleHighlighted;
        public Color playerHighlighted;
    }

    public Color ObstacleColor => obstacleColor;
    public int ObstacleColorId => -1;
    public int PlayerColorId { get; private set; }
    public int RoadBallColorId { get; private set; }

    private int lastGameCountToGetNewColors = -1;

    public void Awake()
    {
        ///
        TryGetNewColors();

        ///
        Entry.Instance.gameStateManager.OnBeforeBeat += GameStateManager_OnBeforeBeat;
    }

    private void GameStateManager_OnBeforeBeat()
    {
        TryGetNewColors();
    }

    public Color GetColor(int id)
    {
        ///
        if (id == ObstacleColorId)
        {
            return ObstacleColor;
        }

        ///
        return colors[id].normalColor;
    }

    public int GetRandomColorId()
    {
        return Random.Range(0, colors.Count);
    }

    public int GetNewRandomColor(int currentColorId)
    {
        ///
        int newColorId = GetRandomColorId();
        while (newColorId == currentColorId)
        {
            newColorId = GetRandomColorId();
        }

        ///
        return newColorId;
    }

    public void TryGetNewColors()
    {
        ///
        int currentGameCount = Entry.Instance.playerDataObject.Data.GameCount;

        ///
        if (currentGameCount == lastGameCountToGetNewColors)
        {
            return;
        }

        ///
        lastGameCountToGetNewColors = currentGameCount;

        ///
        PlayerColorId = GetNewRandomColor(PlayerColorId);
        RoadBallColorId = GetNewRandomColor(PlayerColorId);

        ///
#if UNITY_EDITOR
        if (useTest)
        {
            PlayerColorId = testPlayerColorId;
            RoadBallColorId = testRoadBallColorId;
        }
#endif

        ///
        OnGotNewColors?.Invoke();
    }
}
