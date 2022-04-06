using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeTickView : MonoBehaviour
{
    [SerializeField]
    private Tube tube;
    [SerializeField]
    private GameObject tick;

    public void OnEnable()
    {
        UpdateTick();
    }

    public void LateUpdate()
    {
        UpdateTick();
    }

    private void UpdateTick()
    {
        if (tube.ColorCount == 1 && tube.CurrentWaterHeight == TubeData.GlassHeight && tube.tubeMovement.State == TubeMovementState.Idle)
        {
            tick.gameObject.SetActive(true);
        }
        else
        {
            tick.gameObject.SetActive(false);
        }
    }
}
