using GD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RandomColorImage : MonoBehaviour
{
    [SerializeField]
    private List<Color> colors;

    public void OnEnable()
    {
        TryChangeColor();
    }

    public void TryChangeColor()
    {
        GetComponent<Image>().color = colors.GetRandomItem();
    }
}
