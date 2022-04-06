using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockThemeButton : UnlockButton
{
    [SerializeField]
    private ThemeGrid themeGrid;

    protected override void Unlock()
    {
        themeGrid.StartUnlockingNewTheme();
    }
}
