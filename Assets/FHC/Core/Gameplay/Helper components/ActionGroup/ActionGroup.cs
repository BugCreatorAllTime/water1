using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace FH.Core.Gameplay.HelperComponent
{
    [System.Serializable]
    public class ActionGroup
    {
        [SerializeField]
        string name;
        [SerializeField]
        UnityEvent actions = new UnityEvent();

        #region Public properties
        public string Name
        {
            get
            {
                return name;
            }
        }

        public UnityEvent Actions
        {
            get
            {
                return actions;
            }
        }
        #endregion
    }

}