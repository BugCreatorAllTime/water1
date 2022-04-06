using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BigNumberString
{
    const string InfiniteName = "INF";
    const char MaxSuffixChar = 'Z';
    const char MinSuffixChar = 'A';

    static List<Suffix> suffixes = new List<Suffix>()
    {
        new Suffix("K",1E+6,1E+3),
        new Suffix("M",1E+9,1E+6),
        new Suffix("B",1E+12,1E+9),
        new Suffix("T",1E+15,1E+12),
    };

    static char nextSuffix_1 = 'A';
    static char nextSuffix_2 = 'A';
    static int nextSuffixMinPower = 18;
    static int nextSuffixFactorPower = 15;

    struct Suffix
    {
        public string name;
        public double min;
        public double factor;

        public Suffix(string name, double min, double factor)
        {
            this.name = name;
            this.min = min;
            this.factor = factor;
        }
    }

    public static string ToBigNumberString(long value)
    {
        return ToBigNumberString((double)value);
    }

    public static string ToBigNumberString(float value)
    {
        return ToBigNumberString((double)value);
    }

    public static string ToBigNumberString(int value)
    {
        return ToBigNumberString((double)value);
    }

    public static string ToBigNumberString(double value)
    {
        ///
        if (value > double.MaxValue)
        {
            return InfiniteName;
        }

        ///
        if (value < suffixes[0].min)
        {
            return GetPrefixNumber(value);
        }

        ///
        int i = 1;
        while (true)
        {
            ///
            if (suffixes.Count <= i)
            {
                AddNewSuffix();
            }

            ///
            if (value < suffixes[i].min)
            {
                ///
                var lastSuffix = suffixes[i - 1];

                ///
                return string.Format("{0}{1}", GetPrefixNumber(value / lastSuffix.factor), lastSuffix.name);
            }

            ///
            i++;
        }
    }

    static void AddNewSuffix()
    {
        ///
        string suffixName = string.Concat(nextSuffix_1, nextSuffix_2);
        double min = System.Math.Pow(10, nextSuffixMinPower);
        double factor = System.Math.Pow(10, nextSuffixFactorPower);

        ///
        suffixes.Add(new Suffix(suffixName, min, factor));

        ///        
        if (nextSuffix_2 == MaxSuffixChar)
        {
            nextSuffix_2 = MinSuffixChar;
            nextSuffix_1++;
        }
        else
        {
            nextSuffix_2++;
        }
        nextSuffixMinPower += 3;
        nextSuffixFactorPower += 3;
    }

    static string GetPrefixNumber(double value)
    {
        return System.Math.Floor(value).ToString("N0");
    }
}
