using System.Collections.Specialized;
using System.Text;
using UnityEngine;

public static class QueryUtility
{
    public static void ParseQueryString(this string query, Encoding encoding, NameValueCollection result)
    {
        if (query.Length == 0)
            return;

        var decodedLength = query.Length;
        var namePos = 0;
        var first = true;

        while (namePos <= decodedLength)
        {
            int valuePos = -1, valueEnd = -1;
            for (var q = namePos; q < decodedLength; q++)
            {
                if ((valuePos == -1) && (query[q] == '='))
                {
                    valuePos = q + 1;
                }
                else if (query[q] == '&')
                {
                    valueEnd = q;
                    break;
                }
            }

            if (first)
            {
                first = false;
                if (query[namePos] == '?')
                    namePos++;
            }

            string name;
            if (valuePos == -1)
            {
                name = null;
                valuePos = namePos;
            }
            else
            {
                name = WWW.UnEscapeURL(query.Substring(namePos, valuePos - namePos - 1), encoding);
            }
            if (valueEnd < 0)
            {
                namePos = -1;
                valueEnd = query.Length;
            }
            else
            {
                namePos = valueEnd + 1;
            }
            var value = WWW.UnEscapeURL(query.Substring(valuePos, valueEnd - valuePos), encoding);

            result.Add(name, value);
            if (namePos == -1)
                break;
        }
    }
}