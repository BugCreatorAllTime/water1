using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace FH.Core.Gameplay.HelperComponent
{
    public class TransformRandomPositionProvider : PositionProvider
    {
        [SerializeField]
        List<Transform> transforms = new List<Transform>();

        public override Vector3 Position
        {
            get
            {
                Assert.IsTrue(transforms.Count > 0);
                int randomIndex = Random.Range(0, transforms.Count);
                return transforms[randomIndex].position;
            }
        }
       
    }

}