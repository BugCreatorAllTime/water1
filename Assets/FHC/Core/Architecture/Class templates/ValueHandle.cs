using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueHandle<T>
{
    public delegate void ValueChangeHandler(T oldValue, T newValue);

    public event ValueChangeHandler OnValueChanged;

    T value;

    public T Value
    {
        get
        {
            return value;
        }

        set
        {
            var oldValue = this.value;
            this.value = value;
            OnValueChanged?.Invoke(oldValue, value);
        }
    }

}
