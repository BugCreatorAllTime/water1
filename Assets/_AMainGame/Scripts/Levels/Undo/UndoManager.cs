using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    private List<UndoStep> undoSteps = new List<UndoStep>();

    public void Awake()
    {
        Entry.Instance.levelSpawner.OnSpawnedLevelEntry += LevelSpawner_OnSpawnedLevelEntry;
    }

    public void RegisterStep(UndoStep undoStep)
    {
        undoSteps.Add(undoStep);
    }

    private void LevelSpawner_OnSpawnedLevelEntry()
    {
        undoSteps.Clear();
    }

    public bool TryPerformingUndo()
    {
        ///
        if (undoSteps.Count == 0)
        {
            return false;
        }

        ///
        var undoStep = undoSteps[undoSteps.Count - 1];
        undoSteps.RemoveAt(undoSteps.Count - 1);

        ///
        var colorId = undoStep.receivedTube.TopColorId;
        undoStep.receivedTube.tubeView.RemoveWater(undoStep.amount);
        undoStep.givedTube.tubeView.AddWater(colorId, undoStep.amount);

        ///
        undoStep.receivedTube.CurrentWaterHeight -= undoStep.amount;
        undoStep.givedTube.CurrentWaterHeight += undoStep.amount;

        ///
        undoStep.receivedTube.tubeView.RoundLastWaterSegment();
        undoStep.givedTube.tubeView.RoundLastWaterSegment();

        ///
        undoStep.receivedTube.tubeMovement.UpdateCompleteStatus();
        undoStep.givedTube.tubeMovement.UpdateCompleteStatus();

        ///
        return true;
    }
}
