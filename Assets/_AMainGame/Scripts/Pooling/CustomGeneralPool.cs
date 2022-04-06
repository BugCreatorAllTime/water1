using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGeneralPool : MonoBehaviour
{
    GeneralPool generalPool;

    public GeneralPool GeneralPool
    {
        get
        {
            ///
            if (generalPool == null)
            {
                generalPool = new GeneralPool();
            }

            ///
            return generalPool;
        }
    }
}
