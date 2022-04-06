using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockTubeButton : UnlockButton
{
    [SerializeField]
    private TubeSkinGrid tubeSkinGrid;

    protected override void Unlock()
    {
        tubeSkinGrid.StartUnlockingNewSkin();
    }
}
