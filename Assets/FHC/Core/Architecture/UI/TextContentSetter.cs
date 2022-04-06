using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace FH.Core.Architecture.UI
{
    [RequireComponent(typeof(Text))]
    [DisallowMultipleComponent]
    public abstract class TextContentSetter : ContentSetter
    {
        [SerializeField, HideInNormalInspector]
        Text text;

        protected Text Text
        {
            get
            {
                if (text == null)
                {
                    text = GetComponent<Text>();
                }
                return text;
            }
        }       

        public virtual void Reset()
        {
            text = GetComponent<Text>();
        }
    }

}