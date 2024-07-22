using UnityEngine;
using Cinemachine;

public class DynamicCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform playerTransform;
    public float speedThreshold = 5f; // Pr�g pr�dko�ci, powy�ej kt�rego kamera b�dzie porusza� si� przed graczem
    public float lookAheadDistance = 5f; // Dystans, o jaki kamera b�dzie wyprzedza� gracza
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

        // Oblicz pr�dko�� gracza
        float playerSpeed = playerTransform.GetComponent<Rigidbody2D>().velocity.magnitude;

        if (playerSpeed > speedThreshold)
        {
            // Przesu� kamer� przed gracza
            Vector3 lookAhead = playerTransform.position + playerTransform.right * lookAheadDistance;
            virtualCamera.Follow.position = Vector3.Lerp(virtualCamera.Follow.position, lookAhead, Time.deltaTime);
        }
        else
        {
            // �led� gracza normalnie
            virtualCamera.Follow.position = playerTransform.position;
        }
    }
}
