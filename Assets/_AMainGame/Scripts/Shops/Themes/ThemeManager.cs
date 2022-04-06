using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    [SerializeField]
    private List<Theme> themes;

    public Theme GetTheme(int id)
    {
        return themes[id];
    }

    public Theme GetCurrentTheme()
    {
        int themeId = Entry.Instance.playerDataObject.Data.CurrentThemeId;
        return GetTheme(themeId);
    }

    public int nThemes => themes.Count;
}
