using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Gameplay.HelperComponent
{
    public class QuadStretcherLeftPositionProvider : PositionProvider
    {
        public override Vector3 Position
        {
            get
            {
                return transform.position + new Vector3(-(transform.lossyScale.x / 2.0f), 0, 0);
            }
        }
    }

}