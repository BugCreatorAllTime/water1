using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

namespace FH.Core.HelperComponent
{
    [ExecuteInEditMode]
    public class DeltaPositionSync : MonoBehaviour
    {
        [Header("Source")]
        [SerializeField]
        Transform sourceAnchor;
        [SerializeField]
        Transform sourceObject;

        [Header("Target")]
        [SerializeField]
        Transform targetAnchor;
        [SerializeField]
        Transform targetObject;

        [Header("Option")]
        [SerializeField]
        bool ignoreZ = true;

        Transform oldSourceAnchor;
        Transform oldSourceObject;

        float transitionProgress = 1;
        Vector3 oldTargetPosition;

        public Transform SourceAnchor
        {
            get
            {
                return sourceAnchor;
            }
        }

        public Transform SourceObject
        {
            get
            {
                return sourceObject;
            }
        }

        public void LateUpdate()
        {
            var p = SourceObject.position - SourceAnchor.position + targetAnchor.position;          
            if (transitionProgress < 1)
            {
                oldTargetPosition = oldSourceObject.position - oldSourceAnchor.position + targetAnchor.position;                
                p = Vector3.Lerp(oldTargetPosition, p, transitionProgress);
            }
            if (ignoreZ)
            {
                p.z = targetObject.position.z;
            }
            targetObject.position = p;
        }

        public void ChangeSources(Transform newSourceAnchor, Transform newTargetObject, float transitionTime = 0)
        {
            oldSourceAnchor = sourceAnchor;
            oldSourceObject = sourceObject;

            sourceAnchor = newSourceAnchor;
            sourceObject = newTargetObject;

            if (transitionTime > 0)
            {
                StartCoroutine(TransitToNewSources(transitionTime));
            }
            else
            {
                transitionProgress = 1;
            }
        }

        IEnumerator TransitToNewSources(float transitionDuration)
        {
            float currentTime = 0;
            while (currentTime < transitionDuration)
            {
                currentTime = Mathf.MoveTowards(currentTime, transitionDuration, Time.deltaTime);
                transitionProgress = currentTime / transitionDuration;                
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }

        public void OnValidate()
        {
            Assert.IsNotNull(SourceAnchor);
            Assert.IsNotNull(SourceObject);
            Assert.IsNotNull(targetAnchor);
            Assert.IsNotNull(targetObject);
        }
    }

}