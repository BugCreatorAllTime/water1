using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSkinItem : ShopItem
{
    private TubeSkin tubeSkin;

    [field: System.NonSerialized]
    public int SkinId { get; private set; }

    public override bool IsUnlocked => Entry.Instance.playerDataObject.Data.IsTubeSkinUnlocked(SkinId);

    protected override bool IsSelected => Entry.Instance.playerDataObject.Data.CurrentTubeSkinId == SkinId;

    protected override Sprite Sprite => tubeSkin.shopView;

    public void Awake()
    {
        PlayerData.OnSkinUnlocked += PlayerData_OnSkinUnlocked;
        PlayerData.OnTubeSkinChanged += PlayerData_OnTubeSkinChanged;
    }

    private void PlayerData_OnTubeSkinChanged()
    {
        UpdateView();
    }

    private void PlayerData_OnSkinUnlocked(int obj)
    {
        UpdateView();
    }

    public void SetSkinId(int id)
    {
        ///
        SkinId = id;
        if (id < nItem)
        {
            gameObject.SetActive(true);

            tubeSkin = Entry.Instance.tubeSkinManager.GetSkin(id);

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
        Entry.Instance.playerDataObject.Data.ChangeTubeSkin(SkinId);
    }

    TubeSkinManager tubeSkinManager => Entry.Instance.tubeSkinManager;
    int nItem => tubeSkinManager.nSkin;
}
