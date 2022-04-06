using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainCameraVibrator : MonoBehaviour
{
    [SerializeField]
    private UnityEvent vibrateDelegate;
    
    [ContextMenu("Vibrate")]
    public void Vibrate()
    {
        vibrateDelegate?.Invoke();
    }
}
