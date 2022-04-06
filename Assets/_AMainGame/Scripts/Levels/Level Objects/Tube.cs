using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    public TubeView tubeView;
    public TubeMovement tubeMovement;

    private TubeData tubeData;

    private List<WaterData> waterData = new List<WaterData>();

    [field: System.NonSerialized]
    public int CurrentWaterHeight { get; set; }

    public int ColorCount => waterData == null ? 0 : waterData.Count;

    public float TopWaterHeight
    {
        get
        {
            if (waterData == null)
            {
                return 0;
            }
            else if (waterData.Count == 0)
            {
                return 0;
            }
            else
            {
                return waterData[waterData.Count - 1].height;
            }
        }
    }

    public int TopColorId => waterData == null ? -1 : (waterData.Count == 0 ? -1 : waterData[waterData.Count - 1].colorId);

    public int GlassHeight => TubeData.GlassHeight;

    public void SetInitialData(TubeData tubeData)
    {
        ///
        this.tubeData = tubeData;
        CopyWaterData(tubeData);

        ///
        tubeView.SetGlassHeight(TubeData.GlassHeight);
        tubeView.UpdateNewVisual();
        tubeView.SetWaterData(waterData);

        ///
        CurrentWaterHeight = tubeData.CurrentWaterHeight;
    }

    public void SetTubeId(int tubeId)
    {
        tubeView.SetSpriteMaskId(tubeId * 5);
    }

    private void CopyWaterData(TubeData tubeData)
    {
        waterData.Clear();

        ///
        if (tubeData.waterData != null)
        {
            for (int i = 0; i < tubeData.waterData.Count; i++)
            {
                waterData.Add(tubeData.waterData[i]);
            }
        }
    }
}
