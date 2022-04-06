using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.Helper
{
    public interface IIdsService
    {
        void Initialize(int[] ids);
        void FreeId(int Id);
        int GetNewId();
    }

}