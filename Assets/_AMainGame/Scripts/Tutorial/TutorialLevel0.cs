using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel0 : MonoBehaviour
{
    [SerializeField]
    private GameObject stage1;
    [SerializeField]
    private GameObject stage2;

    public void Start()
    {
        ///
        stage1.SetActive(true);
        stage2.SetActive(false);

        ///
        TubeMovement.OnAnyTubeStartBecomeActive += TubeMovement_OnAnyTubeStartBecomeActive;
    }

    private void TubeMovement_OnAnyTubeStartGiving()
    {
        ///
        TubeMovement.OnAnyTubeStartGiving -= TubeMovement_OnAnyTubeStartGiving;

        ///
        gameObject.SetActive(false);
    }

    private void TubeMovement_OnAnyTubeStartBecomeActive()
    {
        ///
        TubeMovement.OnAnyTubeStartBecomeActive -= TubeMovement_OnAnyTubeStartBecomeActive;
        TubeMovement.OnAnyTubeStartGiving += TubeMovement_OnAnyTubeStartGiving;

        ///
        stage1.SetActive(false);
        stage2.SetActive(true);
    }
}
