using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.WritableData
{
    public static partial class WritableDataManagerProvider
    {
        static IWritableDataManager manager = null;

        public static IWritableDataManager GetManager()
        {
            if (manager == null)
            {
                manager = new WritableDataManager();
            }

            return manager;
        }
    }

}