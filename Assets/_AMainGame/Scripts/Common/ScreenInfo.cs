using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInfo : MonoBehaviour
{
    public event System.Action OnCalculated;

    [SerializeField]
    private float playFieldMaxY;
    [SerializeField]
    private float playFieldMinY;

    [Header("Tap area")]
    [SerializeField]
    private float tapAreaMinY;
    [SerializeField]
    private float tapAreaMaxY;

    [Header("[LEGACY] Standard Fire Point")]
    [SerializeField]
    private float standardFirePointY;
    [SerializeField]
    private float standardFirePointMaxWidth;
    [SerializeField]
    private float standardFirePointMinHorizontalPadding;
    [SerializeField]
    private float standardFirePointOffsetY;

    public float WorldWidth { get; private set; }
    public float WorldHeight { get; private set; }
    public float WorldHalfWidth { get; private set; }
    public float WorldHalfHeight { get; private set; }

    public bool IsCalculated { get; private set; } = false;
    public float PlayFieldMaxY { get => playFieldMaxY; }
    public float PlayFieldMinY { get => playFieldMinY; }

    public float StandardFirePointY { get => standardFirePointY; }
    public float StandardFirePointWidth { get; private set; }
    public float StandardFirePointOffsetY { get => standardFirePointOffsetY; }

    public Vector2 PlayfieldCenter
    {
        get
        {
            var y = (playFieldMinY + playFieldMaxY) / 2.0f;
            return new Vector2(0, y);
        }
    }

    public Rect PlayfieldRect { get; private set; }

    public void Awake()
    {
        ///
        Calculate();
        OnCalculated?.Invoke();
    }

    public void DoAfterCalculation(System.Action action)
    {
        if (IsCalculated)
        {
            action?.Invoke();
        }
        else
        {
            OnCalculated += action;
        }
    }

    public Vector2 GetRandomPointInsidePlayfield(float xPadding, float yPadding)
    {
        ///
        var minX = -WorldHalfWidth + xPadding;
        var maxX = WorldHalfWidth - xPadding;
        var minY = playFieldMinY + yPadding;
        var maxY = PlayFieldMaxY - yPadding;

        ///
        var x = Random.Range(minX, maxX);
        var y = Random.Range(minY, maxY);

        ///
        return new Vector2(x, y);
    }

    /// <summary>
    /// Normalized vector from a point to playfield's center
    /// </summary>
    /// <returns></returns>
    public Vector2 GetInwardVector(Vector2 startPoint)
    {
        return (PlayfieldCenter - startPoint).normalized;
    }

    public Vector3 ClampToTapArea(Vector3 pos)
    {
        pos.y = Mathf.Clamp(pos.y, tapAreaMinY, tapAreaMaxY);
        pos.x = Mathf.Clamp(pos.x, -WorldHalfWidth, WorldHalfWidth);
        return pos;
    }

    private void Calculate()
    {
        CalculateScreen();
        CalculateStandardFirePoint();
        CalculatePlayfieldRect();

        ///
        IsCalculated = true;
    }

    private void CalculatePlayfieldRect()
    {
        var pos = new Vector2(-WorldHalfWidth, PlayFieldMinY);
        var size = new Vector2(WorldWidth, PlayFieldMaxY - PlayFieldMinY);
        PlayfieldRect = new Rect(pos, size);
    }

    private void CalculateScreen()
    {
        ///
        var camera = Camera.main;

        ///
        WorldHeight = camera.orthographicSize * 2;
        WorldWidth = camera.aspect * WorldHeight;

        ///
        WorldHalfHeight = camera.orthographicSize;
        WorldHalfWidth = camera.aspect * WorldHalfHeight;
    }

    private void CalculateStandardFirePoint()
    {
        var paddedScreenWidth = WorldWidth - standardFirePointMinHorizontalPadding * 2;
        StandardFirePointWidth = Mathf.Min(paddedScreenWidth, standardFirePointMaxWidth);
    }
}
