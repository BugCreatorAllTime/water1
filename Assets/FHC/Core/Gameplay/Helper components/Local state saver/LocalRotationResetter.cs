using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Gameplay.HelperComponent
{
    public class LocalRotationResetter : GameObjectLocalResetter
    {
        public override void ResetToLastSavedState()
        {
            ResetRotations();
        }

        public override void SaveCurrentState()
        {
            SaveRotations();
        }
    }

}