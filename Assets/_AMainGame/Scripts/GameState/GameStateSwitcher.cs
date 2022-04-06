using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSwitcher : MonoBehaviour
{
    public void SwitchToPrepare()
    {
        Entry.Instance.gameStateManager.SwitchToPrepare();
    }

    public void SwitchToBeat()
    {
        Entry.Instance.gameStateManager.SwitchToBeat();
    }
}
