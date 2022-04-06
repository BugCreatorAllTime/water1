using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Gameplay.HelperComponent
{
    public class SelfActivationResetter : GameObjectLocalResetter
    {
        public override void ResetToLastSavedState()
        {
            ResetActivations();
        }

        public override void SaveCurrentState()
        {
            SaveActivations();
        }
    }

}