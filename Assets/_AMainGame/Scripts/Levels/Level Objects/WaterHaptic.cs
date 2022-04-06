using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHaptic : MonoBehaviour
{
    [SerializeField]
    private NiceVibrationTriggerer niceVibrationTriggerer;
    [SerializeField]
    private float interval = 0.2f;

    private float lastTimeHaptic = -99;

    public static bool WaterTransferredThisFrame { get; set; } = false;

    public void LateUpdate()
    {
        ///
        if (WaterTransferredThisFrame)
        {
            ///
            if ((Time.time - lastTimeHaptic) >= interval)
            {
                niceVibrationTriggerer.Haptic();
                lastTimeHaptic = Time.time;
            }

            ///
            WaterTransferredThisFrame = false;
        }
    }
}
