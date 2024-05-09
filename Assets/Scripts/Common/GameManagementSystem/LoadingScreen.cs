using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class LoadingScreen : MonoBehaviour
    {
        float progress;

        public void Process( AsyncOperation[] aops, Action callback )
        {
            ShowLoadingScreen();

            // Remove nulls
            aops = aops.Where(a => a != null).ToArray();

            StartCoroutine( ShowProgress(aops, callback) );
        }

        private IEnumerator ShowProgress(AsyncOperation[] aops, Action callback)
        {
            // Untill all async operations are done, update progress
            while (aops.Any(ao => !ao.isDone))
            {
                progress = aops.Sum(ao => ao.progress) / aops.Length;
                yield return null;
            }

            HideLoadingScreen();
            callback?.Invoke();
        }

        private void ShowLoadingScreen()
        {
            gameObject.SetActive(true);
        }

        private void HideLoadingScreen()
        {
            gameObject.SetActive(false);
        }
    } 
}
