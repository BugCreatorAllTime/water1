using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GD
{
    public class MultiGraphicButton : Button
    {
        [Header("MultiGraphicToggle")]
        [SerializeField]
        List<Item> additionalTargetGraphics = new List<Item>();

        [System.Serializable]
        public struct Item
        {
            public Graphic graphic;
            public bool useCustomColors;
            public ColorBlock customColors;
        }


        public List<Item> AdditionalTargetGraphics
        {
            get
            {
                ///
                if (additionalTargetGraphics == null)
                {
                    additionalTargetGraphics = new List<Item>();
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
            var commonTargetColor = GetTargetColor(state, colors);
            StartColorTween(targetGraphic, commonTargetColor, instant);

            ///
            for (int i = 0; i < AdditionalTargetGraphics.Count; i++)
            {
                var item = AdditionalTargetGraphics[i];
                var targetColor = GetTargetColor(state, item);
                StartColorTween(item.graphic, targetColor, instant);
            }
        }

        Color GetTargetColor(SelectionState state, Item item)
        {
            ///
            if (!item.useCustomColors)
            {
                return GetTargetColor(state, colors);
            }

            ///
            return GetTargetColor(state, item.customColors);
        }

        Color GetTargetColor(SelectionState state, ColorBlock colors)
        {
            ///
            Color targetColor;

            switch (state)
            {
                case SelectionState.Normal:
                    targetColor = colors.normalColor;
                    break;
                case SelectionState.Selected:
                    targetColor = colors.selectedColor;
                    break;
                case SelectionState.Highlighted:
                    targetColor = colors.highlightedColor;
                    break;
                case SelectionState.Pressed:
                    targetColor = colors.pressedColor;
                    break;
                case SelectionState.Disabled:
                    targetColor = colors.disabledColor;
                    break;
                default:
                    targetColor = Color.black;
                    break;
            }

            ///
            return targetColor;
        }

        void StartColorTween(Graphic graphic, Color targetColor, bool instant)
        {
            if (graphic == null)
                return;

            graphic.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
        }

        public void DoStateTransitionInstant()
        {
            DoStateTransition(currentSelectionState, true);
        }
    }

}