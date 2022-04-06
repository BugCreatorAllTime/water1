using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Core.Gameplay.HelperComponent
{
    public class PositionFollower : MonoBehaviour
    {
        [SerializeField]
        PositionProvider positionProvider;
        [SerializeField]
        Vector3 offset;
        [SerializeField]
        int maxIterations = 5;

        int iterations;

        public void OnEnable()
        {
            iterations = 0;
        }

        public void LateUpdate()
        {
            ///
            if (maxIterations > 0 && iterations >= maxIterations)
            {
                return;
            }

            ///
            transform.position = positionProvider.Position + offset;

            ///
            iterations++;
        }
    }
}