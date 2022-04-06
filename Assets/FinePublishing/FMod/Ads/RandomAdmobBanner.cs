using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class RandomAdmobBanner : AdmobBannerBase
    {
        [SerializeField]
        List<WeightedMString> adUnitIds = new List<WeightedMString>();

#if USE_STANDALONE_ADMOB
        //protected override string GetBannerUnitId()
        //{
        //    var mstring = WeightedMString.GetFromList(adUnitIds).value;
        //    mstring.TrimAllValues();
        //    return mstring;
        //} 
#endif
    }

}