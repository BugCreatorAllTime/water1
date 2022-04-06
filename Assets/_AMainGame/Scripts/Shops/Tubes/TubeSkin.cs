using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TubeSkin
{
    public Sprite normalView;
    public Sprite mask;
    public Sprite fullView;
    public Sprite shopView;

    [Space]
    public float width;
    public float height;
}
