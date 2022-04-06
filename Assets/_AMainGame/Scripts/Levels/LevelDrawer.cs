using FH.Core.Architecture.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDrawer : MonoBehaviour
{
    private const int MaxSingleRowTubeCount = 4;

    [SerializeField]
    private Tube tubeProtoype;
    [SerializeField]
    private float tubeWidth;
    [SerializeField]
    private float tubeMaxSpacing;
    [SerializeField]
    private float playfieldWidth;

    [Space]
    [SerializeField]
    private float upperTubePosY = 3;
    [SerializeField]
    private float thirdTubePosY = 6;

    [Space]
    [SerializeField]
    private Transform playfieldTransform;
    [SerializeField]
    private float playFieldPosY_OneRow = 0;
    [SerializeField]
    private float playFieldPosY_TwoRow = -2;
    [SerializeField]
    private float playFieldPosY_ThreeRow = -4;

    private List<Tube> activeTubes = new List<Tube>();
    private List<float> tubePosXs_Under = new List<float>();
    private List<float> tubePosXs_Upper = new List<float>();
    private List<float> tubePosXs_Third = new List<float>();

    private int tubeCount_Under;
    private int tubeCount_Upper;
    private int tubeCount_Third;

    public IEnumerable<Tube> ActiveTubes
    {
        get
        {
            for (int i = 0; i < activeTubes.Count; i++)
            {
                yield return activeTubes[i];
            }
        }
    }

    public void Awake()
    {
        ///
        tubeProtoype.gameObject.SetActive(false);

        ///
        
        var levelEntry = Entry.Instance.levelSpawner.CurrentLevelEntry;
        if (levelEntry != null)
        {
            DrawLevel(levelEntry);
        }

        ///
        Entry.Instance.levelSpawner.OnSpawnedLevelEntry += LevelSpawner_OnSpawnedLevelEntry;
    }

    private void LevelSpawner_OnSpawnedLevelEntry()
    {
        var levelEntry = Entry.Instance.levelSpawner.CurrentLevelEntry;
        DrawLevel(levelEntry);
    }

    public void AddEmptyTube()
    {
        ///
        var emptyTube = CreateEmptyTube();

        ///        
        PlaceTubes();

        ///
        SetPlayfieldPosY();
    }

    private Tube CreateEmptyTube()
    {
        ///
        var tube = SpawnNewTube();
        var tubeData = new TubeData()
        {
            waterData = new List<WaterData>(),
        };
        tube.SetInitialData(tubeData);

        ///
        return tube;
    }

    private void DrawLevel(LevelEntry levelEntry)
    {
        ///
        DestroyAllActiveTubes();

        ///
        TubeView.MaxWorldGlassHeight = 0;

        ///
        if (TubeMovement.ActiveTube != null)
        {
            TubeMovement.ActiveTube.tubeMovement.BecomeIdleImmediately();
        }

        ///
        CreateTubes(levelEntry.tubes);
        PlaceTubes();

        ///
        SetPlayfieldPosY();
    }

    private void PlaceTubes()
    {
        ///
        DistributeTubesToRows(activeTubes.Count);

        ///
        CalculateTubePosXs(tubeCount_Under, tubePosXs_Under);
        CalculateTubePosXs(tubeCount_Upper, tubePosXs_Upper);
        CalculateTubePosXs(tubeCount_Third, tubePosXs_Third);

        ///
        for (int i = 0; i < tubeCount_Under; i++)
        {
            PlaceTube(activeTubes[i], tubePosXs_Under[i], 0);
        }
        for (int i = tubeCount_Under; i < activeTubes.Count - tubeCount_Third; i++)
        {
            PlaceTube(activeTubes[i], tubePosXs_Upper[i - tubeCount_Under], upperTubePosY);
        }
        for (int i = tubeCount_Upper + tubeCount_Under; i < activeTubes.Count; i++)
        {
            PlaceTube(activeTubes[i], tubePosXs_Third[i - tubeCount_Upper - tubeCount_Under], thirdTubePosY);
        }
    }

    private void CreateTubes(List<TubeData> tubeData)
    {
        ///
        activeTubes.Clear();

        ///
        for (int i = 0; i < tubeData.Count; i++)
        {
            ///
            var tube = SpawnNewTube();

            ///
            tube.SetInitialData(tubeData[i]);
        }
    }

    private void SetPlayfieldPosY()
    {
        var p = playfieldTransform.position;
        p.y = tubeCount_Third > 0 ? playFieldPosY_ThreeRow : (tubeCount_Upper > 0 ? playFieldPosY_TwoRow : playFieldPosY_OneRow);
        playfieldTransform.position = p;
    }

    private void PlaceTube(Tube tube, float posX, float posY)
    {
        ///
        tube.transform.localPosition = new Vector3(posX, posY, 0);
        tube.tubeMovement.SaveInitialPosition();
    }

    private Tube SpawnNewTube()
    {
        ///
        var tube = Instantiate(tubeProtoype, transform.parent);
        activeTubes.Add(tube);
        tube.gameObject.SetActive(true);

        ///
        tube.SetTubeId(activeTubes.Count);

        ///
        return tube;
    }

    private void CalculateTubePosXs(int tubeCount, List<float> posXList)
    {
        ///
        posXList.Clear();

        ///
        var tubeSpacing = CalculatTubeSpacing(tubeCount);

        ///
        float tubesLength = (tubeCount - 1) * (tubeWidth + tubeSpacing);

        ///
        float firstPosX = -tubesLength / 2.0f;
        posXList.Add(firstPosX);

        ///
        for (int i = 1; i < tubeCount; i++)
        {
            posXList.Add(firstPosX + i * (tubeWidth + tubeSpacing));
        }
    }

    private void DestroyAllActiveTubes()
    {
        ///
        foreach (var item in activeTubes)
        {
            Destroy(item.gameObject);
        }

        ///
        activeTubes.Clear();
    }

    private float CalculatTubeSpacing(int tubeCount)
    {
        var totalTubeWidth = tubeWidth * tubeCount;
        var totalSpace = playfieldWidth - totalTubeWidth;
        var spaceCount = tubeCount - 1;
        var maxTubeSpacing = totalSpace / spaceCount;

        ///
        return Mathf.Min(maxTubeSpacing, tubeMaxSpacing);
    }

    private void DistributeTubesToRows(int tubeCount)
    {
        if (tubeCount <= MaxSingleRowTubeCount)
        {
            tubeCount_Under = tubeCount;
            tubeCount_Upper = 0;
            tubeCount_Third = 0;
        }
        else
        {
            tubeCount_Under = tubeCount / 2;
            tubeCount_Upper = tubeCount - tubeCount_Under;
            if (tubeCount_Upper > 7)
            {
                tubeCount_Upper = 7;
                tubeCount_Third = 1;
            }
            else
            {
                tubeCount_Third = 0;
            }
        }
    }
}
