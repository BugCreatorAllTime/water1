using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using FH.Core.Architecture.Pool;

namespace FH.Core.Gameplay.HelperComponent
{
    public abstract class GameObjectLocalResetter : MonoBehaviour, IResetableObject
    {
        [Header("Objects to apply")]
        [SerializeField]
        Transform toAddTransform;
        [SerializeField]
        List<Transform> transforms = new List<Transform>();

        Vector3[] localPositions;
        Quaternion[] localRotations;
        Vector3[] localScales;
        bool[] selfActivations;

        public abstract void ResetToLastSavedState();
        public abstract void SaveCurrentState();

        #region Position
        protected void SavePositions()
        {
            localPositions = new Vector3[transforms.Count];
            for (int i = 0; i < localPositions.Length; i++)
            {
                localPositions[i] = transforms[i].localPosition;
            }
        }

        protected void ResetPositions()
        {
            for (int i = 0; i < localPositions.Length; i++)
            {
                transforms[i].localPosition = localPositions[i];
            }
        }
        #endregion

        #region Rotation
        protected void SaveRotations()
        {
            localRotations = new Quaternion[transforms.Count];
            for (int i = 0; i < localRotations.Length; i++)
            {
                localRotations[i] = transforms[i].localRotation;
            }
        }

        protected void ResetRotations()
        {
            for (int i = 0; i < localRotations.Length; i++)
            {
                transforms[i].localRotation = localRotations[i];
            }
        }
        #endregion

        #region Scales
        protected void SaveScales()
        {
            localScales = new Vector3[transforms.Count];
            for (int i = 0; i < localScales.Length; i++)
            {
                localScales[i] = transforms[i].localScale;
            }
        }

        protected void ResetScales()
        {
            for (int i = 0; i < localScales.Length; i++)
            {
                transforms[i].localScale = localScales[i];
            }
        }
        #endregion

        #region Activations
        protected void SaveActivations()
        {
            selfActivations = new bool[transforms.Count];
            for (int i = 0; i < selfActivations.Length; i++)
            {
                selfActivations[i] = transforms[i].gameObject.activeSelf;
            }
        }

        protected void ResetActivations()
        {
            for (int i = 0; i < selfActivations.Length; i++)
            {
                transforms[i].gameObject.SetActive(selfActivations[i]);
            }
        }

        #endregion

        public void OnDrawGizmos()
        {
            if (toAddTransform != null)
            {
                AddNewTransformToList(toAddTransform);
                toAddTransform = null;
            }
        }

        void AddNewTransformToList(Transform transform)
        {
            if (transforms.Contains(transform))
            {
                return;
            }

            transforms.Add(transform);
        }

        [ContextMenu("Add all children transforms")]
        protected void AddAllChildrenTransform()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform[] childrenTransform = transform.GetChild(i).GetComponentsInChildren<Transform>(true);
                for (int j = 0; j < childrenTransform.Length; j++)
                {
                    AddNewTransformToList(childrenTransform[j]);
                }

            }
        }
    }

}