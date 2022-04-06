using System.Collections.Generic;
using UnityEngine;
using GD;
using EasyExcelGenerated;
using System;
using Random = UnityEngine.Random;

[System.Serializable]
public partial class LevelEntry
{
    private const int MaxIterations = 5000;

    public List<TubeData> tubes;
    public List<int> additionalTubeIndexes;
    public int levelId;

    public LevelEntry(int levelId, int colorCount)
    {
        ///
        this.levelId = levelId;

        ///
        while (!GenerateData(colorCount))
        {

        }
    }

    private bool GenerateData(int colorCount)
    {
        ///
        //var data = Resources.Load<LevelDefitions_WaterDefition_Sheet>("EasyExcelGeneratedAsset/LevelDefitions_WaterDefition_Sheet");

        /////
        //var effLevelId = Mathf.Clamp(levelId, 0, data.GetDataCount() - 1);

        /////
        //var levelData = data.GetData(effLevelId) as WaterDefition;

        ///
        // int colorCount = levelData.colorCount;

        ///
        int additionalTubeCount = colorCount <= 2 ? 1 : 2;/* levelData.tubeCount - levelData.colorCount;*/

        ///
        int tubeCount = colorCount + additionalTubeCount;

        ///
        tubes = new List<TubeData>();
        for (int i = 0; i < tubeCount; i++)
        {
            ///
            var tubeData = new TubeData()
            {
                waterData = new List<WaterData>(),
                MaxInitialHeight = int.MaxValue,
            };

            ///
            tubes.Add(tubeData);
        }

        ///
        int additionWaterHeight = additionalTubeCount == 1 ? 2 : Random.Range(0, colorCount <= 2 ? 0 : 0);

        ///
        var allColors = new List<int>();
        for (int i = 0; i < GameConst.ColorCount; i++)
        {
            allColors.Add(i);
        }

        ///
        List<WaterData> colorHeights = new List<WaterData>();
        for (int i = 0; i < colorCount; i++)
        {
            colorHeights.Add(new WaterData() { colorId = allColors.TakeRandomItem(), height = TubeData.GlassHeight });
        }

        ///
        List<int> tubeIndexes = new List<int>();
        for (int i = 0; i < tubeCount; i++)
        {
            tubeIndexes.Add(i);
        }

        ///
        additionalTubeIndexes = new List<int>();
        foreach (var tubeIndex in tubeIndexes.GetRandomItems(additionalTubeCount))
        {
            additionalTubeIndexes.Add(tubeIndex);
        }

        ///
        foreach (var tubeId in additionalTubeIndexes)
        {
            int height = Random.Range(0, additionWaterHeight + 1);
            var tube = tubes[tubeId];
            tube.MaxInitialHeight = height;
            tubes[tubeId] = tube;
            additionWaterHeight -= height;
        }

        ///
        var totalHeight = colorCount * TubeData.GlassHeight;

        // Predefined colors       
        int threeColorCount = colorCount < 5 ? Random.Range(1, 3) : Random.Range(0, 5);
        threeColorCount = Mathf.Clamp(threeColorCount, 0, colorCount);
        int twoColorCount = colorCount < 5 ? (2 - threeColorCount) : Random.Range(0, 3);
        twoColorCount = Mathf.Clamp(twoColorCount, 0, colorCount - 1);
        int oneColorCount = colorCount < 5 ? -1 : totalHeight - threeColorCount * 3 - twoColorCount * 2;

        ///
        if ((oneColorCount) < 0 && (colorCount >= 5))
        {
            return false;
        }

        // 3 colors
        for (int i = 0; i < threeColorCount; i++)
        {
            ///
            var colorData = colorHeights[i];
            colorData.height -= 3;
            colorHeights[i] = colorData;

            ///
            totalHeight -= 3;

            ///
            bool found = false;
            for (int tubeId = 0; tubeId < tubes.Count; tubeId++)
            {
                if (tubes[tubeId].TryInsertWater(colorData.colorId, 3))
                {
                    found = true;
                    break;
                }
            }
            //if (!found)
            //{
            //    return false;
            //}
        }

        // 2 colors
        for (int i = 0; i < twoColorCount; i++)
        {
            ///
            var colorData = colorHeights[colorHeights.Count - i - 1];
            colorData.height -= 2;
            colorHeights[colorHeights.Count - i - 1] = colorData;

            ///
            totalHeight -= 2;

            ///
            bool found = false;
            for (int tubeId = 0; tubeId < tubes.Count; tubeId++)
            {
                if (tubes[tubeId].TryInsertWater(colorData.colorId, 2))
                {
                    found = true;
                    break;
                }
            }
            //if (!found)
            //{
            //    return false;
            //}
        }

        int oneColorUnplaced = oneColorCount;

        ///
        int colorIndex = -1;
        for (int i = 0; i < totalHeight; i++)
        {
            ///
            if (oneColorUnplaced < 0)
            {
                colorIndex = Random.Range(0, colorHeights.Count);
            }
            else
            {
                colorIndex++;
                if (colorIndex >= colorHeights.Count)
                {
                    colorIndex = 0;
                }
            }
            var waterData = colorHeights[colorIndex];

            ///
            int iterations = 0;
            while (true)
            {
                iterations++;

                ///
                var randomTubeIndex = tubeIndexes.GetRandomItem();
                var tube = tubes[randomTubeIndex];

                ///
                if (oneColorUnplaced > 0 && tube.TopColorId == waterData.colorId)
                {
                    ///
                    if (iterations > MaxIterations)
                    {
                        return false;
                    }

                    ///
                    continue;
                }

                ///
                if (tube.TryInsertOneHeightOfWater(waterData.colorId))
                {
                    ///
                    waterData.height--;

                    ///
                    if (tube.CurrentWaterHeight == TubeData.GlassHeight || tube.CurrentWaterHeight == tube.MaxInitialHeight)
                    {
                        tubeIndexes.Remove(randomTubeIndex);
                    }

                    ///
                    break;
                }
                else
                {
                    ///
                    if (tube.CurrentWaterHeight == tube.MaxInitialHeight)
                    {
                        tubeIndexes.Remove(randomTubeIndex);
                    }

                    ///
                    if (tubeIndexes.Count == 1)
                    {
                        return false;
                    }

                    ///
                    if (iterations > MaxIterations)
                    {
                        return false;
                    }
                }
            }

            ///
            if (waterData.height > 0)
            {
                colorHeights[colorIndex] = waterData;
            }
            else
            {
                colorHeights.RemoveAt(colorIndex);
            }

            ///
            oneColorUnplaced--;
        }

        ///
        if (!IsTotalWaterValid(colorCount))
        {
            return false;
        }

        ///
        return true;
    }

    private bool IsTotalWaterValid(int colorCount)
    {
        ///
        Dictionary<int, int> heightByColors = new Dictionary<int, int>();

        ///
        int righHeight = colorCount * TubeData.GlassHeight;
        int height = 0;

        ///
        foreach (var tube in tubes)
        {
            ///
            var waters = tube.waterData;
            foreach (var water in waters)
            {
                ///
                height += (int)water.height;

                ///
                if (height > righHeight)
                {
                    return false;
                }

                ///
                if (heightByColors.ContainsKey(water.colorId))
                {
                    heightByColors[water.colorId] += (int)water.height;
                }
                else
                {
                    heightByColors[water.colorId] = (int)water.height;
                }

                ///
                if (heightByColors[water.colorId] > TubeData.GlassHeight)
                {
                    return false;
                }
            }


        }

        ///
        foreach (var colorHeight in heightByColors.Values)
        {
            if (colorHeight > TubeData.GlassHeight)
            {
                return false;
            }
        }

        ///
        return height == righHeight;
    }
}
