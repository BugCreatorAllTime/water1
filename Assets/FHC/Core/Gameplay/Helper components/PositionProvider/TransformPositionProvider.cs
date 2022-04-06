using UnityEngine;
using FH.Core.Architecture;

namespace FH.Core.Gameplay.HelperComponent
{
    public class TransformPositionProvider : PositionProvider
    {
        [SerializeField]
        Transform targetTransform;
        [SerializeField]
        bool useLocal;
        [SerializeField]
        Vector3Modifier modifier = new Vector3Modifier();

        public override Vector3 Position
        {
            get
            {
                Vector3 position = useLocal ? targetTransform.localPosition : targetTransform.position;
                return modifier.GetModified(position);
            }
        }
    }

}