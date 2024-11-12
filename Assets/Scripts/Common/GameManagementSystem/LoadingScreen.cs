using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] float progress;
        [SerializeField] float addedTime;

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
            yield return new WaitForSeconds(addedTime);

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
