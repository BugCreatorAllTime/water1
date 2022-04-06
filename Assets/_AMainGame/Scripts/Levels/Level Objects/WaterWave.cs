using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWave : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float maxHeightScale = 0.6f;
    [SerializeField]
    private float speedY = 1.1f;
    [SerializeField]
    private float speedX = 1;

    [Space]
    [SerializeField]
    private bool isVisible = false;

    private float loopWidth;
    private float minWidth;

    public bool IsVisible
    {
        get => isVisible;
        set => isVisible = value;
    }

    public bool IsReversedX { get; set; }

    public void SetSortingOrder(int order)
    {
        spriteRenderer.sortingOrder = order;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void SetPosY(float y)
    {
        var p = transform.localPosition;
        p.y = y;
        transform.localPosition = p;
    }

    public void SetWidth(float width)
    {
        //var s = transform.localScale;
        //s.x = width;
        //transform.localScale = s;

        //var s = spriteRenderer.size;
        //s.x = width;
        //spriteRenderer.size = s;

        minWidth = width;
    }

    public void LateUpdate()
    {
        UpdateScaleY();
        UpdateWidth();
        
    }

    private void UpdateScaleY()
    {
        var s = transform.localScale;
        if (isVisible)
        {
            s.y = Mathf.MoveTowards(s.y, maxHeightScale, speedY * Time.deltaTime);
        }
        else
        {
            s.y = Mathf.MoveTowards(s.y, 0, speedY * Time.deltaTime);
        }
        transform.localScale = s;
    }

    private void UpdateWidth()
    {
        ///
        loopWidth += IsReversedX ? -Time.deltaTime * speedX : Time.deltaTime * speedX;
        loopWidth = Mathf.Repeat(loopWidth, 2);

        ///
        float width = loopWidth;
        while (width < minWidth)
        {
            width += 2;
        }

        ///
        var s = spriteRenderer.size;
        s.x = width;
        spriteRenderer.size = s;
    }
}
