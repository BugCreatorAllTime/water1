using UnityEngine;
using System.Collections;

namespace FH.Core.Gameplay.HelperComponent
{
    [System.Serializable]
    public class Vector3AnimationCurve
    {
        [SerializeField]
        AnimationCurve x = AnimationCurve.Linear(0, 0, 1, 0);
        [SerializeField]
        AnimationCurve y = AnimationCurve.Linear(0, 0, 1, 0);
        [SerializeField]
        AnimationCurve z = AnimationCurve.Linear(0, 0, 1, 0);

        public Vector3 Evaluate(float time)
        {
            return new Vector3(x.Evaluate(time), y.Evaluate(time), z.Evaluate(time));
        }

        public static Vector3AnimationCurve Linear(float timeStart, Vector3 valueStart, float timeEnd, Vector3 valueEnd)
        {
            return new Vector3AnimationCurve()
            {
                x = AnimationCurve.Linear(timeStart, valueStart.x, timeEnd, valueEnd.x),
                y = AnimationCurve.Linear(timeStart, valueStart.y, timeEnd, valueEnd.y),
                z = AnimationCurve.Linear(timeStart, valueStart.z, timeEnd, valueEnd.z),
            };

        }
    }

}