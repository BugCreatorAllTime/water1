using UnityEngine;
using System.Collections;

namespace FH.Core
{
    public interface IInspectorCommandObject
    {
        void ExcuteCommand(int intPara, string stringPara);
    }

}