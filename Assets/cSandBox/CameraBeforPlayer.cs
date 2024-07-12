using UnityEngine;
using Cinemachine;

namespace SpaceDrifter2D
{
    public class CameraBeforPlayer : MonoBehaviour
    {
        public Transform playerTransform;
        public CinemachineVirtualCamera virtualCamera;
        public float speedThreshold = 5f; // Pr�g pr�dko�ci
        public float offsetDistance = 5f; // Dystans od gracza
        public float smoothSpeed = 0.125f; // G�adko�� poruszania si� kamery

        //[Header("Important")]
        //public float playerSpeed;

        private Vector3 initialOffset;
        private CinemachineTransposer transposer;
        private Rigidbody2D playerRigidbody;

        void Start()
        {
            if (playerTransform == null || virtualCamera == null)
            {
                Debug.LogError("Brak przypisanego obiektu gracza lub kamery.");
                return;
            }

            // Pobierz komponent transposer
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer == null)
            {
                Debug.LogError("CinemachineVirtualCamera nie ma komponentu CinemachineTransposer.");
                return;
            }

            // Pobierz komponent Rigidbody2D gracza
            playerRigidbody = playerTransform.GetComponent<Rigidbody2D>();
            if (playerRigidbody == null)
            {
                Debug.LogError("Gracz nie ma komponentu Rigidbody2D.");
                return;
            }

            // Zapisz pocz�tkowe przesuni�cie kamery wzgl�dem gracza
            initialOffset = transposer.m_FollowOffset;
        }

        void LateUpdate()
        {
            UpdateCameraPosition();
        }

        void UpdateCameraPosition()
        {
            if (playerTransform == null || playerRigidbody == null)
            {
                return;
            }

            // Oblicz pr�dko�� gracza
            float playerSpeed = playerRigidbody.velocity.magnitude;

            // Je�li pr�dko�� gracza jest mniejsza ni� pr�g, kamera �ledzi gracza normalnie
            if (playerSpeed < speedThreshold)
            {
                transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, initialOffset, smoothSpeed * 5f);
                return;
            }

            // Oblicz kierunek, w kt�rym skierowany jest gracz
            Vector3 playerForward = playerTransform.up.normalized;

            // Oblicz docelow� pozycj� przesuni�cia
            Vector3 targetOffset = initialOffset + new Vector3(
                playerForward.x * offsetDistance,
                playerForward.y * offsetDistance,
                0
            );

            // P�ynne przesuni�cie kamery do docelowej pozycji
            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, targetOffset, smoothSpeed);
        }
    }
}