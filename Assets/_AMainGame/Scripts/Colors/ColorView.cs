using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorView : MonoBehaviour
{
    [SerializeField]
    private ColorSource colorSource;

    [Space]
    [SerializeField]
    private bool useStaticColor;
    [SerializeField]
    private GameColor gameColor;

    public int ColorId
    {
        get
        {
            if (useStaticColor)
            {
                return (int)gameColor;
            }
            else
            {
                switch (colorSource)
                {
                    case ColorSource.Player:
                        return Entry.Instance.colorManager.PlayerColorId;
                    case ColorSource.RoadBall:
                        return Entry.Instance.colorManager.RoadBallColorId;
                    case ColorSource.Obstacle:
                        return Entry.Instance.colorManager.ObstacleColorId;
                    default:
                        throw new System.NotImplementedException();
                }
            }
        }
    }

    protected abstract void SetColor(Color color);

    private enum ColorSource
    {
        RoadBall,
        Player,
        Obstacle
    }

    public void OnEnable()
    {
        ///
        SetColor();

        ///
        Entry.Instance.colorManager.OnGotNewColors += ColorManager_OnGotNewColors;
    }

    public void OnDisable()
    {
        ///
        Entry.Instance.colorManager.OnGotNewColors -= ColorManager_OnGotNewColors;
    }

    private void ColorManager_OnGotNewColors()
    {
        SetColor();
    }

    [ContextMenu("SetColor")]
    public void SetColor()
    {
        var color = Entry.Instance.colorManager.GetColor(ColorId);
        SetColor(color);
    }
}
