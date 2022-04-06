using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryDetector : MonoBehaviour
{
    [SerializeField]
    private LevelDrawer levelDrawer;

    public void LateUpdate()
    {
        ///
        if (Entry.Instance.gameStateManager.CurrentState != GameState.Beat)
        {
            return;
        }

        ///
        bool isVictory = true;

        ///
        foreach (var tube in levelDrawer.ActiveTubes)
        {
            ///
            if ((tube.CurrentWaterHeight != TubeData.GlassHeight || tube.ColorCount != 1) && (tube.ColorCount != 0))
            {
                isVictory = false;
                break;
            }

            ///
            var tubeMovementState = tube.tubeMovement.State;
            if (tubeMovementState != TubeMovementState.Idle)
            {
                isVictory = false;
                break;
            }
        }

        ///
        if (isVictory)
        {
            Entry.Instance.gameStateManager.SwitchToOver(true);
        }
    }
}
