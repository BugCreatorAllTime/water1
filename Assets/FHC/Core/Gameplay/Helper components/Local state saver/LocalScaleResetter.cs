using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Gameplay.HelperComponent
{
    public class LocalScaleResetter : GameObjectLocalResetter
    {
        public override void ResetToLastSavedState()
        {
            ResetScales();
        }

        public override void SaveCurrentState()
        {
            SaveScales();
        }
    }

}