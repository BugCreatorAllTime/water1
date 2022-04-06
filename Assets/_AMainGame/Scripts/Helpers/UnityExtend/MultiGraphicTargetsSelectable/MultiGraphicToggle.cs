using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GD
{
    public class MultiGraphicToggle : Toggle, IMultiGraphicTransition
    {
        [Header("MultiGraphicToggle")]
        [SerializeField]
        List<Graphic> additionalTargetGraphics = new List<Graphic>();

        public List<Graphic> AdditionalTargetGraphics
        {
            get
            {
                ///
                if (additionalTargetGraphics == null)
                {
                    additionalTargetGraphics = new List<Graphic>();
                }

                ///
                return additionalTargetGraphics;
            }
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            ///
            if (transition != Transition.ColorTint)
            {
                base.DoStateTransition(state, instant);
            }

            ///
            Color tintColor;

            switch (state)
            {
                case SelectionState.Normal:
                    tintColor = colors.normalColor;
                    break;
                case SelectionState.Selected:
                    tintColor = colors.selectedColor;
                    break;
                case SelectionState.Highlighted:
                    tintColor = colors.highlightedColor;
                    break;
                case SelectionState.Pressed:
                    tintColor = colors.pressedColor;
                    break;
                case SelectionState.Disabled:
                    tintColor = colors.disabledColor;
                    break;
                default:
                    tintColor = Color.black;
                    break;
            }

            StartColorTween(tintColor * colors.colorMultiplier, instant);

        }

        void StartColorTween(Color targetColor, bool instant)
        {
            StartColorTween(targetGraphic, targetColor, instant);
            for (int i = 0; i < AdditionalTargetGraphics.Count; i++)
            {
                StartColorTween(AdditionalTargetGraphics[i], targetColor, instant);
            }
        }

        void StartColorTween(Graphic graphic, Color targetColor, bool instant)
        {
            if (graphic == null)
                return;

            graphic.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
        }
    }

}