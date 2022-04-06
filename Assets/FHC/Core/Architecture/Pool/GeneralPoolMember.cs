using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.Pool
{
    public class GeneralPoolMember : MultiPrototypesPoolMemeberMonoBehavior<GeneralPoolMember>
    {
        public virtual void TryReturnToPool()
        {
            if (Pool != null)
            {
                Pool.PushInstance(this);
            }
        }

        public void TryReturnToPoolAndDeactivate()
        {
            if (Pool != null && !InPool)
            {
                Pool.PushInstance(this);
                gameObject.SetActive(false);
            }
        }
    }

}