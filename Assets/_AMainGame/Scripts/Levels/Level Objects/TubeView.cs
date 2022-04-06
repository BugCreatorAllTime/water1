using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeView : MonoBehaviour
{
    public static float MaxWorldGlassHeight { get; set; }

    [SerializeField]
    private Tube tube;

    [Space]
    [SerializeField]
    private Transform glassRoot;

    [SerializeField]
    private Transform bottomSprite;
    [SerializeField]
    private Transform midSprite;
    [SerializeField]
    private Transform topSprite;

    public Transform normalTopLeft;
    public Transform normalTopRight;
    public Transform normalBottomRight;
    public Transform normalBottomLeft;

    [Space]
    [SerializeField]
    private float bottomWorldHeight;
    [SerializeField]
    private float midNormalWorldHeight;
    [SerializeField]
    private float topWorldHeight;
    [SerializeField]
    private float worldHeightPerGlassHeight = 0.5f;

    [Header("Water")]
    [SerializeField]
    private WaterWave waterWave;
    [SerializeField]
    private Transform waterSquare;
    [SerializeField]
    private List<SpriteRenderer> waterSegmentSquares;

    [Header("New visual")]
    [SerializeField]
    private Transform newVisualRoot;
    [SerializeField]
    private SpriteRenderer newVisualView;
    [SerializeField]
    private SpriteMask newVisualMask;

    [Space]
    [SerializeField]
    private ParticleSystem pouringParticleSystem;

    private List<WaterData> waterData;

    private float glassWorldHeight;
    private float totalWaterHeight;

    private int glassHeight;

    public float WaterHeight { get => totalWaterHeight; }

    [field: System.NonSerialized]
    public bool IsWaterSpilling { get; private set; } = false;

    public int TopWaterColorId
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
            else
            {
                return waterData[waterData.Count - 1].colorId;
            }
        }
    }

    public void Start()
    {
        PlayerData.OnTubeSkinChanged += PlayerData_OnTubeSkinChanged;
    }

    public void OnDestroy()
    {
        PlayerData.OnTubeSkinChanged -= PlayerData_OnTubeSkinChanged;
    }

    private void PlayerData_OnTubeSkinChanged()
    {
        UpdateNewVisual();
    }

    public void SetWaterData(List<WaterData> waterData)
    {
        ///
        this.waterData = waterData;
        UpdateTotalWaterHeight();

        ///
        UpdatePouringParticleColor();
    }

    public void SetSpriteMaskId(int spriteMaskId)
    {
        ///
        SetSpriteMaskRange(topSprite.gameObject, spriteMaskId + 1, spriteMaskId - 1);
        SetSpriteMaskRange(midSprite.gameObject, spriteMaskId + 1, spriteMaskId - 1);
        SetSpriteMaskRange(bottomSprite.gameObject, spriteMaskId + 1, spriteMaskId - 1);
        SetSpriteMaskRange(newVisualMask.gameObject, spriteMaskId + 1, spriteMaskId - 1);

        ///
        waterSquare.GetComponent<Renderer>().sortingOrder = spriteMaskId;
        waterWave.SetSortingOrder(spriteMaskId);
        foreach (var item in waterSegmentSquares)
        {
            item.sortingOrder = spriteMaskId;
        }
    }

    private void SetSpriteMaskRange(GameObject go, int frontSortingOrder, int backSortingOrder)
    {
        var spriteMask = go.GetComponent<SpriteMask>();
        spriteMask.frontSortingOrder = frontSortingOrder;
        spriteMask.backSortingOrder = backSortingOrder;
    }

    public void UpdateNewVisual()
    {
        SetGlassHeight(glassHeight);
    }

    public void SetGlassHeight(int glassHeight)
    {
        ///
        this.glassHeight = glassHeight;

        ///
        var tubeSkin = Entry.Instance.tubeSkinManager.GetCurrentSkin();
        newVisualView.sprite = tube.tubeMovement.IsCompleted ? tubeSkin.fullView : tubeSkin.normalView;
        newVisualMask.sprite = tubeSkin.mask;

        ///
        var newVisualEffectiveHeight = tubeSkin.height * newVisualRoot.localScale.y;
        var newVisualEffectiveWidth = tubeSkin.width * newVisualRoot.localScale.x;

        ///
        worldHeightPerGlassHeight = (newVisualEffectiveHeight - topWorldHeight) / glassHeight;

        // Mid
        var midScale = midSprite.localScale;
        var midWorldHeight = glassHeight * worldHeightPerGlassHeight - bottomWorldHeight;
        midScale.y = midWorldHeight / midNormalWorldHeight;
        midSprite.localScale = midScale;

        ///
        glassWorldHeight = topWorldHeight + midWorldHeight + bottomWorldHeight;

        // Top
        var topP = topSprite.localPosition;
        topP.y = midWorldHeight / 2.0f;
        topSprite.localPosition = topP;

        // Bottom
        var bottomP = bottomSprite.localPosition;
        bottomP.y = -midWorldHeight / 2.0f;
        bottomSprite.localPosition = bottomP;

        // Normal Top left
        var normalTopLeftP = normalTopLeft.localPosition;
        normalTopLeftP.y = glassWorldHeight / 2.0f;
        normalTopLeftP.x = -newVisualEffectiveWidth / 2.0f;
        normalTopLeft.localPosition = normalTopLeftP;

        // Normal Top right
        var normalTopRightP = normalTopRight.localPosition;
        normalTopRightP.y = normalTopLeftP.y;
        normalTopRightP.x = newVisualEffectiveWidth / 2.0f;
        normalTopRight.localPosition = normalTopRightP;

        // Normal Bottom left and right
        var normalBottomLeftP = normalTopLeft.localPosition;
        var normalBottomRightP = normalTopRight.localPosition;
        normalBottomLeftP.y = normalBottomRightP.y = -normalTopLeftP.y;
        normalBottomLeft.localPosition = normalBottomLeftP;
        normalBottomRight.localPosition = normalBottomRightP;

        ///
        if (glassWorldHeight > MaxWorldGlassHeight)
        {
            MaxWorldGlassHeight = glassWorldHeight;
        }

        ///
        var glassRootP = glassRoot.localPosition;
        glassRootP.y = glassWorldHeight / 2.0f;
        glassRoot.localPosition = glassRootP;

        ///
        var p = newVisualRoot.localPosition;
        p.y = normalBottomLeft.localPosition.y;
        newVisualRoot.localPosition = p;
    }

    public void LateUpdate()
    {
        UpdateWater();
    }

    private float GetWaterNormalWorlHeight(float waterHeight)
    {
        ///
        return waterHeight * worldHeightPerGlassHeight;
    }

    public void UpdateWater()
    {
        ///
        float waterNormalWorldHeight = GetWaterNormalWorlHeight(totalWaterHeight);
        float waterNormalWorldRatio = waterNormalWorldHeight / glassWorldHeight;

        ///
        float minX;
        float maxX;
        float minY;
        float maxY;

        ///
        GetCurrentBoundary(out minX, out maxX, out minY, out maxY, waterSquare.transform.parent);

        ///
        float waterWorldHeight = (maxY - minY) * waterNormalWorldRatio;
        float waterWorldWidth = maxX - minX;
        float waterWorldX = (maxX + minX) / 2.0f;
        float waterWorldY = minY + waterWorldHeight / 2.0f;

        ///
        waterSquare.localScale = new Vector3(waterWorldWidth, waterWorldHeight, 1);
        waterSquare.localPosition = new Vector3(waterWorldX, waterWorldY, 0);

        ///
        UpdateIsWaterSpilling();

        // Update water segments
        if (waterData != null)
        {
            float segmentMinY = minY;
            for (int i = 0; i < waterSegmentSquares.Count; i++)
            {
                ///
                var segmentSquare = waterSegmentSquares[i];

                ///
                if (i >= waterData.Count)
                {
                    segmentSquare.gameObject.SetActive(false);
                    continue;
                }
                else
                {
                    segmentSquare.gameObject.SetActive(true);
                }

                ///
                var segmentWater = waterData[i];

                ///
                var segmentWorldHeight = Mathf.Approximately(totalWaterHeight, 0) ? 0 : segmentWater.height / totalWaterHeight * waterWorldHeight;

                float segmentWorldY = segmentMinY + segmentWorldHeight / 2.0f;

                ///
                segmentSquare.transform.localScale = new Vector3(waterWorldWidth, segmentWorldHeight, 1);
                segmentSquare.transform.localPosition = new Vector3(waterWorldX, segmentWorldY, 0);

                ///
                segmentMinY += segmentWorldHeight;

                ///
                segmentSquare.color = Entry.Instance.colorManager.GetColor(segmentWater.colorId);
            }
        }

        ///
        waterWave.SetColor(Entry.Instance.colorManager.GetColor(TopWaterColorId));
        waterWave.SetPosY(minY + waterWorldHeight);
        waterWave.SetWidth(waterWorldWidth);
    }

    private void UpdateIsWaterSpilling()
    {
        ///
        var waterY = waterSquare.position.y;
        var glassY = Mathf.Min(normalTopLeft.position.y, normalTopRight.position.y);

        ///
        IsWaterSpilling = waterY >= glassY;
    }

    private void GetCurrentBoundary(out float minX, out float maxX, out float minY, out float maxY, Transform transformParent)
    {
        ///
        var root = transformParent.position;

        ///
        minX = -root.x + Mathf.Min(normalBottomLeft.position.x, normalBottomRight.position.x, normalTopLeft.position.x, normalTopRight.position.x);
        maxX = -root.x + Mathf.Max(normalBottomLeft.position.x, normalBottomRight.position.x, normalTopLeft.position.x, normalTopRight.position.x);
        minY = -root.y + Mathf.Min(normalBottomLeft.position.y, normalBottomRight.position.y, normalTopLeft.position.y, normalTopRight.position.y);
        maxY = -root.y + Mathf.Max(normalBottomLeft.position.y, normalBottomRight.position.y, normalTopLeft.position.y, normalTopRight.position.y);
    }

    private void UpdateTotalWaterHeight()
    {
        ///
        totalWaterHeight = 0;

        ///
        if (waterData != null)
        {
            foreach (var item in waterData)
            {
                totalWaterHeight += item.height;
            }
        }
    }

    public void AddWater(int colorId, float amount)
    {
        ///
        if (TopWaterColorId != colorId)
        {
            waterData.Add(new WaterData() { colorId = colorId, height = amount });
        }
        else
        {
            var index = waterData.Count - 1;
            var water = waterData[index];
            water.height += amount;
            waterData[index] = water;
        }

        ///
        totalWaterHeight += amount;
    }

    public void RemoveWater(float amount)
    {
        ///
        if (waterData.Count > 0)
        {
            ///
            var index = waterData.Count - 1;
            var water = waterData[index];
            water.height = Mathf.MoveTowards(water.height, 0, amount);
            waterData[index] = water;

            ///
            totalWaterHeight = Mathf.MoveTowards(totalWaterHeight, 0, amount);
        }
    }

    public void RoundLastWaterSegment()
    {
        ///
        if (waterData.Count > 0)
        {
            var index = waterData.Count - 1;
            var water = waterData[index];
            water.height = Mathf.RoundToInt(water.height);

            ///
            if (water.height > 0)
            {
                waterData[index] = water;
            }
            else
            {
                waterData.RemoveAt(index);
            }

            ///
            UpdatePouringParticleColor();
        }
    }

    private void UpdatePouringParticleColor()
    {
        ///
        if (waterData.Count > 0)
        {
            var index = waterData.Count - 1;
            var water = waterData[index];
            var color = Entry.Instance.colorManager.GetColor(water.colorId);

            ///
            var setting = pouringParticleSystem.main;
            setting.startColor = color;
        }
    }
}
