using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LargeNumberString
{
    const double pK = 1.0E+3f; // k
    const double pM = 1.0E+6f; // m
    const double pB = 1.0E+9f; // B
    const double pT = 1.0E+12f; // t
    const double pQ = 1.0E+15f; // q
    const double pQ2 = 1.0E+18f; // Q
    const double pS = 1.0E+21f; // s
    const double pS2 = 1.0E+24f; // S
    const double pO = 1.0E+27f; // o
    const double pN = 1.0E+30f; // n
    const double pD = 1.0E+33f; // d
    const double INF = 1.0E+36f;

    public static string ToLargeNumberString(int value)
    {
        return ToLargeNumberString((double)value);
    }
    public static string ToLargeNumberString(double value)
    {
        ///
        if (value < 0)
        {
            return "-" + System.Math.Abs(value).ToLargeNumberString();
        }

        ///
        if (value < pK)
        {
            return GetPrefixNumber(value);
        }

        // k
        if (value < pM)
        {
            return string.Format("{0}k", GetPrefixNumber(value / pK));
        }

        // m
        if (value < pB)
        {
            return string.Format("{0}m", GetPrefixNumber(value / pM));
        }

        // B
        if (value < pT)
        {
            return string.Format("{0}b", GetPrefixNumber(value / pB));
        }

        // t
        if (value < pQ)
        {
            return string.Format("{0}t", GetPrefixNumber(value / pT));
        }

        // q
        if (value < pQ2)
        {
            return string.Format("{0}q", GetPrefixNumber(value / pQ));
        }

        // Q
        if (value < pS)
        {
            return string.Format("{0}Q", GetPrefixNumber(value / pQ2));
        }

        // s
        if (value < pS2)
        {
            return string.Format("{0}s", GetPrefixNumber(value / pS));
        }

        // S
        if (value < pO)
        {
            return string.Format("{0}S", GetPrefixNumber(value / pS2));
        }

        // o
        if (value < pN)
        {
            return string.Format("{0}o", GetPrefixNumber(value / pO));
        }

        // n
        if (value < pD)
        {
            return string.Format("{0}n", GetPrefixNumber(value / pN));
        }

        // d
        if (value < INF)
        {
            return string.Format("{0}d", GetPrefixNumber(value / pD));
        }

        ///
        return "INF";
    }

    static string GetPrefixNumber(double value)
    {
        int intPart = (int)System.Math.Floor(value);
        int floatPart = (int)System.Math.Floor((value - intPart) * 100);
        float partEnding = floatPart % 10;

        if (floatPart == 0)
        {
            return intPart.ToString();
        }
        else
        {
            if (partEnding == 0)
            {
                return string.Format("{0}.{1}", intPart, floatPart / 10);
            }
            else if (floatPart >= 10)
            {
                return string.Format("{0}.{1}", intPart, floatPart);
            }
            else
            {
                return string.Format("{0}.0{1}", intPart, floatPart);
            }
        }
    }
}
