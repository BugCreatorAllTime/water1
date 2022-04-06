using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FMod
{
    public class PromotionTexts : MonoBehaviour
    {
        [SerializeField]
        List<Text> textsList = new List<Text>();
        [SerializeField]
        float durationEach = 2.0f;
        [SerializeField]
        float transitionDuration = 0.2f;
        Text currentText = null;

        Text newText;

        List<Text> secondTextsList = new List<Text>();

        public void Awake()
        {
            HideAllTexts();
        }

        public void OnEnable()
        {
            StartCoroutine(RunPromotion());
        }

        public void OnDisable()
        {
            StopAllCoroutines();
            try
            {
                Stablize();
            }
            catch (Exception e)
            {

            }
        }

        void HideAllTexts()
        {
            foreach (var item in textsList)
            {
                item.gameObject.SetActive(false);
            }
        }

        IEnumerator SwitchText(Text oldText, Text newText)
        {
            ///
            oldText.gameObject.SetActive(true);
            newText.gameObject.SetActive(true);

            ///
            float t = 0;
            while (t < transitionDuration)
            {
                t = Mathf.MoveTowards(t, transitionDuration, Time.deltaTime);
                SetTextOpacity(oldText, 1 - (t / transitionDuration));
                SetTextOpacity(newText, t / transitionDuration);

                yield return null;
            }

            ///
            oldText.gameObject.SetActive(false);
        }

        IEnumerator RunPromotion()
        {
            if (currentText == null)
            {
                currentText = GetRandomText();
                textsList.Remove(currentText);
                secondTextsList.Add(currentText);
            }

            if (newText == null)
            {
                newText = GetRandomText();
            }

            while (true)
            {
                currentText.gameObject.SetActive(true);
                newText.gameObject.SetActive(true);
                SetTextOpacity(currentText, 1);
                SetTextOpacity(newText, 0);

                yield return new WaitForSeconds(durationEach);               

                yield return SwitchText(currentText, newText);

                //textsList.Add(currentText);
                textsList.Remove(newText);
                secondTextsList.Add(newText);
                currentText = newText;

                if (textsList.Count == 0)
                {
                    var t = textsList;
                    textsList = secondTextsList;
                    secondTextsList = t;
                }

                newText = GetRandomText();
            }
        }

        void Stablize()
        {
            SetTextOpacity(currentText, 1);
            HideAllTexts();
        }

        Text GetRandomText()
        {
            return textsList[UnityEngine.Random.Range(0, textsList.Count)];
        }

        void SetTextOpacity(Text oldText, float opacity)
        {
            var oldTextColor = oldText.color;
            oldTextColor.a = opacity;
            oldText.color = oldTextColor;
        }
    }

}