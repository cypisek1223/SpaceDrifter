using UnityEngine;
using Cinemachine;

public class DynamicCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform playerTransform;
    public float speedThreshold = 5f; // Próg prêdkoœci, powy¿ej którego kamera bêdzie poruszaæ siê przed graczem
    public float lookAheadDistance = 5f; // Dystans, o jaki kamera bêdzie wyprzedzaæ gracza
    private CinemachineFramingTransposer framingTransposer;

    void Start()
    {
        if (virtualCamera != null)
        {
            framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        else
        {
            Debug.LogError("Brak przypisanej kamery wirtualnej Cinemachine.");
        }
    }

    void Update()
    {
        if (framingTransposer == null || playerTransform == null)
        {
            return;
        }

        // Oblicz prêdkoœæ gracza
        float playerSpeed = playerTransform.GetComponent<Rigidbody2D>().velocity.magnitude;

        if (playerSpeed > speedThreshold)
        {
            // Przesuñ kamerê przed gracza
            Vector3 lookAhead = playerTransform.position + playerTransform.right * lookAheadDistance;
            virtualCamera.Follow.position = Vector3.Lerp(virtualCamera.Follow.position, lookAhead, Time.deltaTime);
        }
        else
        {
            // ŒledŸ gracza normalnie
            virtualCamera.Follow.position = playerTransform.position;
        }
    }
}
