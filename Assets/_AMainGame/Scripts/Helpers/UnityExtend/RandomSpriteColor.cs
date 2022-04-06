using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD;
using FH.Core;

public class RandomSpriteColor : MonoBehaviour, IInspectorCommandObject
{
    [SerializeField]
    private List<SpriteRenderer> spriteRenderers;

    [Space]
    [SerializeField]
    List<Color> colors;

    [Space]
    [SerializeField, InspectorCommand]
    int _commandApply;

    public Color GetCurrentColor()
    {
        return spriteRenderers[0].color;
    }

    public void OnEnable()
    {
        ApplyRandomColor();
    }

    public void ApplyColor(Color color)
    {
        foreach (var item in spriteRenderers)
        {
            item.color = color;
        }
    }

    public void ApplyRandomColor()
    {
        var color = colors.GetRandomItem();
        ApplyColor(color);
    }

    public void ExcuteCommand(int intPara, string stringPara)
    {
        ApplyRandomColor();
    }

#if UNITY_EDITOR
    public void Reset()
    {
        ///
        colors = new List<Color>();
        spriteRenderers = new List<SpriteRenderer>();
        var r = GetComponent<SpriteRenderer>();
        if (r != null)
        {
            spriteRenderers.Add(r);
            colors.Add(r.color);
        }
    }
#endif
}
