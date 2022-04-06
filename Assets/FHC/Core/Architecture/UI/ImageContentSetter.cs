using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace FH.Core.Architecture.UI
{
    [RequireComponent(typeof(Image))]
    public abstract class ImageContentSetter : ContentSetter
    {

        [SerializeField, HideInNormalInspector]
        Image image;

        protected Image Image
        {
            get
            {
                if (image == null)
                {
                    image = GetComponent<Image>();
                }
                return image;
            }
        }
        
        public virtual void Reset()
        {
            image = GetComponent<Image>();
        }
    }

}