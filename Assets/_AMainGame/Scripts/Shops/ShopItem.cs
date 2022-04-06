using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopItem : MonoBehaviour
{
    [SerializeField]
    private GameObject lockedObject;
    [SerializeField]
    private GameObject unlockedObject;
    [SerializeField]
    private Image lockedImage;
    [SerializeField]
    private Image unlockedImage;
    [SerializeField]
    private GameObject selectedObject;
    [SerializeField]
    private Button button;

    [Space]
    [SerializeField]
    private Image animatedBackground;
    [SerializeField]
    private Color flashColor;
    [SerializeField]
    private float shortFlashDuration;
    [SerializeField]
    private float longFlashDuration;
    [SerializeField]
    private float longFlashInterval;
    [SerializeField]
    private float longFlashIterations = 3;

    private Color normalAnimatedBackgroudnColor;

    public abstract bool IsUnlocked { get; }
    protected abstract bool IsSelected { get; }
    protected abstract Sprite Sprite { get; }

    protected abstract void SelectThisItem();

    protected void UpdateView()
    {
        ///
        if (IsUnlocked)
        {
            lockedObject.SetActive(false);
            unlockedObject.SetActive(true);
            unlockedImage.sprite = Sprite;
            selectedObject.SetActive(IsSelected);
            button.interactable = true;
        }
        else
        {
            lockedObject.SetActive(true);
            unlockedObject.SetActive(false);
            lockedImage.sprite = Sprite;
            button.interactable = false;
        }
    }

    public void Start()
    {
        normalAnimatedBackgroudnColor = animatedBackground.color;
    }

    public void Select()
    {
        SelectThisItem();
        UpdateView();
    }

    public IEnumerator DoShortFlash()
    {
        yield return StartCoroutine(DoSingleFlash(shortFlashDuration));
    }

    public IEnumerator DoLongFlash()
    {
        for (int i = 0; i < longFlashIterations; i++)
        {
            yield return StartCoroutine(DoSingleFlash(longFlashDuration));
            yield return new WaitForSeconds(longFlashInterval);
        }
    }

    private IEnumerator DoSingleFlash(float duration)
    {
        ///
        animatedBackground.color = flashColor;

        ///
        yield return new WaitForSeconds(duration);

        ///
        animatedBackground.color = normalAnimatedBackgroudnColor;
    }
}
