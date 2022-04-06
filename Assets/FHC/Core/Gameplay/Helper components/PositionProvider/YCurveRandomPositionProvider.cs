using UnityEngine;

namespace FH.Core.Gameplay.HelperComponent
{
    public class YCurveRandomPositionProvider : PositionProvider
    {
        [SerializeField]
        PositionProvider originalPositionProvider;
        [SerializeField]
        float leftDistance;
        [SerializeField]
        float rightDistance;
        [SerializeField]
        AnimationCurve yCurve;

        public override Vector3 Position
        {
            get
            {
                Vector3 originalPosition = originalPositionProvider.Position;
                float originalX = originalPositionProvider.Position.x;
                float minX = originalX - leftDistance;
                float maxX = originalX + rightDistance;
                float x = Random.Range(minX, maxX);
                float y = yCurve.Evaluate((x - minX) / (leftDistance + rightDistance)) + originalPosition.y;
                return new Vector3(x, y, originalPosition.z);
            }
        }


    }

}