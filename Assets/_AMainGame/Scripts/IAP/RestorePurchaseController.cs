using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestorePurchaseController : MonoBehaviour
{
    [SerializeField] Button restorePurchaseButton;

    private void Start()
    {
        restorePurchaseButton.onClick.AddListener(DoRestorePurchase);
    }

    public void DoRestorePurchase()
    {
        FMod.Purchaser.Instance.RestorePurchases();
    }

    private void OnEnable()
    {
#if UNITY_IOS
        restorePurchaseButton.interactable = true;

#else
        restorePurchaseButton.interactable = false;
#endif
    }
}
