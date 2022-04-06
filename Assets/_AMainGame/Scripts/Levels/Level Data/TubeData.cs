using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TubeData
{
    public const int GlassHeight = 4;

    public List<WaterData> waterData;

    public int TopColorId
    {
        get
        {
            if (waterData == null)
            {
                return -1;
            }

            if (waterData.Count == 0)
            {
                return -1;
            }

            return waterData[waterData.Count - 1].colorId;
        }
    }

    public int CurrentWaterHeight
    {
        get
        {
            int h = 0;

            ///
            if (waterData != null)
            {
                foreach (var item in waterData)
                {
                    h += (int)item.height;
                }
            }

            ///
            return h;
        }
    }

    public int MaxInitialHeight { get; set; }

    public bool TryInsertOneHeightOfWater(int colorId)
    {
        return TryInsertWater(colorId, 1);
    }

    public bool TryInsertWater(int colorId, int height)
    {
        ///
        int currentWaterHeight = CurrentWaterHeight;

        ///
        if ((currentWaterHeight + height) > GlassHeight)
        {
            return false;
        }

        ///
        if ((currentWaterHeight + height) > MaxInitialHeight)
        {
            return false;
        }

        ///
        if (waterData.Count == 0)
        {
            waterData.Add(new WaterData() { colorId = colorId, height = height });
        }
        else
        {
            var topWaterData = waterData[waterData.Count - 1];
            if (topWaterData.colorId == colorId)
            {
                ///
                if (waterData.Count == 1 && ((currentWaterHeight + height) == GlassHeight))
                {
                    return false;
                }

                ///
                topWaterData.height += height;
                waterData[waterData.Count - 1] = topWaterData;
            }
            else
            {
                waterData.Add(new WaterData() { colorId = colorId, height = height });
            }
        }

        ///
        return true;
    }
}

[System.Serializable]
public struct WaterData
{
    public int colorId;
    public float height;
}