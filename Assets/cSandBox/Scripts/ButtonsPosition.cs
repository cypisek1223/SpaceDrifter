using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsPosition : MonoBehaviour
{
    public RectTransform leftBottomButton; // Przycisk w lewym dolnym rogu
    public RectTransform rightBottomButton; // Przycisk w prawym dolnym rogu
    public float referenceWidth = 800f; // Referencyjna szeroko�� ekranu
    public float referenceHeight = 480f; // Referencyjna wysoko�� ekranu
    public float anchorXL, anchorYL, anchorXR, anchorYR;

    void Start()
    {
        PositionButtons();
    }

    void Update()
    {
        PositionButtons();
    }

    void PositionButtons()
    {
        referenceWidth = Screen.width;
        referenceHeight = Screen.height;
        // Oblicz skal� na podstawie referencyjnych wymiar�w
        float scaleFactorX = Screen.width / referenceWidth;
        float scaleFactorY = Screen.height / referenceHeight;
        float scaleFactor = Mathf.Min(scaleFactorX, scaleFactorY);

        // Ustaw pozycje przycisk�w
        SetButtonPosition(leftBottomButton, new Vector2(anchorXL, anchorYL), scaleFactor);
        SetButtonPosition(rightBottomButton, new Vector2(anchorXR, anchorYR), scaleFactor);
    }

    void SetButtonPosition(RectTransform button, Vector2 anchor, float scaleFactor)
    {
        button.anchorMin = anchor;
        button.anchorMax = anchor;
        button.pivot = new Vector2(0.5f, 0.5f);

        // Ustaw pozycj� przycisku na przesuni�cie od kraw�dzi ekranu
        button.anchoredPosition = new Vector2((anchor.x == 0 ? 1 : -1) * button.rect.width * scaleFactor / 2,
                                              button.rect.height * scaleFactor / 2);

        // Ustaw skal� przycisku
        button.localScale = new Vector3(scaleFactor, scaleFactor, 1);
    }
}