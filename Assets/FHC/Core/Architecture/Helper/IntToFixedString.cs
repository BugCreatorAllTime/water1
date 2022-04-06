using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class IntToFixedString
{
    const int MaxValue = 200;

    static string[] fixedStrings = new string[MaxValue + 1];

    static IntToFixedString()
    {
        for (int i = 0; i < MaxValue; i++)
        {
            fixedStrings[i] = i.ToString();
        }
    }

    public static string ToFixedString(this int numberUnder100)
    {
        return fixedStrings[numberUnder100];
    }
}
