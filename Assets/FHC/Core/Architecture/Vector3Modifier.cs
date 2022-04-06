using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture
{
    [System.Serializable]
    public class Vector3Modifier
    {
        [SerializeField]
        Vector3 offsetVector = Vector3.zero;
        [SerializeField]
        Vector3 overrideVector = Vector3.zero;
        [SerializeField]
        bool overrideX = false;
        [SerializeField]
        bool overrideY = false;
        [SerializeField]
        bool overrideZ = false;

        public Vector3 OffsetVector
        {
            get
            {
                return offsetVector;
            }

            set
            {
                offsetVector = value;
            }
        }

        public Vector3 OverrideVector
        {
            get
            {
                return overrideVector;
            }

            set
            {
                overrideVector = value;
            }
        }

        public bool OverrideX
        {
            get
            {
                return overrideX;
            }

            set
            {
                overrideX = value;
            }
        }

        public bool OverrideY
        {
            get
            {
                return overrideY;
            }

            set
            {
                overrideY = value;
            }
        }

        public bool OverrideZ
        {
            get
            {
                return overrideZ;
            }

            set
            {
                overrideZ = value;
            }
        }

        public Vector3 GetModified(Vector3 originalVector3)
        {
            if (OverrideX)
            {
                originalVector3.x = OverrideVector.x;
            }
            if (OverrideY)
            {
                originalVector3.y = OverrideVector.y;
            }
            if (OverrideZ)
            {
                originalVector3.z = OverrideVector.z;
            }

            return originalVector3 + OffsetVector;
        }

        public static Vector3Modifier Unchange
        {
            get
            {
                return new Vector3Modifier()
                {
                    OffsetVector = Vector3.zero,
                    OverrideVector = Vector3.zero,
                    OverrideX = false,
                    OverrideY = false,
                    OverrideZ = false
                };
            }
        }
    }

}