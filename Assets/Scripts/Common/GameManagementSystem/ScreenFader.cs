using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SpaceDrifter2D
{
    public class ScreenFader : SingleInstance<ScreenFader>
    {
        [SerializeField] private Image image;
        public float fadeSpeed = 1;

        private event Action OnFade;

        public void Fade( Action callback )
        {
            SetFadeCallback(callback);
            RevealImmediately();

            Color c = image.color;
            c.a = 1;
            StartCoroutine(SetColor(c));
        }
        public void Reveal( Action callback )
        {
            SetFadeCallback(callback);
            FadeImmediately();

            Color c = image.color;
            c.a = 0;
            StartCoroutine(SetColor(c));
        }

        private void FadeImmediately()
        {
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }
        private void RevealImmediately()
        {
            Color c = image.color;
            c.a = 0;
            image.color = c;
        }
        private void SetFadeCallback(Action callback)
        {
            OnFade = null;
            OnFade += callback;
        }
        private IEnumerator SetColor(Color c)
        {
            Color startC = image.color;
            for (float t = 0; t < 1; t += Time.unscaledDeltaTime * fadeSpeed)
            {
                image.color = Color.Lerp(startC, c, t);
                yield return null;
            }

            image.color = c;
            OnFade?.Invoke();
        }

    }
}
