using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace FH.Core.Architecture.Helper
{
    public class IdsService : IIdsService
    {
        List<int> freeIds = new List<int>();
        List<int> ids = new List<int>();

        int currentMaxId = -1;

        void IIdsService.FreeId(int Id)
        {
            ids.Remove(Id);
            freeIds.Add(Id);
        }

        int IIdsService.GetNewId()
        {
            if (freeIds.Count > 0)
            {
                int id = freeIds[freeIds.Count - 1];
                freeIds.RemoveAt(freeIds.Count - 1);
                ids.Add(id);
                return id;
            }

            currentMaxId++;
            ids.Add(currentMaxId);
            return currentMaxId;
        }

        void IIdsService.Initialize(int[] initialIds)
        {
            freeIds = new List<int>();
            ids = new List<int>();
            for (int i = 0; i < initialIds.Length; i++)
            {
                int id = initialIds[i];
                ids.Add(id);
                if (id > currentMaxId)
                {
                    currentMaxId = id;
                }
            }
        }
    }

}