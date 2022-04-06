using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.Helper
{
    public static class NumberReducer
    {
        const float OneMega = 1000000.0f;
        const float OneKilo = 1000.0f;

        public static string ToMegaFormat(float value)
        {
            if (value >= OneMega)
            {
                return string.Format("{0:00}M", value / OneMega);
            }
            else
            {
                return value.ToString();
            }
        }

        public static string ToKiloFormat(float value)
        {
            if (value >= OneKilo)
            {
                return string.Format("{0:00}K", value / OneKilo);
            }
            else
            {
                return value.ToString();
            }
        }
    }

}