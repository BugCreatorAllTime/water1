using UnityEngine;

namespace FH.Core.Gameplay.HelperComponent
{
    public class ConstantPositionProvider : PositionProvider
    {
        [SerializeField]
        Vector3 position;

        #region PositionProvider
        public override Vector3 Position
        {
            get
            {
                return position;
            }
        }
        #endregion
    }

}