using UnityEngine;
using System.Collections;

namespace FH.Core.HelperComponent
{
    public class TimeScaleSetter : MonoBehaviour
    {
        public void Set(float value)
        {
            Time.timeScale = value;
        }
    }

}