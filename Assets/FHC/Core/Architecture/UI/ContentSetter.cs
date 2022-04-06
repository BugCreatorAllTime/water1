using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture.UI
{
    public abstract class ContentSetter : MonoBehaviour, IUIRefresher
    {
        [Header("ContentSetter")]
        [SerializeField]
        protected bool setContentAtUpdate = false;
        [SerializeField]
        bool setContentAtAwake = true;
        [SerializeField]
        bool setContentAtEnable = false;

        bool initialized = false;

        #region IUIRefresher
        [ContextMenu("Refresh")]
        public void Refresh()
        {
            if (!initialized)
            {
                Initialize();
            }
            SetContent();
        }
        #endregion

        #region MonoB
        public void Awake()
        {
            Initialize();
            if (setContentAtAwake)
            {
                SetContent();
            }
        }

        public void OnEnable()
        {
            if (setContentAtEnable)
            {
                SetContent();
            }
        }

        public void Update()
        {
            if (setContentAtUpdate)
            {
                SetContent();
            }
        }
        #endregion

        void Initialize()
        {
            OnInitialize();
            initialized = true;
        }

        protected abstract void SetContent();

        protected virtual void OnInitialize()
        {

        }
    }

}