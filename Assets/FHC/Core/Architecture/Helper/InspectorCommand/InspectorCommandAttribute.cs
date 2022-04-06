using UnityEngine;
using System.Collections;

namespace FH.Core
{
    public class InspectorCommandAttribute : PropertyAttribute
    {
        public int IntPara { get; set; }
        public string StringPara { get; set; }

    }

}