using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ValueDisplayerUnified<T> : MonoBehaviour where T : System.IComparable
{
    [SerializeField]
    UnifiedText unifiedText;

    T lastValue;

    protected abstract T GetCurrentValue();

    protected virtual string GetString(T value)
    {
        return value.ToString();
    }

    protected virtual bool IsDifferentValue(T oldValue, T newValue)
    {
        return oldValue.CompareTo(newValue) != 0;
    }

    public void OnEnable()
    {
        lastValue = GetCurrentValue();
        Display(lastValue);
    }

    public virtual void Update()
    {
        var currentValue = GetCurrentValue();
        if (IsDifferentValue(currentValue, lastValue))
        {
            lastValue = currentValue;
            Display(currentValue);
        }
    }

    protected virtual void Display(T currentValue)
    {
        unifiedText.Text = GetString(currentValue);
    }

    public virtual void Reset()
    {
        unifiedText = GetComponent<UnifiedText>();
    }
}