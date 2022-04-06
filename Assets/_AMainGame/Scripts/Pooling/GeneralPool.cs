using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FH.Core.Architecture.Pool;

public class GeneralPool : MultiPrototypesPool<GeneralPoolMember>
{
    public void TryPushPrototype(GeneralPoolMember prototype)
    {
        if (!ContainsPrototype(prototype.PrototypeId))
        {
            PushPrototype(prototype);
        }
    }

}