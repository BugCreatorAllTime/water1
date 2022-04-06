using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace FH.Core.HelperComponent
{
    [RequireComponent(typeof(Toggle))]
    [ExecuteInEditMode]
    public class ToggleSpriteSwaper : MonoBehaviour
    {
        [SerializeField]
        Image targetGraphic;
        [SerializeField]
        Sprite onSprite;
        [SerializeField]
        Sprite offSprite;

        Toggle toggle;

        void Awake()
        {
            toggle = GetComponent<Toggle>();
        }

        void Update()
        {
            if (targetGraphic == null)
            {
                return;
            }

            targetGraphic.sprite = toggle.isOn ? onSprite : offSprite;
        }

        void Reset()
        {
            targetGraphic = GetComponent<Toggle>().targetGraphic as Image;
        }
    }

}