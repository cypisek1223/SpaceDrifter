using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangButtonColor : MonoBehaviour
{
    public Image buttonImage1;
    public Image buttonImage2;
    public Image buttonImage3;
    public Camera uiCamera;
    public Canvas cameraCanvas;
    public Color lightColor = Color.white;
    public Color darkColor = Color.black;

    private Texture2D screenTexture;

    void Start()
    {
        
        // Ustawienie domyœlnej kamery, jeœli nie jest przypisana
        if (uiCamera == null)
        {
            uiCamera = Camera.main;
            cameraCanvas.worldCamera = Camera.main;
        }

        // Inicjalizacja Texture2D
        screenTexture = new Texture2D(1, 1, TextureFormat.RGB24, false);
        
        
    }

    void Update()
    {
        // Jeœli uiCamera nadal nie jest przypisana, zakoñcz Update
        if (uiCamera == null) return;

        // Rozpocznij coroutine do sprawdzania koloru
        StartCoroutine(CheckButtonColor());
    }

    IEnumerator CheckButtonColor()
    {
        // Poczekaj na koniec frame'u
        yield return new WaitForEndOfFrame();

        Vector3[] corners = new Vector3[4];
        buttonImage1.rectTransform.GetWorldCorners(corners);

        // Œrodkowy punkt przycisku
        Vector3 center = (corners[0] + corners[2]) / 2;

        // Rzutowanie œrodkowego punktu przycisku na ekran
        Vector3 screenPoint = uiCamera.WorldToScreenPoint(center);

        // Sprawdzenie, czy punkt ekranu jest w granicach
        if (IsScreenPointValid(screenPoint))
        {
            // Pobranie koloru piksela w œrodkowym punkcie przycisku
            Color backgroundColor = GetBackgroundColor((int)screenPoint.x, (int)screenPoint.y);

            // Zmiana koloru przycisku na podstawie jasnoœci t³a
            buttonImage1.color = IsDarkColor(backgroundColor) ? lightColor : darkColor;
            buttonImage2.color = buttonImage1.color;
            buttonImage3.color = buttonImage1.color;
        }
        else
        {
            Debug.LogWarning("Screen point is out of bounds: " + screenPoint);
        }
    }

    Color GetBackgroundColor(int x, int y)
    {
        // Pobranie piksela z ekranu w okreœlonym punkcie
        screenTexture.ReadPixels(new Rect(x, Screen.height - y - 1, 1, 1), 0, 0);
        screenTexture.Apply();

        return screenTexture.GetPixel(0, 0);
    }

    bool IsScreenPointValid(Vector3 screenPoint)
    {
        // Sprawdzenie, czy punkt ekranu jest w granicach
        return screenPoint.x >= 0 && screenPoint.x < Screen.width && screenPoint.y >= 0 && screenPoint.y < Screen.height;
    }

    bool IsDarkColor(Color color)
    {
        // Algorytm okreœlania jasnoœci koloru uwzglêdniaj¹cy percepcjê ludzkiego oka
        float brightness = 0.2126f * color.r + 0.7152f * color.g + 0.0722f * color.b;
        return brightness < 0.5f;
    }

    void OnDestroy()
    {
        if (screenTexture != null)
        {
            Destroy(screenTexture);
        }
    }
}
