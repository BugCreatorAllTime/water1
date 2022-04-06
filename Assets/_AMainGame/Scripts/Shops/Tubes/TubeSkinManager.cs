using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSkinManager : MonoBehaviour
{
    [SerializeField]
    private List<TubeSkin> tubeSkins;

    public TubeSkin GetSkin(int id)
    {
        return tubeSkins[id];
    }

    public TubeSkin GetCurrentSkin()
    {
        var skindId = Entry.Instance.playerDataObject.Data.CurrentTubeSkinId;
        return GetSkin(skindId);
    }

    public int nSkin => tubeSkins.Count;
}
