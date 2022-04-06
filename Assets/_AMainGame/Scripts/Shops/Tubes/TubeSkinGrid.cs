using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD;
using DG.Tweening.Core.Easing;

public class TubeSkinGrid : MonoBehaviour
{
    [SerializeField]
    private TubeSkinItem firstTubeSkinItem;
    [SerializeField]
    private List<Transform> otherTubeSkinPlaceholders;

    [Space]
    [SerializeField]
    private int flashRadomItemCount = 5;
    [SerializeField]
    private GameObject raycastBlockerObject;

    private List<TubeSkinItem> tubeSkinItems;

    private List<TubeSkinItem> lockedSkins = new List<TubeSkinItem>();

    public void Awake()
    {
        ///
        CreateTubeSkinItems();

        ///
        SetDataForItems();
    }

    private void CreateTubeSkinItems()
    {
        ///
        tubeSkinItems = new List<TubeSkinItem>();

        ///
        tubeSkinItems.Add(firstTubeSkinItem);

        ///
        for (int i = 0; i < otherTubeSkinPlaceholders.Count; i++)
        {
            var tubeSkinItem = Instantiate(firstTubeSkinItem, otherTubeSkinPlaceholders[i]);
            tubeSkinItems.Add(tubeSkinItem);
        }
    }

    private void SetDataForItems()
    {
        for (int i = 0; i < tubeSkinItems.Count; i++)
        {
            tubeSkinItems[i].SetSkinId(i);
        }
    }

    public void StartUnlockingNewSkin()
    {
        StartCoroutine(UnlockNewSkin());
    }

    private IEnumerator UnlockNewSkin()
    {
        ///
        UpdateLockedSkinsList();

        ///
        if (lockedSkins.Count == 0)
        {
            yield break;
        }

        ///
        raycastBlockerObject.SetActive(true);

        ///
        if (lockedSkins.Count > 1)
        {
            yield return StartCoroutine(FlashRadomItems());
        }

        ///
        var pickedItem = lockedSkins.GetRandomItem();
        
        ///
        yield return StartCoroutine(pickedItem.DoLongFlash());

        ///
        Entry.Instance.playerDataObject.Data.UnlockTubeSkin(pickedItem.SkinId);
        Entry.Instance.playerDataObject.Data.ChangeTubeSkin(pickedItem.SkinId);

        ///
        raycastBlockerObject.SetActive(false);
    }

    private IEnumerator FlashRadomItems()
    {
        ///
        TubeSkinItem lastItem = null;
        for (int i = 0; i < flashRadomItemCount; i++)
        {
            ///
            var currentItem = lockedSkins.GetRandomItem();
            while (currentItem == lastItem)
            {
                currentItem = lockedSkins.GetRandomItem();
            }

            ///
            yield return StartCoroutine(currentItem.DoShortFlash());

            ///
            lastItem = currentItem;
        }
    }

    private void UpdateLockedSkinsList()
    {
        ///
        lockedSkins.Clear();

        ///
        foreach (var item in tubeSkinItems)
        {
            if (!item.IsUnlocked && item.gameObject.activeInHierarchy)
            {
                lockedSkins.Add(item);
            }
        }
    }
}
