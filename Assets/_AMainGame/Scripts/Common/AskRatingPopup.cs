using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AskRatingPopup : MonoBehaviour
{
    [SerializeField]
    private Button rateButton;

    [Space]
    [SerializeField]
    private int minStarValueToRate = 4;

    [Space]
    [SerializeField]
    private Color selectedStarColor = Color.yellow;
    [SerializeField]
    private List<Image> starImages;

    [Space]
    [SerializeField]
    private UnityEvent onShouldRate;
    [SerializeField]
    private UnityEvent onShouldCloseOnly;

    private int starValue;

    public void OnEnable()
    {
        SetStarValue(0);
    }

    public void SetStarValue(int value)
    {
        ///
        rateButton.interactable = value > 0;

        ///
        for (int i = 0; i < starImages.Count; i++)
        {
            if (i < value)
            {
                starImages[i].color = selectedStarColor;
            }
            else
            {
                starImages[i].color = Color.white;
            }
        }

        ///
        starValue = value;
    }

    public void OnClickRate()
    {
        ///
        if (starValue >= minStarValueToRate)
        {
            onShouldRate?.Invoke();
        }
        else
        {
            onShouldCloseOnly?.Invoke();
        }
    }
}
