using GD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeGrid : MonoBehaviour
{
    [SerializeField]
    private ThemeItem firstThemeItem;
    [SerializeField]
    private List<Transform> otherThemePlaceholders;

    [Space]
    [SerializeField]
    private int flashRadomItemCount = 5;
    [SerializeField]
    private GameObject raycastBlockerObject;

    private List<ThemeItem> themeItems;

    private List<ThemeItem> lockedThemes = new List<ThemeItem>();

    public void Awake()
    {
        ///
        CreateThemeItems();

        ///
        SetDataForItems();
    }

    private void CreateThemeItems()
    {
        ///
        themeItems = new List<ThemeItem>();

        ///
        themeItems.Add(firstThemeItem);

        ///
        for (int i = 0; i < otherThemePlaceholders.Count; i++)
        {
            var themeItem = Instantiate(firstThemeItem, otherThemePlaceholders[i]);
            themeItems.Add(themeItem);
        }
    }

    private void SetDataForItems()
    {
        for (int i = 0; i < themeItems.Count; i++)
        {
            themeItems[i].SetThemeId(i);
        }
    }

    public void StartUnlockingNewTheme()
    {
        StartCoroutine(UnlockNewTheme());
    }

    private IEnumerator UnlockNewTheme()
    {
        ///
        UpdateLockedThemesList();

        ///
        if (lockedThemes.Count == 0)
        {
            yield break;
        }

        ///
        raycastBlockerObject.SetActive(true);

        ///
        if (lockedThemes.Count > 1)
        {
            yield return StartCoroutine(FlashRadomItems());
        }

        ///
        var pickedItem = lockedThemes.GetRandomItem();

        ///
        yield return StartCoroutine(pickedItem.DoLongFlash());

        ///
        Entry.Instance.playerDataObject.Data.UnlockTheme(pickedItem.ThemeId);
        Entry.Instance.playerDataObject.Data.ChangeTheme(pickedItem.ThemeId);

        ///
        raycastBlockerObject.SetActive(false);
    }

    private IEnumerator FlashRadomItems()
    {
        ///
        ThemeItem lastItem = null;
        for (int i = 0; i < flashRadomItemCount; i++)
        {
            ///
            var currentItem = lockedThemes.GetRandomItem();
            while (currentItem == lastItem)
            {
                currentItem = lockedThemes.GetRandomItem();
            }

            ///
            yield return StartCoroutine(currentItem.DoShortFlash());

            ///
            lastItem = currentItem;
        }
    }

    private void UpdateLockedThemesList()
    {
        ///
        lockedThemes.Clear();

        ///
        foreach (var item in themeItems)
        {
            if (!item.IsUnlocked && item.gameObject.activeInHierarchy)
            {
                lockedThemes.Add(item);
            }
        }
    }
}
