using UnityEngine;
using System.Collections;

namespace FH.Core.HelperComponent
{
    public class DeltaPositionSourceSetter : MonoBehaviour
    {
        [SerializeField]
        DeltaPositionSync deltaPositionSync;

        [Space]
        [SerializeField]
        SourceType sourceType = SourceType.sourceAnchor;
        [SerializeField]
        float transitionTime;
        [SerializeField]
        bool setAtEnable;

        enum SourceType
        {
            sourceObject,
            sourceAnchor
        }

        public void OnEnable()
        {
            if (setAtEnable)
            {
                Set();
            }
        }

        [ContextMenu("Set")]
        public void Set()
        {
            switch (sourceType)
            {
                case SourceType.sourceObject:
                    deltaPositionSync.ChangeSources(deltaPositionSync.SourceAnchor, transform, transitionTime);
                    break;
                case SourceType.sourceAnchor:
                    deltaPositionSync.ChangeSources(transform, deltaPositionSync.SourceObject, transitionTime);
                    break;
                default:
                    break;
            }
        }
    }


}