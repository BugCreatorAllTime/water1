using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InfiniteFloatList
{
    [SerializeField]
    private float maxValue;
    [SerializeField]
    private float increment;
    [SerializeField]
    private List<float> list;

    public float this[int index]
    {
        get
        {
            ///
            if (list.Count == 0)
            {
                throw new System.Exception("InfiniteFloatList - predefined list can't be empty");
            }

            ///
            if (index < list.Count)
            {
                return list[index];
            }
            else
            {
                var lastValue = list[list.Count - 1];
                var value = lastValue + (index - list.Count + 1) * increment;
                return value;
            }
        }
    }
}
