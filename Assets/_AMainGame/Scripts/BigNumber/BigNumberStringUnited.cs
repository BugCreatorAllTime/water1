using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BigNumberStringUnited
{
    public static string ToStringWithComma(this int value)
    {
        return string.Format("{0:n0}", value);
    }

    public static string ToLargeNumberString(this float value)
    {
        var rs = BigNumberString.ToBigNumberString(System.Math.Abs(value));
        return value < 0 ? "-" + rs : rs;
    }

    public static string ToLargeNumberString(this long value)
    {
        var rs = BigNumberString.ToBigNumberString(System.Math.Abs(value));
        return value < 0 ? "-" + rs : rs;
    }

    public static string ToLargeNumberString(this int value)
    {
        var rs = BigNumberString.ToBigNumberString(System.Math.Abs(value));
        return value < 0 ? "-" + rs : rs;
    }

    public static string ToLargeNumberString(this double value)
    {
        var rs = BigNumberString.ToBigNumberString(System.Math.Abs(value));
        return value < 0 ? "-" + rs : rs;
    }
}
