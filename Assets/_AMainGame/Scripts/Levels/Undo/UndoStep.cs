using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UndoStep
{
    public Tube givedTube;
    public Tube receivedTube;
    public int amount;
}
