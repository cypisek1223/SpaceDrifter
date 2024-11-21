using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private float progress; // Postêp ³adowania (do debugowania w Inspectorze)
        [SerializeField] private float addedTime = 1f; // Opcjonalny dodatkowy czas na ekranie ³adowania

        [Header("Spaceship Animation Settings")]
        [SerializeField] private GameObject spaceship; // Obiekt statku
        [SerializeField] private Transform startPosition; // Punkt pocz¹tkowy animacji
        [SerializeField] private Transform endPosition;   // Punkt koñcowy animacji
        [SerializeField] private float animationDuration = 3f; // Czas trwania animacji statku (w sekundach)

        private bool animationCompleted = false; // Flaga, która okreœla, czy animacja zosta³a zakoñczona

        /// <summary>
        /// Rozpoczyna proces ³adowania scen z animacj¹ statku.
        /// </summary>
        /// <param name="aops">Operacje ³adowania sceny</param>
        /// <param name="callback">Akcja do wykonania po zakoñczeniu</param>
        public void Process(AsyncOperation[] aops, Action callback)
        {
            ShowLoadingScreen();

            // Usuñ null-e z operacji
            aops = aops.Where(a => a != null).ToArray();

            // Ustaw stateczek w pozycji pocz¹tkowej
            if (spaceship != null && startPosition != null)
            {
                spaceship.transform.position = startPosition.position;
            }

            // Rozpocznij korutynê ³adowania i animacji
            StartCoroutine(ShowProgress(aops, callback));
        }

        /// <summary>
        /// Korutyna œledz¹ca postêp ³adowania i animacjê.
        /// </summary>
        private IEnumerator ShowProgress(AsyncOperation[] aops, Action callback)
        {
            float animationStartTime = Time.time; // Czas rozpoczêcia animacji
            animationCompleted = false; // Upewnij siê, ¿e animacja jeszcze siê nie zakoñczy³a

            // Dopóki jakakolwiek operacja nie jest zakoñczona, aktualizuj postêp
            while (aops.Any(ao => !ao.isDone) || !animationCompleted)
            {
                // Oblicz œredni postêp ³adowania
                progress = aops.Sum(ao => ao.progress) / aops.Length;

                // Oblicz czas, który up³yn¹³ od rozpoczêcia animacji
                float elapsedTime = Time.time - animationStartTime;

                // Zaktualizuj pozycjê statku (animacja) w zale¿noœci od up³yniêtego czasu
                if (spaceship != null && startPosition != null && endPosition != null)
                {
                    // Animacja na podstawie czasu
                    float lerpProgress = Mathf.Clamp01(elapsedTime / animationDuration); // Normalizujemy czas (0-1)
                    spaceship.transform.position = Vector3.Lerp(startPosition.position, endPosition.position, lerpProgress);

                    // SprawdŸ, czy animacja siê zakoñczy³a
                    if (lerpProgress >= 1f)
                    {
                        animationCompleted = true;
                    }
                }

                yield return null; // Czekaj jedn¹ klatkê
            }

            // Dodatkowy czas po zakoñczeniu ³adowania
            yield return new WaitForSeconds(addedTime);

            // Ukryj ekran ³adowania i wywo³aj callback
            HideLoadingScreen();
            callback?.Invoke();
        }

        /// <summary>
        /// Pokazuje ekran ³adowania.
        /// </summary>
        private void ShowLoadingScreen()
        {
            gameObject.SetActive(true); // W³¹cz obiekt LoadingScreen
        }

        /// <summary>
        /// Ukrywa ekran ³adowania.
        /// </summary>
        private void HideLoadingScreen()
        {
            gameObject.SetActive(false); // Wy³¹cz obiekt LoadingScreen
        }
    }
}
