using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeItem : ShopItem
{
    private Theme theme;

    [field: System.NonSerialized]
    public int ThemeId { get; private set; }

    public override bool IsUnlocked => Entry.Instance.playerDataObject.Data.IsThemeUnlocked(ThemeId);

    protected override bool IsSelected => Entry.Instance.playerDataObject.Data.CurrentThemeId == ThemeId;

    protected override Sprite Sprite => theme.sprite;

    public void Awake()
    {
        PlayerData.OnThemeUnlocked += PlayerData_OnThemeUnlocked;
        PlayerData.OnThemeChanged += PlayerData_OnThemeChanged;
    }

    private void PlayerData_OnThemeChanged()
    {
        UpdateView();
    }

    private void PlayerData_OnThemeUnlocked(int obj)
    {
        UpdateView();
    }

    public void SetThemeId(int id)
    {
        ///
        ThemeId = id;
        if (id < nThemes)
        {
            gameObject.SetActive(true);
            theme = themeManager.GetTheme(id);

            ///
            UpdateView();
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    protected override void SelectThisItem()
    {
        Entry.Instance.playerDataObject.Data.ChangeTheme(ThemeId);
    }

    int nThemes => themeManager.nThemes;
    ThemeManager themeManager => Entry.Instance.themeManager;
}
