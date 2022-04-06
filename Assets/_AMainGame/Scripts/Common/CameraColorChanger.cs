using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraColorChanger : MonoBehaviour
{
    [SerializeField]
    private List<Color> colors;

    private int lastColorId = -1;
    private int lastGameCount = -1;

    public void Awake()
    {
        ///
        TryChangeColor();

        ///
        Entry.Instance.gameStateManager.OnBeforeBeat += GameStateManager_OnBeforeBeat;
    }

    private void GameStateManager_OnBeforeBeat()
    {
        ///
        TryChangeColor();
    }

    public void TryChangeColor()
    {
        ///
        int currentGameCount = Entry.Instance.playerDataObject.Data.GameCount;

        ///
        if (currentGameCount == lastGameCount)
        {
            return;
        }

        ///
        lastGameCount = currentGameCount;

        ///
        var nextColorId = lastColorId;
        while (nextColorId == lastColorId)
        {
            nextColorId = Random.Range(0, colors.Count);
        }

        ///
        lastColorId = nextColorId;
        var color = colors[nextColorId];

        ///
        GetComponent<Camera>().backgroundColor = color;
    }
}
