using FH.Core.Architecture.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPoolMemberSpawner : MonoBehaviour
{
    [SerializeField]
    GeneralPoolMember prototype;
    [SerializeField]
    Transform spawningPosition;
    [SerializeField]
    Transform transformParent;

    [Space]
    [SerializeField]
    CustomGeneralPool customGeneralPool;

    bool inited = false;

    GeneralPool Pool
    {
        get
        {
            if (customGeneralPool != null)
            {
                return customGeneralPool.GeneralPool;
            }
            else
            {
                return Entry.Instance.GeneralPool;
            }
        }
    }

    public void Awake()
    {
        TryInit();
    }

    private void TryInit()
    {
        ///
        if (inited)
        {
            return;
        }

        ///
        if (!Pool.ContainsPrototype(prototype.PrototypeId))
        {
            Pool.PushPrototype(prototype);
        }

        ///
        inited = true;
    }

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        SpawnWithResult();
    }

    public GeneralPoolMember SpawnWithResult()
    {
        ///
        TryInit();

        ///
        var instance = Pool.TakeInstance(prototype.PrototypeId, true);
        instance.transform.parent = (transformParent == null) ? null : transformParent;
        if (spawningPosition != null)
        {
            instance.transform.position = spawningPosition.position;
        }

        ///
        instance.gameObject.SetActive(true);

        ///
        return instance;
    }
}