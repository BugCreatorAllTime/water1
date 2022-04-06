using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Gameplay.HelperComponent
{
    public class LocalPositionResetter : GameObjectLocalResetter
    {
        public override void ResetToLastSavedState()
        {
            ResetPositions();
        }

        public override void SaveCurrentState()
        {
            SavePositions();
        }
    }

}