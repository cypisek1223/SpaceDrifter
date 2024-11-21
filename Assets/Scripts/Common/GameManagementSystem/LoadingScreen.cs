using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private float progress; // Post�p �adowania (do debugowania w Inspectorze)
        [SerializeField] private float addedTime = 1f; // Opcjonalny dodatkowy czas na ekranie �adowania

        [Header("Spaceship Animation Settings")]
        [SerializeField] private GameObject spaceship; // Obiekt statku
        [SerializeField] private Transform startPosition; // Punkt pocz�tkowy animacji
        [SerializeField] private Transform endPosition;   // Punkt ko�cowy animacji
        [SerializeField] private float animationDuration = 3f; // Czas trwania animacji statku (w sekundach)

        private bool animationCompleted = false; // Flaga, kt�ra okre�la, czy animacja zosta�a zako�czona

        /// <summary>
        /// Rozpoczyna proces �adowania scen z animacj� statku.
        /// </summary>
        /// <param name="aops">Operacje �adowania sceny</param>
        /// <param name="callback">Akcja do wykonania po zako�czeniu</param>
        public void Process(AsyncOperation[] aops, Action callback)
        {
            ShowLoadingScreen();

            // Usu� null-e z operacji
            aops = aops.Where(a => a != null).ToArray();

            // Ustaw stateczek w pozycji pocz�tkowej
            if (spaceship != null && startPosition != null)
            {
                spaceship.transform.position = startPosition.position;
            }

            // Rozpocznij korutyn� �adowania i animacji
            StartCoroutine(ShowProgress(aops, callback));
        }

        /// <summary>
        /// Korutyna �ledz�ca post�p �adowania i animacj�.
        /// </summary>
        private IEnumerator ShowProgress(AsyncOperation[] aops, Action callback)
        {
            float animationStartTime = Time.time; // Czas rozpocz�cia animacji
            animationCompleted = false; // Upewnij si�, �e animacja jeszcze si� nie zako�czy�a

            // Dop�ki jakakolwiek operacja nie jest zako�czona, aktualizuj post�p
            while (aops.Any(ao => !ao.isDone) || !animationCompleted)
            {
                // Oblicz �redni post�p �adowania
                progress = aops.Sum(ao => ao.progress) / aops.Length;

                // Oblicz czas, kt�ry up�yn�� od rozpocz�cia animacji
                float elapsedTime = Time.time - animationStartTime;

                // Zaktualizuj pozycj� statku (animacja) w zale�no�ci od up�yni�tego czasu
                if (spaceship != null && startPosition != null && endPosition != null)
                {
                    // Animacja na podstawie czasu
                    float lerpProgress = Mathf.Clamp01(elapsedTime / animationDuration); // Normalizujemy czas (0-1)
                    spaceship.transform.position = Vector3.Lerp(startPosition.position, endPosition.position, lerpProgress);

                    // Sprawd�, czy animacja si� zako�czy�a
                    if (lerpProgress >= 1f)
                    {
                        animationCompleted = true;
                    }
                }

                yield return null; // Czekaj jedn� klatk�
            }

            // Dodatkowy czas po zako�czeniu �adowania
            yield return new WaitForSeconds(addedTime);

            // Ukryj ekran �adowania i wywo�aj callback
            HideLoadingScreen();
            callback?.Invoke();
        }

        /// <summary>
        /// Pokazuje ekran �adowania.
        /// </summary>
        private void ShowLoadingScreen()
        {
            gameObject.SetActive(true); // W��cz obiekt LoadingScreen
        }

        /// <summary>
        /// Ukrywa ekran �adowania.
        /// </summary>
        private void HideLoadingScreen()
        {
            gameObject.SetActive(false); // Wy��cz obiekt LoadingScreen
        }
    }
}
