using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

namespace FH.Core.Architecture.Pool
{
    public class MultiPrototypesPoolMemeberMonoBehavior<T> : MonoBehaviour, IMultiPrototypesPoolMember<T> where T : class
    {
        bool inPool = false;
        IMultiPrototypesPool<T> pool;
        int prototypeId;
        bool prototypeIdOverridden = false;

        #region IPoolMemberBase<IGreenPower>
        public bool InPool
        {
            get
            {
                return inPool;
            }

            set
            {
                inPool = value;
            }
        }
        #endregion

        #region IMultiPrototypesPoolMember<IGreenPower>
        public IMultiPrototypesPool<T> Pool
        {
            get
            {
                return pool;
            }

            set
            {
                pool = value;
            }
        }

        public int PrototypeId
        {
            get
            {
                if (prototypeIdOverridden)
                {
                    return prototypeId;
                }
                else
                {
                    return GetInstanceID();
                }
            }

            set
            {
                prototypeIdOverridden = true;
                prototypeId = value;
            }
        }
        #endregion

        #region ICloneable<T>
        public T Clone()
        {
            MultiPrototypesPoolMemeberMonoBehavior<T> clone = Instantiate(this);
            clone.PrototypeId = PrototypeId;
            clone.OnClone();
            Assert.IsNotNull(clone as T);
            return clone as T;
        }
        #endregion
                      
        protected virtual void OnClone() { }
    }
}
