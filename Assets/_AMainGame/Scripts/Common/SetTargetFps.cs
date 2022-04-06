using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class SetTargetFps : MonoBehaviour
    {
        [SerializeField]
        int targetFps = 60;

        public void Awake()
        {
            Application.targetFrameRate = targetFps;
        }
    }

}